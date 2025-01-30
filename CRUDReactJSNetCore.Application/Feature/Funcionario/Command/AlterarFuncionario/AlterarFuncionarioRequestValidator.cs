using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Application.Validators;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.AlterarFuncionario
{
    public class AlterarFuncionarioRequestValidator : FuncionarioValidatorBase<AlterarFuncionarioRequest>
    {
        public AlterarFuncionarioRequestValidator(IFuncionarioRepository funcionarioRepository, ICargoRepository cargoRepository) : base(funcionarioRepository, cargoRepository)
        {
        }
    }
}
