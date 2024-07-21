using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using WhiteLotus.Main.Service.Data;
using WhiteLotus.Main.Service.Entities;

namespace WhiteLotus.Main.Service.Repositories;

// public class EntityFrameworkWhiteLotusRepository<T>(WhiteLotusContext context) : IRepository<T> where T : class , IEntity
public class EntityFrameworkWhiteLotusRepository<TEntity>(WhiteLotusContext context) : IRepository<TEntity>  where TEntity:class
{
    private readonly WhiteLotusContext _context = context;

    

    public async Task CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
       _context.Set<TEntity>().Remove(entity);
    }

   

    public async Task<IReadOnlyCollection<TEntity>>GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().Where(filter).ToListAsync();
    }

    public async Task<TEntity?> GetAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
    }


    public async Task UpdateAsync(TEntity updatedItem)
    {
        _context.Set<TEntity>().Update(updatedItem);
        await _context.SaveChangesAsync();
    }
}