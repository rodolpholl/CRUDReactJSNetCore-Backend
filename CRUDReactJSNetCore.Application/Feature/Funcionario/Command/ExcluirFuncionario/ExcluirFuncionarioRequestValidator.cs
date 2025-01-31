using CRUDReactJSNetCore.Application.Repository;
using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ExcluirFuncionario
{
    public class ExcluirFuncionarioRequestValidator : AbstractValidator<ExcluirFuncionarioRequest>
    {
        protected readonly IFuncionarioRepository _funcionarioRepository;

        public ExcluirFuncionarioRequestValidator(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;

            RuleFor(x => x.FuncionarioId)
               .NotNull()
               .GreaterThan(0)
               .MustAsync((x, ct) =>
                   _funcionarioRepository.FuncionarioExists(x)
               )
               .WithMessage(x => $"Não existe funcionario com o Id '{x.FuncionarioId}'");
        }
    }
}
