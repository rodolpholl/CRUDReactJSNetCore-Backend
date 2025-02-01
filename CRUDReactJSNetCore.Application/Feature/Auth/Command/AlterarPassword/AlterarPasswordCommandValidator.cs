using CRUDReactJSNetCore.Application.Helpers;
using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Application.Validators;
using FluentValidation;
using FluentValidation.Results;
using FuncionarioDomain = CRUDReactJSNetCore.Domain.Entities.Funcionario;

namespace CRUDReactJSNetCore.Application.Feature.Auth.Command.AlterarPassword
{
    public class AlterarPasswordCommandValidator : AbstractValidator<AlterarPasswordCommand>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private FuncionarioDomain _funcionario;
        private const string MENSAGEM = "Dados inválidos";

        public AlterarPasswordCommandValidator(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.SenhaAtual)
            .NotEmpty()
                .Must(x => EncryptHelper.Validar(x, _funcionario!.Senha))
                .WithMessage(MENSAGEM);

            RuleFor(x => x.NovaSenha)
                .NotEmpty()
                .SetValidator(new PasswordStrenghValidator<AlterarPasswordCommand>());

            RuleFor(x => x.ConfirmarSenha)
                .NotEmpty()
                .Equal(x => x.NovaSenha)
                .WithMessage("A confirmação da senha deve ser igual a nova senha");

        }

        protected override bool PreValidate(ValidationContext<AlterarPasswordCommand> context, ValidationResult result)
        {

            var email = context.InstanceToValidate.Email;
            var senhaAtual = context.InstanceToValidate.SenhaAtual;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senhaAtual))
            {
                result.Errors.Add(new ValidationFailure { ErrorMessage = MENSAGEM, Severity = Severity.Error });
                return base.PreValidate(context, result);
            }

            _funcionario = Task.Run(() => _funcionarioRepository.GetFuncionarioByEmail(email)).GetAwaiter().GetResult();

            if (_funcionario == null)
                result.Errors.Add(new ValidationFailure { ErrorMessage = MENSAGEM, Severity = Severity.Error });
            else
                context.InstanceToValidate.Funcionario = _funcionario;

            return base.PreValidate(context, result);
        }
    }
}
