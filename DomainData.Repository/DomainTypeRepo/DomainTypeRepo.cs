using Dapper;
using DomainData.Models;
using DomainData.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DomainData.Repository.DomainTypeRepo
{
    public class DomainTypeRepo : Repository<DomainType>, IDomainTypeRepo
    {

        public DomainTypeRepo(string connection) : base(connection)
        { }

        public override Task<bool> DeleteAsync(int Id)
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<DomainType>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public override Task<IEnumerable<DomainType>> GetAllByGroupIdAsync(int GroupId)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<DomainType> GetAsync(int Id)
        {
            try
            {
                using IDbConnection connection = Connection;
                return (await connection.QueryAsync<DomainType>("[dbo].[DomainType_Select_By_ID]", new { Id }, commandType: CommandType.StoredProcedure)).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async override Task<int?> Insert(DomainType entity)
        {
            IDbTransaction transactionopen = null;
            try
            {
                using IDbConnection connection = Connection;
                connection.Open();
                using (transactionopen = connection.BeginTransaction())
                {
                    var result = (await transactionopen.Connection.ExecuteAsync("[dbo].[DomainType_Insert]", new
                    {
                        entity.TypeName,
                        entity.ClassObject
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

        public override Task<bool> UpdateAsync(DomainType entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
