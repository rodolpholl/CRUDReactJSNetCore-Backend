using CRUDReactJSNetCore.Domain.Entities;
using FuncionarioDomain = CRUDReactJSNetCore.Domain.Entities.Funcionario;

namespace CRUDReactJSNetCore.Application.Models
{
    public class FuncionarioModel
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required long CargoId { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Documento { get; set; }
        public string[]? Telefone { get; set; }
        public long GestorId { get; set; }

        internal FuncionarioDomain Gestor { get; set; }
        internal Cargo Cargo { get; set; }
    }
}
