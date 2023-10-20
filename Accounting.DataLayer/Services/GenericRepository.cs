using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DataLayer.Services
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private Accounting_DBEntities _db;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(Accounting_DBEntities db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public virtual void insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual IEnumerable<TEntity> get(Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (where!= null)
            {
                query = query.Where(where);
            }
            return query.ToList();
        }

        public virtual TEntity getById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void delete(TEntity entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void delete(object id)
        {

            var entity = getById(id);
            delete(entity);
        }
    }
}
