using System.Collections.Generic;

namespace HumanArc.Compliance.Data.Interfaces
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
