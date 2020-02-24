using CRUD.Database.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();

        IRepository<TData> Repository<TData>() where TData : class;
        IRawRepository Repository(string sql);
    }
}
