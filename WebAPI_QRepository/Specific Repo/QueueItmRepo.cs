using System;
using CallBack_Model.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_QRepository.Specific_Repo
{
    public class QueueItmRepo : Repository<QueueItem>, IQueueItmRepo
    {
        public QueueItmRepo(string connection) : base(connection)
        {}

        public async override Task<QueueItem> GetAsync(int QueueItemID)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<QueueItem>("[workqueue].[QueueItem_Select_ById]",
                        new { QueueItemID }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

         }

        public async override Task<int?> InsertAsync(QueueItem entity)
        {
            IDbTransaction transactionopen = null;
            var parameters = new DynamicParameters();
            parameters.Add("@QueueItemID", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            parameters.Add("@WescotRef", value: entity.WescotRef , dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@CustomerName", value: entity.CustomerName, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@CompletedDate", value: entity.CompletedDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            parameters.Add("@CompletedBy", value: entity.CompletedBy, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@DueDate", value: entity.DueDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            parameters.Add("@CreatedDate", value: entity.CreatedDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            parameters.Add("@CreatedBy", value: entity.CreatedBy, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@Summary", value: entity.Summary, dbType: DbType.String, direction: ParameterDirection.Input);
            parameters.Add("@ParentQueueItemID", value: entity.ParentQueueItemID, dbType: DbType.Int32, direction: ParameterDirection.Input);
            parameters.Add("@QueueID", value: entity.QueueID, dbType: DbType.Int32, direction: ParameterDirection.Input);

            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[workqueue].[QueueItem_Insert]",
                            parameters, 
                            commandType: CommandType.StoredProcedure, 
                            transaction: transactionopen));
                        transactionopen.Commit();

                        return parameters.Get<int>("@QueueItemID");
;
                    }
                }
            }
            catch (Exception ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }

        public async override Task<bool> UpdateAsync(QueueItem entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[workqueue].[QueueItem_Update_By_ID]", new
                        {
                            entity.QueueItemID,
                            entity.WescotRef,
                            entity.CustomerName,
                            entity.CompletedDate,
                            entity.CompletedBy,
                            entity.DueDate,
                            entity.CreatedDate,
                            entity.CreatedBy,
                            entity.Summary,
                            entity.ParentQueueItemID,
                            entity.LockTime,
                            entity.LockedBy
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
                        var result = (await transactionopen.Connection.ExecuteAsync("[workqueue].[QueueItem_Delete]", new
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


        public async Task<IEnumerable<QueueItem>> GetByGroupAsync(int QueueGroupID)
        {
            try
            {
                Dapper.DynamicParameters parameters = new DynamicParameters();
                parameters.Add("queueGroupId", QueueGroupID);
                using (IDbConnection connection = Connection)
                {
                    return await connection.QueryAsync<QueueItem, RagRules, QueueItem>(
                        "[workqueue].[QueueItem_Select_ActiveByGroupId]", (queueItem, ragRules) =>
                        {
                            queueItem.RagSet = ragRules;
                            return queueItem;
                        },
                        splitOn: "RAGRuleID",
                        commandType: CommandType.StoredProcedure,
                        param: parameters);
                }                              
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<QueueItem>> GetSearchAsync(SearchParameters search)
        {
            try
            {
                Dapper.DynamicParameters parameters = new DynamicParameters();
                parameters.Add("StartDate", search.StartDate);
                parameters.Add("EndDate", search.EndDate);
                parameters.Add("WescotRef", search.WescotRef);
                parameters.Add("RaiseAgentId", search.Raise_AgentId);
                parameters.Add("ActionAgentId", search.Action_AgentId);
                parameters.Add("QueueGroupID", search.QueueGroup);
                parameters.Add("QueueItemID", search.QueueItemID);
                parameters.Add("IsActive", search.IsActive);

                using (IDbConnection connection = Connection)
                {
                    return await connection.QueryAsync<QueueItem, RagRules, QueueItem>("[workqueue].[QueueItem_Select_Search]", 
                        (queueItem, ragRules) =>
                        {
                            queueItem.RagSet = ragRules;
                            return queueItem;
                        },
                        splitOn: "RAGRuleID",
                        commandType: CommandType.StoredProcedure,
                        param: parameters);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
