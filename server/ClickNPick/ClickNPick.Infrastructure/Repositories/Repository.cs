using ClickNPick.Application.Abstractions.Repositories;
using ClickNPick.Domain.Models.Common;
using ClickNPick.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClickNPick.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
 where TEntity : class, ISoftDeletableModel
{
    public Repository(ClickNPickDbContext context)
    {
        this.Context = context ?? throw new ArgumentNullException(nameof(context));
        this.DbSet = this.Context.Set<TEntity>();
    }

    protected DbSet<TEntity> DbSet { get; set; }

    protected ClickNPickDbContext Context { get; set; }

    public  IQueryable<TEntity> All() => this.DbSet.Where(x => !x.IsDeleted);

    public  IQueryable<TEntity> AllAsNoTracking() => this.DbSet.AsNoTracking().Where(x => !x.IsDeleted);

    public IQueryable<TEntity> AllWithDeleted() => All().IgnoreQueryFilters();

    public IQueryable<TEntity> AllAsNoTrackingWithDeleted() => AllAsNoTracking().IgnoreQueryFilters();

    public  Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

    public  void Update(TEntity entity)
    {
        var entry = this.Context.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            this.DbSet.Attach(entity);
        }

        entry.State = EntityState.Modified;
    }

    public  void Delete(TEntity entity) => this.DbSet.Remove(entity);

    public void SoftDelete(TEntity entity)
    {
        entity.IsDeleted = true;
        entity.DeletedOn = DateTime.UtcNow;
        this.Update(entity);
    }

    public void Restore(TEntity entity)
    {
        entity.IsDeleted = false;
        entity.DeletedOn = null;
        this.Update(entity);
    }

    public Task<int> SaveChangesAsync() => this.Context.SaveChangesAsync();
}
