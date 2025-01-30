using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDReactJSNetCore.Domain.Entities
{
    public class Funcionario : EntityBase
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required long CargoId { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Documento { get; set; }
        public string TelefoneString { get; set; }
        [NotMapped]
        public string[]? Telefone
        {
            get => TelefoneString.Split("|");
            set
            {
                if (value != null)
                {
                    TelefoneString = string.Join("|", value);
                }
            }
        }
        public long GestorId { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public long UsuarioCadastro { get; set; }
        public DateTime DataDesativacao { get; set; }
        public long UsuarioDesativacao { get; set; }
        public DateTime DataReativacao { get; set; }
        public long UsuarioReativacao { get; set; }


        #region Relacionamentos

        public Cargo Cargo { get; set; }
        public Funcionario Gestor { get; set; }

        #endregion
    }
}
