using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.DesativarFuncionario
{
    public class DesativarFuncionarioRequest : IRequest<bool>
    {
        public long FuncionarioId { get; set; }
    }
}
