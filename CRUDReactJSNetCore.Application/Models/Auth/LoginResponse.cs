namespace CRUDReactJSNetCore.Application.Models.Auth
{
    public record LoginResponse(bool Flag, string Message = null!, string Token = null!);

}
