using CRUDReactJSNetCore.Application.Repository;
using CRUDReactJSNetCore.Domain.Entities;
using CRUDReactJSNetCore.Infrastructure.ContextDb;
using Microsoft.EntityFrameworkCore;

namespace CRUDReactJSNetCore.Infrastructure.Repository
{
    public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(CRUDReactJSNetCoreDbContent dbContext) : base(dbContext)
        {
        }

        public Task<Funcionario> InserirFuncionario(Funcionario funcionario)
        => Alterar(funcionario);

        public Task<Funcionario> AlterarFuncionario(Funcionario funcionario)
        => Alterar(funcionario);


        public Task<bool> ExcluirFuncionario(Funcionario funcionario)
        => Excluir(funcionario);

        public Task<Funcionario> GetFuncionarioById(long funcionarioId, bool addRelationships = true)
        {
            var query = _dbContext.Funcionarios.Where(x => x.Id == funcionarioId).AsQueryable();

            if (addRelationships)
                query = setIncludes(query);

            return query.FirstOrDefaultAsync();
        }



        public Task<List<Funcionario>> ListFuncionarios(int pageIndex, int pageCount, string filter, bool addRelationships = true)
        {
            var query = _dbContext.Funcionarios.Where(x => x.Active == true);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(x => x.Nome.ToUpper().Contains(filter.ToUpper()) ||
                                         x.Cargo.Nome.ToUpper().Contains(filter.ToUpper()) ||
                                         x.Email.Contains(filter));


                if (long.TryParse(filter, out long IdFilter))
                    query = query.Where(x => x.Id == IdFilter);

                if (DateTime.TryParse(filter, out DateTime dateFilter))
                    query = query.Where(x => x.DataNascimento.Date == dateFilter.Date);

            }

            if (addRelationships)
                query = setIncludes(query);


            query = PaginateQuery(query, pageIndex, pageCount);

            return query.ToListAsync();

        }

        private IQueryable<Funcionario> setIncludes(IQueryable<Funcionario> query)
        => query.Include(x => x.Cargo)
                .Include(x => x.Gestor)
                    .ThenInclude(x => x.Cargo)
            .AsQueryable();

        public Task<bool> FuncionarioExists(long funcinarioId)
        => Exists(funcinarioId);

        public Task<bool> NumeroDocumentoExists(string nomeroDocumento, long? idFuncionarioCorrente = null)
        {
            var query = _dbContext.Funcionarios.Where(x => x.Documento == nomeroDocumento);

            if (idFuncionarioCorrente.HasValue)
                query = query.Where(x => x.Id != idFuncionarioCorrente);

            return query.AnyAsync();
        }

        public Task<bool> FuncionarioAtivo(long funcinarioId)
        => _dbContext.Funcionarios.AnyAsync(x => x.Id == funcinarioId && x.Active == true);

        public async Task<IEnumerable<Funcionario>> ListGestores(long level)
        => (await _dbContext.Funcionarios.Where(x => x.Active == true && x.Cargo.Level < level)
            .OrderBy(x => x.Nome).ToListAsync()).AsEnumerable();

    }
}
