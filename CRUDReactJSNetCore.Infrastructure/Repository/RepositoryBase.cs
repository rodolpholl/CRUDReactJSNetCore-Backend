using CRUDReactJSNetCore.Domain.Entities;
using CRUDReactJSNetCore.Infrastructure.ContextDb;

namespace CRUDReactJSNetCore.Infrastructure.Repository
{
    public abstract class RepositoryBase<T> where T : EntityBase
    {
        protected readonly CRUDReactJSNetCoreDbContent _dbContext;
        public RepositoryBase(CRUDReactJSNetCoreDbContent dbContext)
        {
            _dbContext = dbContext;
        }

        protected virtual Task SaveOperationAsync()
        => _dbContext.SaveChangesAsync();

        protected virtual async Task<T> Inserir(T entity, bool autoSave = true)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            if (autoSave)
                await SaveOperationAsync();

            return entity;
        }

        protected virtual async Task<T> Alterar(T entity, bool autoSave = true)
        {
            _dbContext.Set<T>().Update(entity);
            if (autoSave)
                await SaveOperationAsync();

            return entity;
        }

        protected virtual async Task<bool> Excluir(T entity, bool autoSave = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (autoSave)
                await SaveOperationAsync();

            return true;
        }

        protected virtual async Task<T?> GetById(long id)
        => await _dbContext.Set<T>().FindAsync(id);

        protected IQueryable<T> PaginateQuery(IQueryable<T> query, int pageIndex, int pageCont)
        => query.Skip(pageIndex * pageCont).Take(pageCont);

    }
}
