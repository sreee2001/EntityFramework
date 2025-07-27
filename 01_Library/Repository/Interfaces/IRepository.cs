using Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for managing entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>This interface provides methods for querying, saving, deleting, and managing entities, as
    /// well as operations for refreshing and resetting the repository context. It is designed to abstract the
    /// persistence layer, allowing for flexible implementations such as database-backed repositories or in-memory
    /// storage.</remarks>
    /// <typeparam name="T">The type of entity managed by the repository. Must inherit from <see cref="Entity"/> and have a parameterless
    /// constructor.</typeparam>
    public interface IRepository<T> where T : Entity, new()
    {
        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> as an <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <remarks>This method returns a queryable collection of entities, allowing for deferred
        /// execution and  LINQ-based querying. The caller can apply filters, projections, and other query operations 
        /// before materializing the results.</remarks>
        /// <returns>An <see cref="IQueryable{T}"/> representing the collection of all entities of type <typeparamref name="T"/>.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve. Must be a positive integer.</param>
        /// <returns>The entity of type <typeparamref name="T"/> that matches the specified identifier. Returns <see
        /// langword="null"/> if no entity with the given identifier is found.</returns>
        T GetById(int id);

        /// <summary>
        /// Saves the specified collection of models to the underlying data store.
        /// </summary>
        /// <remarks>Each model in the collection is processed and persisted. Ensure that the models meet
        /// any  required validation criteria before calling this method. If the collection is empty, no  operation is
        /// performed.</remarks>
        /// <param name="models">The collection of models to be saved. Cannot be null or empty.</param>
        void Save(IEnumerable<T> models);

        /// <summary>
        /// Saves the specified model to the underlying data store.
        /// </summary>
        /// <remarks>This method persists the provided model to the data store.  Ensure that the model
        /// contains valid data before calling this method.</remarks>
        /// <param name="model">The model to be saved. Cannot be null.</param>
        void Save(T model);

        /// <summary>
        /// Deletes the specified model from the underlying data source.
        /// </summary>
        /// <remarks>This method removes the provided model from the data source. Ensure that the model
        /// exists in the data source before calling this method to avoid unexpected behavior.</remarks>
        /// <param name="model">The model to delete. Cannot be null.</param>
        void Delete(T model);

        /// <summary>
        /// Deletes the specified collection of models from the data source.
        /// </summary>
        /// <remarks>This method removes the provided models from the underlying data source. If any model
        /// in the collection does not exist,  the behavior may vary depending on the implementation. Ensure that the
        /// collection is not null or empty to avoid unexpected results.</remarks>
        /// <param name="models">The collection of models to delete. Each model must exist in the data source.</param>
        void Delete(IEnumerable<T> models);

        /// <summary>
        /// Marks the specified model as deleted.
        /// </summary>
        /// <remarks>This method updates the state of the provided model to indicate it has been deleted.
        /// The exact behavior may depend on the implementation, such as flagging the model or preparing it for removal
        /// from a data store.</remarks>
        /// <param name="model">The model to mark as deleted. Cannot be <see langword="null"/>.</param>
        void MarkAsDelete(T model);

        /// <summary>
        /// Marks the specified collection of models as deleted.
        /// </summary>
        /// <remarks>This method updates the state of the provided models to indicate they are deleted.
        /// The caller is responsible for ensuring that the models are valid and properly initialized.</remarks>
        /// <param name="models">The collection of models to mark as deleted. Each model in the collection must not be null.</param>
        void MarkAsDelete(IEnumerable<T> models);

        /// <summary>
        /// Clears the dirty flag for the specified model, indicating that it no longer has unsaved changes.
        /// </summary>
        /// <remarks>This method is typically used to mark a model as clean after its changes have been
        /// saved or discarded. Ensure that the <paramref name="model"/> parameter is not null before calling this
        /// method.</remarks>
        /// <param name="model">The model whose dirty flag is to be cleared. Cannot be null.</param>
        void ClearDirtyFlag(T model);

        /// <summary>
        /// Clears the dirty flag for the specified collection of models, indicating that they no longer has unsaved changes.
        /// </summary>
        /// <remarks>This method is typically used to mark a collection of models as clean after their changes have been
        /// saved or discarded. Ensure that the <paramref name="models"/> parameter is not null before calling this
        /// method.</remarks>
        /// <param name="models">The collection of models to mark as deleted. Each model in the collection must not be null.</param>
        void ClearDirtyFlag(IEnumerable<T> models);

        /// <summary>
        /// Refreshes the state of the specified entity by synchronizing it with the latest data from the data source.
        /// </summary>
        /// <remarks>
        /// This method updates the provided entity to reflect its current state in the underlying data source.
        /// </remarks>
        /// <param name="id">The unique identifier of the entity to retrieve. Must be a positive integer.</param>
        void Refresh(int id);

        /// <summary>
        /// Resets the context of the repository, clearing any cached data and resetting the state of tracked entities.
        /// </summary>
        void ResetContext();
    }
}
