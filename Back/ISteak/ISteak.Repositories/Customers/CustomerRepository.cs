using ISteak.Core.Customer;
using ISteak.Core.Users;
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

        public async Task<List<User>> GetAllUserAsync()
        {
            var list = new List<User>();

            var commandText = new StringBuilder()
                .AppendLine(" SELECT * FROM [User]")
                .AppendLine(" WHERE [profile_name] = 'User'")
                .AppendLine(" ORDER BY [code] ASC");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            var dataReader = cm.ExecuteReader();

            while (dataReader.Read())
            {
                var user = LoadDataReaderUser(dataReader);
                list.Add(user);
            }

            return list;
        }

        public async Task<List<Stars>> GetAllStarsAsync()
        {
            var list = new List<Stars>();

            var commandText = new StringBuilder()
                .AppendLine(" SELECT * FROM [Review]")
                .AppendLine(" ORDER BY [id] ASC");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            var dataReader = cm.ExecuteReader();

            while (dataReader.Read()) {
                var star = LoadDataReaderStar(dataReader);
                list.Add(star);
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

        private static User LoadDataReaderUser(SqlDataReader dataReader)
        {
            var user = new User();

            user.Id = dataReader.GetGuid("id");
            user.Code = dataReader.GetInt32("code");
            user.Name = dataReader.GetString("name");
            user.AccessKey = dataReader.GetString("access_key");
            user.Password = dataReader.GetString("password");
            user.Email = dataReader.GetString("email");
            user.Phone = dataReader.GetString("phone");
            user.Note = dataReader.GetString("note");
            user.ProfileId = dataReader.GetGuid("profile_id");
            user.ProfileName = dataReader.GetString("profile_name");
            user.AccessCount = dataReader.GetInt32("access_count");
            user.CreationDate = dataReader.GetDateTime("creation_date");
            user.CreationUserId = dataReader.GetGuid("creation_user_id");
            user.CreationUserName = dataReader.GetString("creation_user_name");
            user.ChangeDate = dataReader.GetDateTime("change_date");
            user.ChangeUserId = dataReader.GetGuid("change_user_id");
            user.ChangeUserName = dataReader.GetString("change_user_name");
            user.ExclusionDate = dataReader.GetDateTime("exclusion_date");
            user.ExclusionUserId = dataReader.GetGuid("exclusion_user_id");
            user.ExclusionUserName = dataReader.GetString("exclusion_user_name");
            user.RecordStatus = dataReader.GetInt32("record_status");
            user.RecordStatusName = dataReader.GetString("record_status_name");

            return user;
        }

        private static Stars LoadDataReaderStar(SqlDataReader dataReader)
        {
            var star = new Stars();

            star.Id = dataReader.GetInt32("id");
            star.Star = dataReader.GetInt32("star");

            return star;
        }

    }
}
