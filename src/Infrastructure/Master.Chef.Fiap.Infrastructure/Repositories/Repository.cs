using Master.Chef.Fiap.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Master.Chef.Fiap.Infrastructure.Contexts;
using Master.Chef.Fiap.Infrastructure.Repositories.Contracts;

namespace Master.Chef.Fiap.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot, new()
{
    private readonly MasterChefApiDbContext _context;
    
    public Repository(MasterChefApiDbContext context)
    {
        _context = context;
    }
    
    public void Dispose()
    {
        _context?.Dispose();
    }

    public async Task<TEntity> Add(TEntity entity, bool saveChanges = true)
    {
        var entityEntry = _context.Set<TEntity>().Add(entity);
            
        if(saveChanges)
            await SaveChanges();

        return entityEntry.Entity;
    }

    public async Task AddList(IEnumerable<TEntity> entities, bool saveChanges = true)
    {
        _context.Set<TEntity>().AddRange(entities);
            
        if(saveChanges)
            await SaveChanges();
    }

    public async Task<TEntity?> FindByIdOrDefault(Guid id, bool asNoTracking = false)
    {
        if (asNoTracking) return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<IEnumerable<TEntity>> GetAll(bool asNoTracking = false)
    {
        if (asNoTracking) return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await SaveChanges();
    }

    public async Task UpdateList(List<TEntity> listEntities)
    {
        _context.Set<TEntity>().UpdateRange(listEntities);
        await SaveChanges();
    }

    public async Task Delete(TEntity entity, bool saveChanges = true)
    {
        _context.Remove(entity);

        if (saveChanges)
            await SaveChanges();
    }

    public async Task DeleteRange(List<TEntity> entities, bool saveChanges = true)
    {
        _context.RemoveRange(entities);

        if (saveChanges)
            await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}