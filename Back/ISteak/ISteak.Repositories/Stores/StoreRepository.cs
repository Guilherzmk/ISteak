using ISteak.Core.Customer;
using ISteak.Core.Products;
using ISteak.Core.Stores;
using ISteak.Repositories.Shared.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Repositories.Stores
{
    public class StoreRepository : IStoreRepository
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public StoreRepository(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<Store> InsertAsync(Store store)
        {
            throw new NotImplementedException();

        }

        public Task<long> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Store> GetAsync(Guid id)
        {
            var commandText = GetSelectQuery()
                  .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            cm.Parameters.Add(new SqlParameter("@id", id));

            var dataReader = cm.ExecuteReader();

            Store store = null;

            while (dataReader.Read())
            {
                store = LoadDataReader(dataReader);
            }

            return store;
        }

        public async Task<Store> UpdateAsync(Store store)
        {
            var commandText = new StringBuilder()
                 .AppendLine("UPDATE [Store]")
                 .AppendLine("SET")
                 .AppendLine(" [id] = @id,")
                 .AppendLine(" [name] = @name,")
                 .AppendLine(" [nickname] = @nickname,")
                 .AppendLine(" [identity] = @identity")
                 .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            SetParameters(store, cm);

            cm.ExecuteNonQuery();

            return store;
        }

        private StringBuilder GetSelectQuery()
        {
            var sb = new StringBuilder()
            .AppendLine(" SELECT")
            .AppendLine(" A.[id],")
            .AppendLine(" A.[code],")
            .AppendLine(" A.[name],")
            .AppendLine(" A.[nickname],")
            .AppendLine(" A.[identity]")
            .AppendLine(" FROM [Store] AS A");

            return sb;
        }

        private static Store LoadDataReader(SqlDataReader dataReader)
        {
            var store = new Store();

            store.Id = dataReader.GetGuid("id");
            store.Code = dataReader.GetInt32("code");
            store.Name = dataReader.GetString("name");
            store.Nickname = dataReader.GetString("nickname");
            store.Identity = dataReader.GetString("identity");
           

            return store;
        }

        private void SetParameters(Store store, SqlCommand cm)
        {
            cm.Parameters.Add(new SqlParameter("@id", store.Id.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@name", store.Name.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@nickname", store.Nickname.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@identity", store.Identity.GetDbValue()));
        }

    }
}
