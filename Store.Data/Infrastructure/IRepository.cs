using System.Linq.Expressions;
using Store.Data.Entities;

namespace Store.Data.Infrastructure;

public interface IRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    ///     Gets all entity records with included entities as no tracking list.
    /// </summary>
    /// <param name="includes">Included entities.</param>
    /// <returns>IQueryable of all entity records with included entities, if includes is null this function is equal GetAll.</returns>
    public IQueryable<TEntity> NoTrackingQuery(params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    ///     Gets all entity records with included entities.
    /// </summary>
    /// <param name="includes">Included entities.</param>
    /// <returns>IQueryable of all entity records with included entities, if includes is null this function is equal GetAll.</returns>
    public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    ///     Async add entity into DBContext.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <exception cref="ArgumentNullException">The entity to add cannot be <see langword="null" />.</exception>
    /// <returns>Added entity.</returns>
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds a range of entities.
    /// </summary>
    /// <param name="entities">Entities to add.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes range.
    /// </summary>
    /// <param name="entities">Entities to delete.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates entity asynchronously.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Awaitable task with updated entity.</returns>
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates entities asynchronously.
    /// </summary>
    /// <param name="entities">Entities to update.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Awaitable task with updated entity.</returns>
    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Saves changes in the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets entity by the keys.
    /// </summary>
    /// <param name="keys">Keys for the search.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Entity with such keys.</returns>
    public ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken = default, params Guid[] keys);

    /// <summary>
    ///     Removes entity from DBContext.
    /// </summary>
    /// <param name="entity">Entity to delete.</param>
    /// <returns>Task.</returns>
    public void Delete(TEntity entity);

    /// <summary>
    ///     Detaches entity.
    /// </summary>
    /// <param name="entity">Entity to detach.</param>
    /// <returns>Task.</returns>
    public void Detach(TEntity entity);
}