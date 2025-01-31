using MediatR;
using Serilog;

namespace CRUDReactJSNetCore.Application.Behaviors
{
    public class UnhandlerExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;

        public UnhandlerExceptionBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                var msg = $"Application Request: Unhandled Exception for Request {requestName}: {request}";
                _logger.Error(msg, ex);
                throw new Exception(msg, ex);
            }
        }
    }
}
