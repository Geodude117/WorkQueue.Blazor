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

namespace DomainData.Repository.DomainGroupRepo
{
    public class DomainGroupRepo : Repository<DomainGroup>, IDomainGroupRepo
    {

        public DomainGroupRepo(string connection) : base(connection)
        { }

        public override Task<bool> DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async override Task<IEnumerable<DomainGroup>> GetAllAsync()
        {
            try
            {
                using IDbConnection connection = Connection;
                return (await connection.QueryAsync<DomainGroup>("[dbo].[DomainGroup_Select_All]",
                    commandType: CommandType.StoredProcedure)).DefaultIfEmpty();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public override Task<IEnumerable<DomainGroup>> GetAllByGroupIdAsync(int GroupId)
        {
            throw new NotImplementedException();
        }

        public async override Task<DomainGroup> GetAsync(int Id)
        {
            try
            {
                using IDbConnection connection = Connection;
                return (await connection.QueryAsync<DomainGroup>("[dbo].[DomainGroup_Select_By_Id]", new { Id }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
            }
            catch (SqlException ex)

            {
                throw ex;
            }
        }

        public async override Task<int?> Insert(DomainGroup entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using IDbConnection connection = Connection;
                connection.Open();
                using (transactionopen = connection.BeginTransaction())
                {
                    var result = (await transactionopen.Connection.ExecuteAsync("[dbo].[DomainGroup_Insert]", new
                    {
                        entity.GroupName,
                        entity.ExternalReferenceId,
                        entity.IsActive,
                        entity.ClassMapping,
                        entity.AccessGroupBase,
                        entity.AccessGroupPublic,
                        entity.AccessGroupExtended,
                        entity.AccessGroupAdmin
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

        public override Task<bool> UpdateAsync(DomainGroup entity)
        {
            throw new NotImplementedException();
        }
    }
}
