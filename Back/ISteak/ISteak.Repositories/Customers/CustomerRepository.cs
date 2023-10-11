using ISteak.Core.Customer;
using ISteak.Repositories.Shared.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public CustomerRepository(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }



        public async Task<Customer> InsertAsync(Customer customer)
        {
            var commandText = new StringBuilder()
              .AppendLine(" INSERT INTO [customer]")
              .AppendLine(" (")
              .AppendLine(" [id],")
              .AppendLine(" [code],")
              .AppendLine(" [name],")
              .AppendLine(" [birthDate],")
              .AppendLine(" [personTypeCode],")
              .AppendLine(" [personTypeName],")
              .AppendLine(" [identity],")
              .AppendLine(" [note],")
              .AppendLine(" [creationDate],")
              .AppendLine(" [creationUserId],")
              .AppendLine(" [creationUserName],")
              .AppendLine(" [changeDate],")
              .AppendLine(" [changeUserId],")
              .AppendLine(" [changeUserName],")
              .AppendLine(" [exclusionDate],")
              .AppendLine(" [exclusionUserId],")
              .AppendLine(" [exclusionUserName],")
              .AppendLine(" [recordStatus],")
              .AppendLine(" [recordStatusName]")
              .AppendLine(" )")
              .AppendLine(" VALUES")
              .AppendLine(" (")
              .AppendLine(" @id,")
              .AppendLine(" @code,")
              .AppendLine(" @name,")
              .AppendLine(" @birth_date,")
              .AppendLine(" @person_type_code,")
              .AppendLine(" @person_type_name,")
              .AppendLine(" @identity,")
              .AppendLine(" @note,")
              .AppendLine(" @creation_date,")
              .AppendLine(" @creation_user_id,")
              .AppendLine(" @creation_user_name,")
              .AppendLine(" @change_date,")
              .AppendLine(" @change_user_id,")
              .AppendLine(" @change_user_name,")
              .AppendLine(" @exclusion_date,")
              .AppendLine(" @exclusion_user_id,")
              .AppendLine(" @exclusion_user_name,")
              .AppendLine(" @record_status_code,")
              .AppendLine(" @record_status_name")
              .AppendLine(" )");

            using var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();
            cm.CommandText = commandText.ToString();

            cm.Parameters.Add(new SqlParameter("@code", InsertCode()));

            SetParameters(customer, cm);

            cm.ExecuteNonQuery();



            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            var commandText = new StringBuilder()
                .AppendLine("UPDATE [Customer]")
                .AppendLine("SET")
                .AppendLine(" [id] = @id,")
                .AppendLine(" [name] = @name,")
                .AppendLine(" [birthDate] = @birth_date,")
                .AppendLine(" [personTypeCode] = @person_type_code,")
                .AppendLine(" [personTYpeName] = @person_type_name,")
                .AppendLine(" [identity] = @identity,")
                .AppendLine(" [note] = @note,")
                .AppendLine(" [creationDate] = @creation_date,")
                .AppendLine(" [creationUserId] = @creation_user_id,")
                .AppendLine(" [creationUserName] = @creation_user_name,")
                .AppendLine(" [changeDate] = @change_date,")
                .AppendLine(" [changeUserId] = @change_user_id,")
                .AppendLine(" [changeUserName] = @change_user_name,")
                .AppendLine(" [exclusionDate] = @exclusion_date,")
                .AppendLine(" [exclusionUserId] = @exclusion_user_id,")
                .AppendLine(" [exclusionUserName] = @exclusion_user_name,")
                .AppendLine(" [recordStatus] = @record_status_code,")
                .AppendLine(" [recordStatusName] = @record_status_name")
                .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            SetParameters(customer, cm);

            cm.ExecuteNonQuery();

            return customer;
        }

        public async Task<long> DeleteAsync(Guid id)
        {
            try
            {
                var commandText = new StringBuilder()
                    .AppendLine(" UPDATE [Customer]")
                    .AppendLine(" SET")
                    .AppendLine(" [exclusionDate] = @exclusion_date,")
                    .AppendLine(" [recordStatus] = @record_status_code,")
                    .AppendLine(" [recordStatusName] = @record_status_name")
                    .AppendLine(" WHERE [id] = @id");

                var connection = new SqlConnection(_connectionProvider.ConnectionString);
                connection.Open();
                var cm = connection.CreateCommand();

                cm.CommandText = commandText.ToString();

                cm.Parameters.Add(new SqlParameter("@id", id));
                cm.Parameters.Add(new SqlParameter("@record_status_code", 2));
                cm.Parameters.Add(new SqlParameter("@record_status_name", "Disabled"));
                cm.Parameters.Add(new SqlParameter("@exclusion_date", DateTime.UtcNow));

                cm.ExecuteNonQuery();

                return 1;
            }
            catch (Exception ex) 
            {
                return 0;
            }

        }

        public async Task<Customer> GetAsync(Guid id)
        {
            var commandText = GetSelectQuery()
                  .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            cm.Parameters.Add(new SqlParameter("@id", id));

            var dataReader = cm.ExecuteReader();

            Customer customer = null;

            while (dataReader.Read())
            {
                customer = LoadDataReader(dataReader);
            }

            return customer;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            var list = new List<Customer>();

            var commandText = GetSelectQuery()
                .AppendLine(" WHERE [recordStatus] = 0")
                .AppendLine(" ORDER BY [code] ASC");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            var dataReader = cm.ExecuteReader();

            while (dataReader.Read())
            {
                var customer = LoadDataReader(dataReader);
                list.Add(customer);
            }

            return list;
        }

        private void SetParameters(Customer customer, SqlCommand cm)
        {
            cm.Parameters.Add(new SqlParameter("@id", customer.Id.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@name", customer.Name.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@birth_date", customer.BirthDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@person_type_code", customer.PersonTypeCode.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@person_type_name", customer.PersonTypeName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@identity", customer.Identity.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@note", customer.Note.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_date", customer.CreationDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_user_id", customer.CreationUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_user_name", customer.CreationUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_date", customer.ChangeDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_id", customer.ChangeUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_name", customer.ChangeUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_date", customer.ExclusionDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_id", customer.ExclusionUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_name", customer.ExclusionUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@record_status_code", customer.RecordStatusCode.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@record_status_name", customer.RecordStatusName.GetDbValue()));
        }

        private int InsertCode()
        {
            var sb = new StringBuilder()
                .AppendLine(" SELECT COUNT([code])")
                .AppendLine(" FROM customer");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = sb.ToString();

            var code = Convert.ToInt32(cm.ExecuteScalar());

            code++;

            return code;
        }
        private StringBuilder GetSelectQuery()
        {
            var sb = new StringBuilder()
            .AppendLine(" SELECT")
            .AppendLine(" A.[id],")
            .AppendLine(" A.[code],")
            .AppendLine(" A.[name],")
            .AppendLine(" A.[birthDate],")
            .AppendLine(" A.[personTypeCode],")
            .AppendLine(" A.[personTypeName],")
            .AppendLine(" A.[identity],")
            .AppendLine(" A.[note],")
            .AppendLine(" A.[creationDate],")
            .AppendLine(" A.[creationUserId],")
            .AppendLine(" A.[creationUserName],")
            .AppendLine(" A.[changeDate],")
            .AppendLine(" A.[changeUserId],")
            .AppendLine(" A.[changeUserName],")
            .AppendLine(" A.[exclusionDate],")
            .AppendLine(" A.[exclusionUserId],")
            .AppendLine(" A.[exclusionUserName],")
            .AppendLine(" A.[recordStatus],")
            .AppendLine(" A.[recordStatusName]")
            .AppendLine(" FROM [Customer] AS A");

            return sb;
        }

        private static Customer LoadDataReader(SqlDataReader dataReader)
        {
            var customer = new Customer();

            customer.Id = dataReader.GetGuid("id");
            customer.Code = dataReader.GetInt32("code");
            customer.Name = dataReader.GetString("name");
            customer.BirthDate = dataReader.GetString("birthDate");
            customer.PersonTypeCode = dataReader.GetInt32("personTypeCode");
            customer.PersonTypeName = dataReader.GetString("personTypeName");
            customer.Identity = dataReader.GetString("identity");
            customer.Note = dataReader.GetString("note");
            customer.CreationDate = dataReader.GetDateTime("creationDate");
            customer.CreationUserId = dataReader.GetGuid("creationUserId");
            customer.CreationUserName = dataReader.GetString("creationUserName");
            customer.ChangeDate = dataReader.GetDateTime("changeDate");
            customer.ChangeUserId = dataReader.GetGuid("changeUserId");
            customer.ChangeUserName = dataReader.GetString("changeUsername");
            customer.ExclusionDate = dataReader.GetDateTime("exclusionDate");
            customer.ExclusionUserId = dataReader.GetGuid("exclusionUserId");
            customer.ExclusionUserName = dataReader.GetString("exclusionUserName");
            customer.RecordStatusCode = dataReader.GetInt32("recordStatus");
            customer.RecordStatusName = dataReader.GetString("recordStatusName");

            return customer;
        }

    }
}
