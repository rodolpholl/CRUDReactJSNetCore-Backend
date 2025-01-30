using CRUDReactJSNetCore.Application.Models;
using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.AlterarFuncionario
{
    public class AlterarFuncionarioRequest : FuncionarioModel, IRequest<long>
    {
        public long Id { get; set; }

    }
}
