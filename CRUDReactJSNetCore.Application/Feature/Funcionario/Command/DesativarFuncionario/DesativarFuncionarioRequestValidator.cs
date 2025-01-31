using CRUDReactJSNetCore.Application.Repository;
using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.DesativarFuncionario
{
    public class DesativarFuncionarioRequestValidator : AbstractValidator<DesativarFuncionarioRequest>
    {
        protected readonly IFuncionarioRepository _funcionarioRepository;

        public DesativarFuncionarioRequestValidator(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;

            RuleFor(x => x.FuncionarioId).Cascade(CascadeMode.Stop)
                .NotNull()
                .GreaterThan(0)
                .MustAsync((x, ct) =>
                    _funcionarioRepository.FuncionarioExists(x)
                )
                .WithMessage(x => $"Não existe funcionario com o Id '{x.FuncionarioId}'")
                .MustAsync(async (x, ct) =>
                    await _funcionarioRepository.FuncionarioAtivo(x)
                )
                .WithMessage(x => $"Funcionario com o Id '{x.FuncionarioId}' já está desativado.");
        }
    }
}
