using CRUDReactJSNetCore.Application.Utils;
using FluentValidation;
using MediatR;

namespace CRUDReactJSNetCore.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));

                var failures = validationResult
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    var falhas = failures.Select(x => x.ErrorMessage).ToArray();
                    throw new ValidationException($"{ConstantsUtils.VALIDATION_ERROR}|{string.Join($"{Environment.NewLine}- ", falhas)}", failures);
                }


            }

            return await next();
        }
    }
}
