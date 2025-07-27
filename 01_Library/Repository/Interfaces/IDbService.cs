using Infrastructure.Entities;
using System.Data.Entity;

namespace Repository.Interfaces
{
    /// <summary>
    /// Database service interface for managing database operations. 
    /// Keeps track of Entity Framework DbContext operations.
    /// Allows for setting entity states, saving changes, and refreshing entities.
    /// </summary>
    public interface IDbService
    {
        /// <summary>
        /// Checks if there are any changes that can be saved to the database.
        /// </summary>
        /// <returns></returns>
        bool CanSaveChanges();

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Sets the state of the specified entity in the context.
        /// </summary>
        /// <remarks>Use this method to explicitly set the state of an entity, such as marking it as
        /// added, modified, or deleted. This is typically used in scenarios where the default state tracking behavior
        /// needs to be overridden.</remarks>
        /// <typeparam name="TEntity">The type of the entity. Must inherit from <see cref="Entity"/>.</typeparam>
        /// <param name="entity">The entity whose state is being set. Cannot be <see langword="null"/>.</param>
        /// <param name="entityState">The new state to assign to the entity. Must be a valid <see cref="EntityState"/> value.</param>
        void SetState<TEntity>(TEntity entity, EntityState entityState) where TEntity : Entity;


        //DbSet<TEntity> GetDbSet<TEntity>() where TEntity : Entity;
        
        /// <summary>
        /// Retrieves a <see cref="DbSet{TEntity}"/> instance for the specified entity type.
        /// </summary>
        /// <remarks>This method is typically used to access the entity set for a specific type in the
        /// context. The returned <see cref="DbSet{TEntity}"/> provides functionality for querying and interacting with
        /// the database.</remarks>
        /// <typeparam name="TEntity">The type of the entity for which the <see cref="DbSet{TEntity}"/> is requested. Must be a reference type.</typeparam>
        /// <returns>A <see cref="DbSet{TEntity}"/> instance that can be used to query and save instances of <typeparamref
        /// name="TEntity"/>.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Refreshes the state of the specified entity by synchronizing it with the latest data.
        /// </summary>
        /// <remarks>This method updates the provided entity to reflect its current state in the
        /// underlying data source. If the entity has been modified externally, those changes will be reflected after
        /// the refresh.</remarks>
        /// <param name="entity">The entity to refresh. Must not be <see langword="null"/>.</param>
        void Refresh(Entity entity);

        /// <summary>
        /// Refreshes the current state of the object, updating any cached or stale data.
        /// </summary>
        /// <remarks>This method ensures that the object's state is synchronized with its source or
        /// underlying data. Call this method when you need to ensure the object reflects the latest changes.</remarks>
        void Refresh();

        /// <summary>
        /// Resets the current context to its default state.
        /// </summary>
        /// <remarks>This method clears any existing state or data associated with the current context. It
        /// is typically used to reinitialize the context before starting a new operation or workflow.</remarks>
        void ResetContext();

        //DbEntityEntry<T> Entry<T>(T entity) where T:Entity;

        /// <summary>
        /// Removes the specified entity from the context, marking it for deletion.
        /// </summary>
        /// <remarks>After calling this method, the entity is marked for deletion and will be removed from
        /// the database when <c>SaveChanges</c> is called. If the entity is not tracked by the context, this method has
        /// no effect.</remarks>
        /// <typeparam name="TEntity">The type of the entity to be removed. Must inherit from <see cref="Entity"/>.</typeparam>
        /// <param name="entity">The entity to remove. Cannot be <see langword="null"/>.</param>
        void Remove<TEntity>(TEntity entity) where TEntity : Entity;
    }
}
