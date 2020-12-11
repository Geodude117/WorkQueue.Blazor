using System;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using DomainData.Models.DPABreachModels;

namespace WebAPI_QRepository.Specific_Repo.DPABreachStage1
{
    public class BreachStage1Repo : Repository<BreachStage1>,  IBreachStage1Repo
    {
        public BreachStage1Repo(string connection) : base(connection)
        {}

        public override async Task<bool> DeleteAsync(int QueueItemID)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[DPABreach].[StageOneDetails_Delete]", new
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

        public override async Task<BreachStage1> GetAsync(int QueueItemID)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<BreachStage1>("[DPABreach].[StageOneDetails_Select_By_QueueItemID]", new { QueueItemID }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<int?> InsertAsync(BreachStage1 entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[DPABreach].[StageOneDetails_Insert]", new
                        {
                            entity.Date,
                            entity.ReportedBy,
                            entity.DateReported,
                            entity.PersonResponsibleForBreach,
                            entity.DateOfBreach,
                            entity.CustomerOrEmployeeDataBreach,
                            entity.DMReference,
                            entity.ClientAffected,
                            entity.Site,
                            entity.NumberOfIndividualsAffected,
                            entity.AreaResponsible,
                            entity.BreachDescription,
                            entity.ActionAlreadyTaken,
                            entity.ResolutionOwner,
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

        public override async Task<bool> UpdateAsync(BreachStage1 entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[DPABreach].[StageOneDetails_Update_By_ID]", new
                        {
                            entity.Date,
                            entity.ReportedBy,
                            entity.DateReported,
                            entity.PersonResponsibleForBreach,
                            entity.DateOfBreach,
                            entity.CustomerOrEmployeeDataBreach,
                            entity.DMReference,
                            entity.ClientAffected,
                            entity.Site,
                            entity.NumberOfIndividualsAffected,
                            entity.AreaResponsible,
                            entity.BreachDescription,
                            entity.ActionAlreadyTaken,
                            entity.ResolutionOwner,
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
