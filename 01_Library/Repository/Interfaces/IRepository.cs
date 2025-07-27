using Repository.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : Entity, new()
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Save(IEnumerable<T> models);
        void Save(T model);
        void Delete(T model);
        void Delete(IEnumerable<T> models);
        void MarkAsDelete(T model);
        void MarkAsDelete(IEnumerable<T> models);
        void ClearDirtyFlag(T model);
        void ClearDirtyFlag(IEnumerable<T> models);
        void Refresh(int id);
        void ResetContext();
    }
}
