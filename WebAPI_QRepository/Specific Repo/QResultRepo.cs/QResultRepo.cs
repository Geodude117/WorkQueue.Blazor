using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Model;
using Dapper;
using WebAPI_QRepository.Specific_Repo.QResultRepo.cs;

namespace WebAPI_QRepository.Specific_Repo.QResultBusiness.cs
{
    public class QResultRepo : Repository<QResult>, IQResultRepo
    {
        public QResultRepo(string connection) : base(connection)
        {
        }

        public async Task<IEnumerable<QResult>> GetAllAsync(int QueueID)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<QResult>("[workqueue].[QueueResult_Select_QueueResult_ByQueueID]", 
                        new { QueueID }, commandType: CommandType.StoredProcedure)).DefaultIfEmpty();
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

        public override Task<QResult> GetAsync(int QueueItemID)
        {
            throw new NotImplementedException();
        }

        public override Task<int?> InsertAsync(QResult entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> UpdateAsync(QResult entity)
        {
            throw new NotImplementedException();
        }
    }
}
