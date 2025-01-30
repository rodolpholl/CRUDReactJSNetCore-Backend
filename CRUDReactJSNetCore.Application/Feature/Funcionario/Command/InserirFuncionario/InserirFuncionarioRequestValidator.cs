using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Application.Validators;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.InserirFuncionario
{
    public class InserirFuncionarioRequestValidator : FuncionarioValidatorBase<InserirFuncionarioRequest>
    {
        public InserirFuncionarioRequestValidator(IFuncionarioRepository funcionarioRepository, ICargoRepository cargoRepository) : base(funcionarioRepository, cargoRepository)
        {
        }
    }
}
