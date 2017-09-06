using HumanArc.Compliance.Data.Database;
using HumanArc.Compliance.Data.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HumanArc.Compliance.Shared.Entities;

namespace HumanArc.Compliance.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T> 
        where T : EntityBase
    {
        private ComplianceContext _dbContext;
        private readonly IDbSet<T> _dbSet;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbSet = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory { get; }

        protected ComplianceContext DataContext
        {
            get { return _dbContext ?? (_dbContext = DatabaseFactory.Get()); }
        }

        public IList<T> GetAll()
        {
            return _dbSet.Where(x => !x.Deleted).ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            entity.Deleted = true;
            _dbContext.SaveChanges();
        }
    }
}
