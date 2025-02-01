using System.Text;

namespace CRUDReactJSNetCore.Application.Utils
{
    public static class FuncionarioUtils
    {
        private static readonly Random _random = new Random();

        public static string GeneratePassword()
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitos = "0123456789";
            const string caracteresEspeciais = "!@#$%";

            // Garantindo que cada tipo de caractere esteja presente
            StringBuilder password = new StringBuilder();

            password.Append(lowercase[_random.Next(lowercase.Length)]);
            password.Append(uppercase[_random.Next(uppercase.Length)]);
            password.Append(digitos[_random.Next(digitos.Length)]);
            password.Append(caracteresEspeciais[_random.Next(caracteresEspeciais.Length)]);

            // Preenchendo os caracteres restantes para completar 8
            string allChars = lowercase + uppercase + digitos + caracteresEspeciais;
            for (int i = password.Length; i < 8; i++)
                password.Append(allChars[_random.Next(allChars.Length)]);

            // Embaralhando a senha para aumentar a aleatoriedade
            return new string(password.ToString().OrderBy(c => _random.Next()).ToArray());

        }
    }
}
