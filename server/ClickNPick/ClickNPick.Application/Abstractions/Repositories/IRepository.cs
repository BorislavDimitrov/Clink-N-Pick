namespace ClickNPick.Application.Abstractions.Repositories;

public interface IRepository<TEntity> 
    where TEntity : class
{
    IQueryable<TEntity> All();

    IQueryable<TEntity> AllAsNoTracking();

    IQueryable<TEntity> AllWithDeleted();

    IQueryable<TEntity> AllAsNoTrackingWithDeleted();

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    void SoftDelete(TEntity entity);

     void Restore(TEntity entity);

    Task<int> SaveChangesAsync();
}
