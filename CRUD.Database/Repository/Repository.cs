using CRUD.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using CRUD.Database.Models;

namespace CRUD.Database.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly TESTContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(TESTContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.Count(filter);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return GetQuery(filter).ToList();
        }

        public IEnumerable<TEntity> GetQuery(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.Where(filter);
        }


        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return Get(filter).FirstOrDefault();
        }
        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return Get(filter).Single();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetQueryAll().ToList();
        }

        public IQueryable<TEntity> GetQueryAll()
        {
            return _entities;
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _context.Set<TEntity>().Attach(entity);
            }
            catch (Exception e)
            {
                _context.Entry(entity).State = EntityState.Detached;
                _context.Set<TEntity>().Attach(entity);
            }

            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> filter)
        {
            foreach (var entity in Get(filter))
            {
                Delete(entity);
            }
        }

        //public RawSqlRepository GetParsedOrDefaultValue(string sql)
        //{
        //return new RawSqlRepository(_context, sql);
        //}
    }
}
