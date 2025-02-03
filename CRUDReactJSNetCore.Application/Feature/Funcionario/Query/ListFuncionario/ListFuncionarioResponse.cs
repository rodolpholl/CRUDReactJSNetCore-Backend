namespace CRUDReactJSNetCore.Application.Feature.Funcionario.Query.ListFuncionario
{
    public class ListFuncionarioResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Gestor { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public bool Active { get; set; }
    }
}
