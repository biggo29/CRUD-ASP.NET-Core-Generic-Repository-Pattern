using CRUD.Database.Context;
using CRUD.Database.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Database.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TESTContext _context;
        private bool _disposed;
        private readonly Hashtable _repositories;

        public UnitOfWork(IOptions<SqlOption> connectionString)
        {
            _repositories = new Hashtable();
            _context = new TESTContext(connectionString);
        }

        //Save to DB
        public void Save()
        {
            _context.SaveChanges();
        }

        //Repository
        public IRawRepository Repository(string sql)
        {
            return new RawRepository(_context, sql);
        }

        //Repository calling
        public IRepository<TData> Repository<TData>() where TData : class
        {
            var type = typeof(TData).Name;

            if (!_repositories.ContainsKey(type))
            {
                _repositories.Add(type, new Repository<TData>(_context));
            }

            return (IRepository<TData>)_repositories[type];
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
