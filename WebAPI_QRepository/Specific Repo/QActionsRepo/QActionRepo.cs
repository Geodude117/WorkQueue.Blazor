using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Model;
using Dapper;

namespace WebAPI_QRepository.Specific_Repo.QActionsRepo
{
    public class QActionRepo : Repository<QueueAction>, IQActionRepo
    {

        public QActionRepo(string connection) : base(connection)
        {}

        public async Task<IEnumerable<QueueAction>> GetActions(int QueueResultID)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<QueueAction>("[workqueue].[QueueResult_Search_Action_Code_By_QueueResultID]", new
                    {
                        QueueResultID
                    }, commandType: CommandType.StoredProcedure));
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override Task<QueueAction> GetAsync(int QueueItemID)
        {
            throw new NotImplementedException();
        }

        public override Task<int?> InsertAsync(QueueAction entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> UpdateAsync(QueueAction entity)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
