using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Gdpr_Interfaces;

namespace WebAPI_QRepository.Specific_Repo.GDPRepo
{
    public class GdprRepo: Repository<GdprSearchModel>, IGdprRepo
    {
        public GdprRepo(string connection) : base(connection)
        { }

        /// <summary>
        /// Uses WescotRef to delete items can delete multiple items.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override async Task<bool> DeleteAsync(int ID)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[workqueue].[ForgetMe]", new
                        {
                            WescotRef = ID
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

        public override Task<GdprSearchModel> GetAsync(int QueueItemID)
        {
            throw new NotImplementedException();
        }

        public override Task<int?> InsertAsync(GdprSearchModel entity)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> UpdateAsync(GdprSearchModel entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using (IDbConnection connection = Connection)
                {
                    connection.Open();
                    using (transactionopen = connection.BeginTransaction())
                    {
                        var result = (await transactionopen.Connection.ExecuteAsync("[workqueue].[GdprAnonymiseMe]", new
                                     {
                                         entity.WescotRef
                                     }, commandType: CommandType.StoredProcedure, transaction: transactionopen))
                                     > 0;
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
