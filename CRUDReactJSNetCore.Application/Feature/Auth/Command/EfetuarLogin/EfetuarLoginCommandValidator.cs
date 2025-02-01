using CRUDReactJSNetCore.Application.Helpers;
using CRUDReactJSNetCore.Application.Repository;
using FluentValidation;
using FluentValidation.Results;

namespace CRUDReactJSNetCore.Application.Feature.Auth.Command.EfetuarLogin
{
    public class EfetuarLoginCommandValidator : AbstractValidator<EfetuarLoginCommand>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private Domain.Entities.Funcionario _funcionario;


        public EfetuarLoginCommandValidator(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;


            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();


        }

        protected override bool PreValidate(ValidationContext<EfetuarLoginCommand> context, ValidationResult result)
        {
            const string ERRO_ACESSO = "Acesso Negado. Verifique o email e sua senha informados";
            var email = context.InstanceToValidate.Email;
            var password = context.InstanceToValidate.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(password))
            {
                result.Errors.Add(new ValidationFailure { ErrorMessage = ERRO_ACESSO, Severity = Severity.Error });
                return base.PreValidate(context, result);
            }

            _funcionario = Task.Run(() => _funcionarioRepository.GetFuncionarioByEmail(context.InstanceToValidate.Email))
                                        .GetAwaiter().GetResult();

            if (_funcionario == null)
                result.Errors.Add(new ValidationFailure { ErrorMessage = ERRO_ACESSO, Severity = Severity.Error });

            else
            {
                var senhaCorreta = EncryptHelper.Validar(password, _funcionario.Senha);
                if (!senhaCorreta)
                    result.Errors.Add(new ValidationFailure { ErrorMessage = ERRO_ACESSO, Severity = Severity.Error });
                else
                    context.InstanceToValidate.Funcionario = _funcionario;
            }


            return base.PreValidate(context, result);
        }
    }
}
