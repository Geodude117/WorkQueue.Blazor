using Dapper;
using DPABreachModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QRepository.Specific_Repo.DPABreachStage2
{

    public class BreachStage2Repo : Repository<BreachStage2>, IBreachStage2Repo
    {
        public BreachStage2Repo(string connection) : base(connection)
        { }

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
                        var result = (await transactionopen.Connection.ExecuteAsync("[DPABreach].[StageTwoDetails_Delete]", new
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

        public override async Task<BreachStage2> GetAsync(int QueueItemID)
        {
            try
            {
                using (IDbConnection connection = Connection)
                {
                    return (await connection.QueryAsync<BreachStage2>("[DPABreach].[StageTwoDetails_Select_By_QueueItemID]", new { QueueItemID }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override async Task<int?> InsertAsync(BreachStage2 entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[DPABreach].[StageTwoDetails_Insert]", new
                        {
                            entity.HRDirectorAware,
                            entity.BreachDescription,
                            entity.ActionAlreadyTaken,
                            entity.AdditionalMitigatingActionRequired,
                            entity.NumberOfDataSubjectsAffected,
                            entity.IndividualsAware,
                            entity.CategoriesOfDataBreaches,
                            entity.PotentialConsequences,
                            entity.Type,
                            entity.RootCause,
                            entity.DPORecommendation,
                            entity.NonDPA,
                            entity.RiskRating,
                            entity.ICOReportable,
                            entity.ClientReportable,
                            entity.DataSubjectReportable,
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

        public override async Task<bool> UpdateAsync(BreachStage2 entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[DPABreach].[StageTwoDetails_Update_By_ID]", new
                        {
                            entity.Id,
                            entity.HRDirectorAware,
                            entity.BreachDescription,
                            entity.ActionAlreadyTaken,
                            entity.AdditionalMitigatingActionRequired,
                            entity.NumberOfDataSubjectsAffected,
                            entity.IndividualsAware,
                            entity.CategoriesOfDataBreaches,
                            entity.PotentialConsequences,
                            entity.Type,
                            entity.RootCause,
                            entity.DPORecommendation,
                            entity.NonDPA,
                            entity.RiskRating,
                            entity.ICOReportable,
                            entity.ClientReportable,
                            entity.DataSubjectReportable,
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

