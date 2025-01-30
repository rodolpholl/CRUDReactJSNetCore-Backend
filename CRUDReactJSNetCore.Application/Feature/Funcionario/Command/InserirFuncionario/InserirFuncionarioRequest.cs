using CRUDReactJSNetCore.Application.Models;
using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.InserirFuncionario
{
    public class InserirFuncionarioRequest : FuncionarioModel, IRequest<long>
    {

    }
}
