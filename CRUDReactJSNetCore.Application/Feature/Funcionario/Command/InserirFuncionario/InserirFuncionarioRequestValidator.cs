using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Application.Validators;
using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.InserirFuncionario
{
    public class InserirFuncionarioRequestValidator : FuncionarioValidatorBase<InserirFuncionarioRequest>
    {
        public InserirFuncionarioRequestValidator(IFuncionarioRepository funcionarioRepository, ICargoRepository cargoRepository) : base(funcionarioRepository, cargoRepository)
        {
            RuleFor(x => x.Documento)
                .MustAsync(async (x, ct) => !(await _funcionarioRepository.NumeroDocumentoExists(x)))
                .WithMessage(x => $"Já existe um Funcionário com documento número '{x.Documento}'");
        }
    }
}
