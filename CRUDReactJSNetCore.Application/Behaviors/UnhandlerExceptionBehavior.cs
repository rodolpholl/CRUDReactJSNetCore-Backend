using MediatR;

namespace CRUDReactJSNetCore.Application.Behaviors
{
    public class UnhandlerExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                throw new Exception($"Application Request: Unhandled Exception for Request {requestName} {request}", ex);
            }
        }
    }
}
