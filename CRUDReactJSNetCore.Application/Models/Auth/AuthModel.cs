using System.ComponentModel.DataAnnotations;

namespace CRUDReactJSNetCore.Application.Models.Auth
{
    public class AuthModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}
