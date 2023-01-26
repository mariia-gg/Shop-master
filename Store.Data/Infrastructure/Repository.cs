using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;

namespace Store.Data.Infrastructure;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly StoreContext _context;
    private readonly DbSet<TEntity> _dbEntities;


    public Repository(
        StoreContext context
    )
    {
        _context = context;
        _dbEntities = _context.Set<TEntity>();
    }

    /// <summary>
    ///     Gets all entity records with included entities.
    /// </summary>
    /// <param name="includes">Included entities.</param>
    /// <returns>IQueryable of all entity records with included entities, if includes is null this function is equal GetAll.</returns>
    public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
    {
        var dbSet = _context.Set<TEntity>();

        var query = includes
            .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(
                dbSet,
                (current, include) => current.Include(include)
            );

        return query ?? dbSet;
    }

    /// <summary>
    ///     Gets entity by the keys.
    /// </summary>
    /// <param name="keys">Keys for the search.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Entity with such keys.</returns>
    public ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken = default, params Guid[] keys) =>
        _dbEntities.FindAsync(keys, cancellationToken);

    /// <summary>
    ///     Async add entity into DBContext.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <exception cref="ArgumentNullException">The entity to add cannot be <see langword="null" />.</exception>
    /// <returns>Added entity.</returns>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        CheckEntityForNull(entity);

        entity.CreatedAt = DateTime.UtcNow;

        return (await _dbEntities.AddAsync(entity, cancellationToken)).Entity;
    }

    /// <summary>
    ///     Adds a range of entities.
    /// </summary>
    /// <param name="entities">Entities to add.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var entitiesList = entities.ToList();

        return _dbEntities.AddRangeAsync(entitiesList, cancellationToken);
    }

    /// <summary>
    ///     Updates entity asynchronously.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Awaitable task with updated entity.</returns>
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        Task.Run(() => _dbEntities.Update(entity).Entity, cancellationToken);

    /// <summary>
    ///     Updates entities asynchronously.
    /// </summary>
    /// <param name="entities">Entities to update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Awaitable task with updated entity.</returns>
    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
        Task.Run(() =>
        {
            var entitiesList = entities.ToList();

            _dbEntities.UpdateRange(entitiesList);
        }, cancellationToken);

    /// <summary>
    ///     Deletes range.
    /// </summary>
    /// <param name="entities">Entities to delete.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
        Task.Run(() => { entities.ToList().ForEach(item => _context.Entry(item).State = EntityState.Deleted); },
            cancellationToken);

    /// <summary>
    ///     Saves changes in the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction.RollbackAsync(cancellationToken);
            }

            throw;
        }
    }

    /// <summary>
    ///     Removes entity from DBContext.
    /// </summary>
    /// <param name="entity">Entity to delete.</param>
    /// <returns>Task.</returns>
    public void Delete(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
    }

    /// <summary>
    ///     Detaches entity.
    /// </summary>
    /// <param name="entity">Entity to detach.</param>
    /// <returns>Task.</returns>
    public void Detach(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Detached;
    }

    /// <summary>
    ///     Gets all entity records with included entities as no tracking list.
    /// </summary>
    /// <param name="includes">Included entities.</param>
    /// <returns>IQueryable of all entity records with included entities, if includes is null this function is equal GetAll.</returns>
    public IQueryable<TEntity> NoTrackingQuery(params Expression<Func<TEntity, object>>[] includes)
    {
        var dbSet = _context.Set<TEntity>();

        var query = includes
            .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(
                dbSet,
                (current, include) => current.Include(include)
            );

        return (query ?? dbSet).AsNoTracking();
    }

    private static void CheckEntityForNull(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "The entity to add cannot be null.");
        }
    }
}