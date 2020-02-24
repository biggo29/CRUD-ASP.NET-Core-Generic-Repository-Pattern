using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRUD.Database.Repository
{
    public interface IRepository<TEntity>
    {
        int Count(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter);

        TEntity GetSingle(Expression<Func<TEntity, bool>> filter);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetQueryAll();

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> filter);

        //RawSqlRepository GetParsedOrDefaultValue(string sql);
    }
}
