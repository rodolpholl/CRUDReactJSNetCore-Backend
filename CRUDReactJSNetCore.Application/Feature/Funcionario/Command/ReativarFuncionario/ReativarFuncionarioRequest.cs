using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ReativarFuncionario
{
    public class ReativarFuncionarioRequest : IRequest<bool>
    {
        public long FuncionarioId { get; set; }
    }
}
