using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebAPI_QRepository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly string _connection;

        public Repository(string connection)
        {
            _connection = connection;
        }

        public IDbConnection Connection => new SqlConnection(_connection);

        public abstract Task<T> GetAsync(int QueueItemID);

        public abstract Task<int?> InsertAsync(T entity);

        public abstract Task<bool> UpdateAsync(T entity);

        public abstract Task<bool> DeleteAsync(int ID);
    }
}
