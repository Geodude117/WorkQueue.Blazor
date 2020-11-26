using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DomainData.Repository.Repository
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

        public abstract Task<int?> Insert(T entity);

        public abstract Task<bool> UpdateAsync(T entity);

        public abstract Task<bool> DeleteAsync(int ID);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<IEnumerable<T>> GetAllByGroupIdAsync(int GroupId);


    }
}
