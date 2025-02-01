namespace CRUDReactJSNetCore.Application.Helpers
{
    public static class EncryptHelper
    {
        public static string EncriptarPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool Validar(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
