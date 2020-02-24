using CRUD.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CRUD.Database.Repository
{
    public class RawRepository : IRawRepository, IDisposable
    {
        private readonly TESTContext _context;
        private readonly string _sql;
        private readonly DbConnection _connection;

        private bool _disposed;

        //Openning connection using Dependency injection
        public RawRepository(TESTContext context, string sql)
        {
            _context = context;
            _sql = sql;
            _connection = _context.Database.GetDbConnection();
            if(_connection.State!= ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        //counter of an Entity
        public int Count(params SqlParameter[] parm)
        {
            int count = 1;
            //using (var connection = _context.Database.GetDbConnection())
            //{
            //connection.Open();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                command.Parameters.AddRange(parm);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    count = reader.GetInt16(0);
                }
                //connection close
                reader.Close();
            }
            return count;
        }

        //Convert Value
        private T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        //reader
        private TEntity reader<TEntity>(DbDataReader reder) where TEntity : class, new()
        {
            var obj = new TEntity();
            Type t = obj.GetType();
            foreach (PropertyInfo propInfo in t.GetProperties())
            {
                if (propInfo.PropertyType.IsClass)
                {
                    object propVal = propInfo.GetValue(obj, null);
                    //setValsRecursive(propVal, value);
                }
                if (Enumerable.Range(0, reder.FieldCount).Any(i => string.Equals(reder.GetName(i), propInfo.Name, StringComparison.OrdinalIgnoreCase)) && reder[propInfo.Name] != DBNull.Value)
                {
                    propInfo.SetValue(obj, reder[propInfo.Name], null);
                }

            }
            return obj;
        }

        //Get Data with parameters
        public List<TEntity> Get<TEntity>(params SqlParameter[] parm) where TEntity : class, new()
        {
            List<TEntity> data = new List<TEntity>();
            Type obj = new TEntity().GetType();
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                command.Parameters.AddRange(parm);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    data.Add(reader<TEntity>(reader));
                }
                //connection close
                reader.Close();
            }

            return data;
        }

        //Get Data without param
        public List<TEntity> GetWithoutParam<TEntity>() where TEntity : class, new()
        {
            List<TEntity> data = new List<TEntity>();
            Type obj = new TEntity().GetType();
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    data.Add(reader<TEntity>(reader));
                }
                //close connection
                reader.Close();
            }
            return data;
        }

        //Get Single Data
        public TEntity GetSingle<TEntity>(params SqlParameter[] parm) where TEntity : class, new()
        {
            int rowCount = 0;
            TEntity data = new TEntity();
            Type obj = new TEntity().GetType();
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    if(rowCount > 0)
                    {
                        throw new Exception();
                    }
                    data = (reader<TEntity>(reader));
                    rowCount++;
                }
                //close conneciton
                reader.Close();
            }
            return data;
        }

        //Fetch list of data calling from SP
        public List<TEntity> CallSP<TEntity>(params SqlParameter[] parm) where TEntity : class, new()
        {
            List<TEntity> data = new List<TEntity>();
            Type obj = new TEntity().GetType();
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                command.Parameters.AddRange(parm);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    data.Add(reader<TEntity>(reader));
                }
                //close connection
                reader.Close();
            }
            return data;
        }

        //SP check
        public int CallSPW(params SqlParameter[] parm)
        {
            using(var command = _connection.CreateCommand())
            {
                command.CommandText = _sql;
                command.CommandType = CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    var res = reader["result"];
                }
                //close connection
                reader.Close();
            }
            return 1;
        }

        //dispose
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
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
            _disposed = true;
        }

        private new List<SqlParameter> convert(params SqlParameter[] sqlParameters)
        {
            List<SqlParameter> _sqlParameters = new List<SqlParameter>();
            foreach(SqlParameter sqlParameter in sqlParameters)
            {
                _sqlParameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
            }
            return _sqlParameters;
        }

        private SqlDataReader ExecuteSqlCommand(SqlParameter parm)
        {
            using(var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using(var command = _connection.CreateCommand())
                {
                    command.CommandText = _sql;
                    return (SqlDataReader)command.ExecuteReader();
                }
            }
        }
    }
}
