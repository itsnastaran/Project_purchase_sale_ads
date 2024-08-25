using Microsoft.EntityFrameworkCore;
using Projectcore.Data;
using System.Linq.Expressions;

namespace Projectcore.Models.Repository
{
    public class BaseRepository<TEntity>:IBaseRepository<TEntity> where TEntity:class
    {
        internal ApplicationDbContext _context;
        internal DbSet<TEntity> dbset;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbset;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetBYID(object id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<TEntity> GetByList()
        {
            return dbset.ToList();
        }
        public virtual TEntity Insert_ReturnEntity(TEntity entity, bool _save = false)
        {
            dbset.Add(entity);
            if (_save)
            {
                _context.SaveChanges();
            }
            return entity;
        }
        public virtual void Insert(TEntity entity)
        {
            dbset.Add(entity);
        }
        public virtual TEntity Delete_ReturnEntity(TEntity entity)
        {
            dbset.Attach(entity);
            return dbset.Remove(entity).Entity;
        }
        public virtual void Delete(TEntity entity)
        {
            dbset.Attach(entity);
            dbset.Remove(entity);
        }
        public virtual void Delete(object id)
        {
            TEntity entity = GetBYID(id);
            dbset.Attach(entity);
            dbset.Remove(entity);
        }
        public virtual TEntity Delete_ReturnEntity(object id)
        {
            TEntity entity = GetBYID(id);
            dbset.Attach(entity);
            return dbset.Remove(entity).Entity;
        }
        public virtual TEntity Update_ReturnEntity(TEntity entity)
        {
            TEntity entity1 = dbset.Attach(entity).Entity;
            _context.Entry(entity).State = EntityState.Modified;
            return entity1;
        }
        public virtual void Update(TEntity entity)
        {
            TEntity entity1 = dbset.Attach(entity).Entity;
            _context.Entry(entity).State = EntityState.Modified;

        }
       
        public void DeleteRange(IEnumerable<TEntity> entities, bool _save = false)
        {
            dbset.RemoveRange(entities);
            if (_save)
            {
                _context.SaveChanges();
            }
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool _save = false)
        {
            foreach (var item in entities)
            {
                dbset.Attach(item);
            }
            if (_save)
            {
                _context.SaveChanges();
            }
        }
        public virtual void InsertRange(IEnumerable<TEntity> entities, bool _save = false)
        {
            dbset.AddRange(entities);
            if (_save)
            {
                _context.SaveChanges();
            }
        }
    }
}
