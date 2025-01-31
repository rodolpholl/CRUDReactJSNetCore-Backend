using CRUDReactJSNetCore.Application.Utils;

namespace Funcionario.Api
{
    public static class RequestHelper
    {
        public static void ValidarPayload(object payload)
        {
            if (payload == null)
                throw new NullReferenceException("Payload nulo ou inválido");
        }

        public static IResult ExceptionResult(Exception ex)
        {
            if (ex.Message.Contains(ConstantsUtils.VALIDATION_ERROR))
                return Results.Problem(ex.Message, statusCode: 400);
            else
                return Results.Problem(ex.Message, statusCode: 500);

        }
    }
}
