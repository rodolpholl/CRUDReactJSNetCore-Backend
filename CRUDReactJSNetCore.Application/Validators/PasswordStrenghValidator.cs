using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CRUDReactJSNetCore.Application.Validators
{
    public class PasswordStrenghValidator<T> : PropertyValidator<T, string>
    {

        public override string Name => "PasswordStrenghValidator";
        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return "{PropertyName} deve ser uma senha forte (mínimo de 8 caracteres, com pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial)";
        }
        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            // Regras para senha forte
            bool temLetraMaiuscula = Regex.IsMatch(value, "[A-Z]");
            bool temLetraMinuscula = Regex.IsMatch(value, "[a-z]");
            bool temNumero = Regex.IsMatch(value, "[0-9]");
            bool temCaractereEspecial = Regex.IsMatch(value, "[^a-zA-Z0-9]");

            return value.Length >= 8 &&
                   temLetraMaiuscula &&
                   temLetraMinuscula &&
                   temNumero &&
                   temCaractereEspecial;
        }
    }
}
