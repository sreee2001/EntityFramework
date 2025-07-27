using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;

namespace Repository.Services
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        #region Protected Members

        [Import]
        protected IDbService DbService { get; set; }

 
        private DbSet<T> ModelDbSet => DbService.Set<T>();

        protected Action PostSaveAction = () => { };
        protected Action PostSaveAllAction = () => { };

        #endregion
        public T GetById(int id)
        {
            return ModelDbSet.Select(b => b).FirstOrDefault(b => b.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return ModelDbSet;
        }

        public void Save(T model)
        {
            model.SetAuditInfo();
            if (model.Id == 0)
                ModelDbSet.Add(model);
            Commit();
            ClearDirtyFlag(model);
            PostSaveAction?.Invoke();
        }

        public void Save(IEnumerable<T> models)
        {
            foreach (T model in models)
                model.SetAuditInfo();
            ModelDbSet.AddRange(models.Where(b => b.Id == 0));
            Commit();
            ClearDirtyFlag(models);
            PostSaveAllAction?.Invoke();
        }


        private void Commit()
        {
            DbService.SaveChanges();
        }

        public abstract void ClearDirtyFlag(T model);

        public void ClearDirtyFlag(IEnumerable<T> models)
        {
            foreach (T model in models)
                ClearDirtyFlag(model);
        }

        public void Delete(T model)
        {
            MarkAsDelete(model);
            Commit();
        }

        public void Delete(IEnumerable<T> models)
        {
            MarkAsDelete(models);
            Commit();
        }

        public abstract void MarkAsDelete(T model);

        protected void MarkEntityAsDeleted<TEntity>(TEntity entity) where TEntity : Entity
        {
            DbService.Remove(entity);
        }

        public void MarkAsDelete(IEnumerable<T> models)
        {
            List<T> list = models.ToList();
            foreach (T model in list)
            {
                MarkAsDelete(model);
            }
        }

        public virtual void Refresh(int id)
        {
            if (id == 0)
                return;
            T entity = GetById(id);
            DbService.Refresh(entity);
            entity.IsDirty = false;
        }

        public void ResetContext()
        {
            DbService.ResetContext();
        }
    }
}
