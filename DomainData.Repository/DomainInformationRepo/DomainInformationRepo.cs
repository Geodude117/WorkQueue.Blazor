using Dapper;
using DomainData.Models;
using DomainData.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainData.Repository.DomainInformationRepo
{
    public class DomainInformationRepo : Repository<DomainInformation>, IDomainInformationRepo
    {
        public DomainInformationRepo(string connection) : base(connection)
        { }

        public override Task<bool> DeleteAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<DomainInformation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async override Task<IEnumerable<DomainInformation>> GetAllByGroupIdAsync(int GroupId)
        {
            try
            {
                using IDbConnection connection = Connection;
                return (await connection.QueryAsync<DomainInformation>("[dbo].[DomainInformation_Select_By_GroupId]", new { GroupId }, commandType: CommandType.StoredProcedure)).DefaultIfEmpty();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override Task<DomainInformation> GetAsync(int QueueItemID)
        {
            throw new NotImplementedException();
        }

        public async override Task<int?> Insert(DomainInformation entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using IDbConnection connection = Connection;
                connection.Open();
                using (transactionopen = connection.BeginTransaction())
                {
                    var result = (await transactionopen.Connection.ExecuteAsync("[dbo].[DomainInformation_Insert]", new
                    {
                        entity.Title,
                        entity.PropertyMapping,
                        entity.Order,
                        entity.Arguments,
                        entity.TypeId,
                        entity.GroupId,
                        entity.HasValidation
                    }, commandType: CommandType.StoredProcedure, transaction: transactionopen));
                    transactionopen.Commit();

                    return result;
                }
            }
            catch (Exception ex)
            {
                transactionopen.Rollback();
                throw ex;
            }
        }

        public override Task<bool> UpdateAsync(DomainInformation entity)
        {
            throw new NotImplementedException();
        }
    }
}
