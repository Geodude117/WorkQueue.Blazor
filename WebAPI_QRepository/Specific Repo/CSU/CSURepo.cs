using CallBack_Model.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WebAPI_QRepository.Specific_Repo.CSU
{
    public class CSURepo : Repository<CSU_Callback>, ICSURepo
    {
        public CSURepo(string connection) : base(connection)
        {}

        public async override Task<bool> DeleteAsync(int QueueItemID)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[callback].[CallbackDetails_Delete]", new
                        {
                            QueueItemID
                        }, commandType: CommandType.StoredProcedure, transaction: transactionopen));
                        transactionopen.Commit();

                        return result > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }

        public async override Task<CSU_Callback> GetAsync(int QueueItemID)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<CSU_Callback>("[callback].[CallbackDetail_Select_By_QueueItemID]", new { QueueItemID }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (SqlException ex)

            {
                throw ex;
            }
        }

        public async override Task<int?> InsertAsync(CSU_Callback entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                       var result = (await transactionopen.Connection.ExecuteAsync("[callback].[CallbackDetail_Insert]", new {
                                entity.WescotRef,
                                entity.DateForCallback,
                                NameOfCaller = entity.NameOfcaller,
                                entity.Relationship,
                                entity.ContactNumber,
                                entity.TimeToAvoid,
                                entity.ReasonForCallback,
                                entity.ReasonForTransfer,
                                entity.HealthIssue,
                                QueueItemID = entity.QueueItemID.Value
                            }, commandType: CommandType.StoredProcedure, transaction: transactionopen));
                        transactionopen.Commit();

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }

        public async override Task<bool> UpdateAsync(CSU_Callback entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[callback].[CallbackDetail_Update_By_ID]", new
                               {
                                   entity.ID,
                                   entity.WescotRef,
                                   entity.DateForCallback,
                                   NameOfCaller = entity.NameOfcaller,
                                   entity.Relationship,
                                   entity.ContactNumber,
                                   entity.TimeToAvoid,
                                   entity.ReasonForCallback,
                                   entity.ReasonForTransfer,
                                   entity.HealthIssue,
                               }, commandType: CommandType.StoredProcedure, transaction: transactionopen))
                               != 0;
                        transactionopen.Commit();
                        return result;
                    }
                }
            }
            catch (SqlException ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
            
        }
    }
}
