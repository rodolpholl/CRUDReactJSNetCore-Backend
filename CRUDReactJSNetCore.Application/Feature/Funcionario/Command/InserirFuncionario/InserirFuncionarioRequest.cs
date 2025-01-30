using MediatR;

namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Command.InserirFuncionario
{
    public class InserirFuncionarioRequest : IRequest<long>
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required long CargoId { get; set; }
        public DateTime DataNascimento { get; set; }
        public required string Documento { get; set; }
        public string[]? Telefone { get; set; }
        public long GestorId { get; set; }
    }
}
