using GatherApp.Contracts.Entities.Interfaces;

namespace GatherApp.Repositories
{
    /// <summary>
    /// Generic repository interface providing basic CRUD operations for entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the repository manages.</typeparam>
    /// <remarks>
    /// The entity type must implement the <see cref="IEntity{TKey}"/> interface with a string as the key type.
    /// </remarks>
    public interface IRepository<TEntity> where TEntity : class, IEntity<string>
    {
        /// <summary>
        /// Gets an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        TEntity Get(string id);

        /// <summary>
        /// Gets all entities of the specified type.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TEntity}"/> representing all entities of the specified type.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add to the database.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Soft deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to soft delete from the database.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Performs a hard delete on the entity, removing it permanently from the database.
        /// </summary>
        /// <param name="entity">The entity to hard delete from the database.</param>
        void HardDelete(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update in the database.</param>
        void Update(TEntity entity);
    }
}
