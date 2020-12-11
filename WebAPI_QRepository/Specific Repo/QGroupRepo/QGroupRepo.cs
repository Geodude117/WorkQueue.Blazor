using CallBack_Model.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QRepository.Specific_Repo.QGroup
{
    public class QGroupRepo : Repository<QueueGroup>, IQGroupRepo
    {
        public QGroupRepo(string connection) : base(connection)
        {}

        public override Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public override Task<QueueGroup> GetAsync(int QueueItemID)
        {
            throw new NotImplementedException();
        }

        public override Task<int?> InsertAsync(QueueGroup entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> UpdateAsync(QueueGroup entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<QueueGroup>> GetAllAsync()
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<QueueGroup>("[workqueue].[QueueGroup_Select_All]", 
                        commandType: CommandType.StoredProcedure)).DefaultIfEmpty();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
