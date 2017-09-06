using System.Collections.Generic;

namespace HumanArc.Compliance.Shared.Interfaces
{
    public interface IEntityService<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
        T Save(T entity);
    }
}
