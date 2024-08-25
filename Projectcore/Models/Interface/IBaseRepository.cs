using System.Linq.Expressions;

namespace Projectcore.Models
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity GetBYID(object id);
        IEnumerable<TEntity> GetByList();
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties = "");
        TEntity Insert_ReturnEntity(TEntity entity, bool _save = false);
        void Insert(TEntity entity);
        TEntity Update_ReturnEntity(TEntity entity);
        TEntity Delete_ReturnEntity(TEntity entity);
        TEntity Delete_ReturnEntity(object id);
        void Delete(TEntity entity);
        void Delete(object id);
        void DeleteRange(IEnumerable<TEntity> entities, bool _save = false);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities, bool _save = false);
        void InsertRange(IEnumerable<TEntity> entities, bool _save = false);

    }
}
