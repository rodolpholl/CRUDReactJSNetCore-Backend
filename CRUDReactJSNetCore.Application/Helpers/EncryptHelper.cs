namespace CRUDReactJSNetCore.Application.Helpers
{
    public static class EncryptHelper
    {
        public static string EncriptarPassword(string conteudo)
            => BCrypt.Net.BCrypt.HashPassword(conteudo);

        public static bool Validar(string contentA, string contentB)
            => BCrypt.Net.BCrypt.Verify(contentA, contentB);
    }
}
