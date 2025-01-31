using CRUDReactJSNetCore.Application.Repository;
using FluentValidation;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.ReativarFuncionario
{
    public class ReativarFuncionarioRequestValidator : AbstractValidator<ReativarFuncionarioRequest>
    {
        protected readonly IFuncionarioRepository _funcionarioRepository;

        public ReativarFuncionarioRequestValidator(IFuncionarioRepository funcionarioRepository)
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
                    !(await _funcionarioRepository.FuncionarioAtivo(x))
                )
                .WithMessage(x => $"Funcionario com o Id '{x.FuncionarioId}' já está ativo.");

        }
    }
}
