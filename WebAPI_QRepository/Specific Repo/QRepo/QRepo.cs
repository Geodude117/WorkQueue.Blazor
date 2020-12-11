using CallBack_Model.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QRepository.Specific_Repo.QRepo
{
    public class QRepo : Repository<QueueModel>, IQRepo
    {

        public QRepo(string connection) : base(connection)
        {
        }

        public async Task<IEnumerable<QueueModel>> GetAllAsync()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<QueueModel>("[workqueue].[Queue_Get]",
                        commandType: CommandType.StoredProcedure)).DefaultIfEmpty();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<QueueModel> GetAsync(int QId)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<QueueModel>("[workqueue].[Queue_Get]", new { QId }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public override Task<int?> InsertAsync(QueueModel entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> UpdateAsync(QueueModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
