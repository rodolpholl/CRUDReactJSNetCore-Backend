using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ExcluirFuncionario
{
    public class ExcluirFuncionarioRequest : IRequest<bool>
    {
        public long FuncionarioId { get; set; }
    }
}
