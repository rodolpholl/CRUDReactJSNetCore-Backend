using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.AlterarFuncionario
{
    public class AlterarFuncionarioRequest : IRequest<long>
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required long CargoId { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Documento { get; set; }
        public string[]? Telefone { get; set; }
        public long GestorId { get; set; }
    }
}
