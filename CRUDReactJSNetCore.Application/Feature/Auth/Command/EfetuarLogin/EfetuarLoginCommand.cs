using MediatR;
using FuncionarioDomain = CRUDReactJSNetCore.Domain.Entities.Funcionario;
namespace CRUDReactJSNetCore.Application.Feature.Auth.Command.EfetuarLogin
{
    public class EfetuarLoginCommand : IRequest<EfetuarLoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        internal FuncionarioDomain Funcionario { get; set; }
    }
}
