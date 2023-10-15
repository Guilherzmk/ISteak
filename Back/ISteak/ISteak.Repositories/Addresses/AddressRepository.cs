using ISteak.Core.Addresses;
using ISteak.Core.Stores;
using ISteak.Repositories.Shared.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Repositories.Addresses
{
    public class AddressRepository : IAddressRepository
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public AddressRepository(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }
        public async Task<Address> GetAsync(Guid id)
        {
            var commandText = GetSelectQuery()
                  .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            cm.Parameters.Add(new SqlParameter("@id", id));

            var dataReader = cm.ExecuteReader();

            Address address = null;

            while (dataReader.Read())
            {
                address = LoadDataReader(dataReader);
            }

            return address;
        }

        public async Task<Address> UpdateAsync(Address address)
        {
            var commandText = new StringBuilder()
                 .AppendLine("UPDATE [Address]")
                 .AppendLine("SET")
                 .AppendLine(" [id] = @id,")
                 .AppendLine(" [street] = @street,")
                 .AppendLine(" [number] = @number,")
                 .AppendLine(" [neighborhood] = @neighborhood,")
                 .AppendLine(" [city] = @city,")
                 .AppendLine(" [state] = @state,")
                 .AppendLine(" [complement] = @complement,")
                 .AppendLine(" [cep] = @cep")
                 .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            SetParameters(address, cm);

            cm.ExecuteNonQuery();

            return address;
        }

        private StringBuilder GetSelectQuery()
        {
            var sb = new StringBuilder()
            .AppendLine(" SELECT")
            .AppendLine(" A.[id],")
            .AppendLine(" A.[street],")
            .AppendLine(" A.[number],")
            .AppendLine(" A.[neighborhood],")
            .AppendLine(" A.[city],")
            .AppendLine(" A.[state],")
            .AppendLine(" A.[complement],")
            .AppendLine(" A.[cep]")
            .AppendLine(" FROM [Address] AS A");

            return sb;
        }

        private static Address LoadDataReader(SqlDataReader dataReader)
        {
            var address = new Address();

            address.Id = dataReader.GetGuid("id");
            address.Street = dataReader.GetString("street");
            address.Number = dataReader.GetString("number");
            address.Neighborhood = dataReader.GetString("neighborhood");
            address.City = dataReader.GetString("city");
            address.State = dataReader.GetString("state");
            address.Complement = dataReader.GetString("complement");
            address.ZipCode = dataReader.GetString("cep");

            return address;
        }

        private void SetParameters(Address address, SqlCommand cm)
        {
            cm.Parameters.Add(new SqlParameter("@id", address.Id.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@street", address.Street.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@number", address.Number.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@neighborhood", address.Neighborhood.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@city", address.City.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@state", address.State.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@complement", address.Complement.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@cep", address.ZipCode.GetDbValue()));
        }
    }
}
