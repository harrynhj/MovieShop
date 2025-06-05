using ApplicationCore.Contracts.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MovieShopDbContext _movieShopDbContext;
    public Repository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }
    
    public async Task<T> Insert(T entity)
    {
        _movieShopDbContext.Set<T>().Add(entity);
        await _movieShopDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteById(int id)
    {
        var entity = _movieShopDbContext.Set<T>().Find(id);
        if (entity != null)
        {
            _movieShopDbContext.Set<T>().Remove(entity);
            await _movieShopDbContext.SaveChangesAsync();
            return entity;
        }

        return null;
    }

    public async Task<T> Update(T entity)
    {
        _movieShopDbContext.Entry(entity).State = EntityState.Modified;
        await _movieShopDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _movieShopDbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        return await _movieShopDbContext.Set<T>().FindAsync(id);
    }
}