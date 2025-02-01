using MediatR;
using FuncionarioDomain = CRUDReactJSNetCore.Domain.Entities.Funcionario;
namespace CRUDReactJSNetCore.Application.Feature.Auth.Command.AlterarPassword
{
    public class AlterarPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmarSenha { get; set; }

        internal FuncionarioDomain Funcionario { get; set; }
    }
}
