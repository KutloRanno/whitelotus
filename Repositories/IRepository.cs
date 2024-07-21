using System.Linq.Expressions;
using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service.Repositories;

// public interface IRepository<T> where T: IEntity
public interface IRepository<TEntity>
{
        // Task CreateAsync(T entity);
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(int id);
        // Task<T> GetAsync(Guid id);
        Task<TEntity?> GetAsync(int id);
        // Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T,bool>>filter);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>>filter);
        // Task<T> GetAsync(Expression<Func<T,bool>>filter);
        Task<TEntity> GetAsync(Expression<Func<TEntity,bool>>filter);
        // Task <IReadOnlyCollection<T>> GetAllAsync();
        Task <IReadOnlyCollection<TEntity>> GetAllAsync();
        // Task UpdateAsync(T updatedItem);
        Task UpdateAsync(TEntity updatedItem);

}