using Master.Chef.Fiap.Domain.Contracts;
using Master.Chef.Fiap.Domain.Entities;

namespace Master.Chef.Fiap.Infrastructure.Repositories.Contracts;

public interface IRepository<TEntity> : IDisposable where TEntity : AggregateRoot
{
    Task<TEntity> Add(TEntity entity, bool saveChanges = true);
    Task AddList(IEnumerable<TEntity> entities, bool saveChanges = true);
    Task<TEntity?> FindByIdOrDefault(Guid id, bool asNoTracking = false);
    Task<IEnumerable<TEntity>> GetAll(bool asNoTracking = false);
    Task Update(TEntity entity);
    Task UpdateList(List<TEntity> listEntities);
    Task Delete(TEntity entity, bool saveChanges = true);
    Task DeleteRange(List<TEntity> entities, bool saveChanges = true);
    Task<int> SaveChanges();
}