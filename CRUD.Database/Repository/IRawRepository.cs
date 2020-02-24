using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CRUD.Database.Repository
{
    public interface IRawRepository
    {
        int Count(params SqlParameter[] parm);
        List<TEntity> Get<TEntity>(params SqlParameter[] parm) where TEntity : class, new();
        List<TEntity> GetWithoutParam<TEntity>() where TEntity : class, new();
        TEntity GetSingle<TEntity>(params SqlParameter[] parm) where TEntity : class, new();
        List<TEntity> CallSP<TEntity>(params SqlParameter[] parm) where TEntity : class, new();
        int CallSPW(params SqlParameter[] parm);
    }
}
