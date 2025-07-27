using Repository.Entities;
using System.Data.Entity;

namespace Repository.Interfaces
{
    public interface IDbService
    {
        bool CanSaveChanges();
        void SaveChanges();
        void SetState<TEntity>(TEntity entity, EntityState entityState) where TEntity : Entity;
        //DbSet<TEntity> GetDbSet<TEntity>() where TEntity : Entity;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Refresh(Entity entity);
        void Refresh();
        void ResetContext();
        //DbEntityEntry<T> Entry<T>(T entity) where T:Entity;
        void Remove<TEntity>(TEntity entity) where TEntity : Entity;
    }
}
