using CRUDReactJSNetCore.Domain.Entities;

namespace CRUDReactJSNetCore.Application.Repository
{
    public interface IFuncionarioRepository
    {
        Task<Funcionario> InserirFuncionario(Funcionario funcionario);
        Task<Funcionario> AlterarFuncionario(Funcionario funcionario);
        Task<bool> ExcluirFuncionario(Funcionario funcionario);

        Task<Funcionario> GetFuncionarioById(long funcionarioId, bool addRelationships = true);
        Task<List<Funcionario>> ListFuncionarios(int pageIndex, int pageCount, string filter, bool addRelationships = true);


        Task<bool> FuncionarioExists(long funcinarioId);
        Task<bool> FuncionarioAtivo(long funcinarioId);
        Task<bool> NumeroDocumentoExists(string nomeroDocumento, long? idFuncionarioCorrente = null);
        Task<IEnumerable<Funcionario>> ListGestores(long level);
    }
}
