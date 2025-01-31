using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Application.Validators;
using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.AlterarFuncionario
{
    public class AlterarFuncionarioRequestValidator : FuncionarioValidatorBase<AlterarFuncionarioRequest>
    {
        public AlterarFuncionarioRequestValidator(IFuncionarioRepository funcionarioRepository, ICargoRepository cargoRepository) : base(funcionarioRepository, cargoRepository)
        {


            RuleFor(x => x.Id)
               .NotNull()
               .GreaterThan(0)
               .MustAsync((x, ct) =>
                   _funcionarioRepository.FuncionarioExists(x)
               )
               .WithMessage(x => $"Não existe funcionario com o Id '{x.Id}'");

            RuleFor(x => x.Documento)
                .MustAsync(async (model, x, ct) => !(await _funcionarioRepository.NumeroDocumentoExists(x, model.Id)))
                .WithMessage(x => $"Já existe um Funcionário com documento número '{x.Documento}'");
        }
    }
}
