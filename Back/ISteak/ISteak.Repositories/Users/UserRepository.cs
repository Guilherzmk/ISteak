using ISteak.Core.Users;
using ISteak.Repositories.Criptographys;
using ISteak.Repositories.Shared.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnectionProvider _connectionProvider;

        public UserRepository(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<User> InsertAsync(User user)
        {
            var commandText = new StringBuilder()
                .AppendLine(" INSERT INTO [User]")
                .AppendLine(" (")
                .AppendLine("[id],")
                .AppendLine("[code],")
                .AppendLine("[name],")
                .AppendLine("[access_key],")
                .AppendLine("[password],")
                .AppendLine("[email],")
                .AppendLine("[phone],")
                .AppendLine("[profile_id],")
                .AppendLine("[profile_name],")
                .AppendLine("[access_count],")
                .AppendLine("[note],")
                .AppendLine("[creation_date],")
                .AppendLine("[creation_user_id],")
                .AppendLine("[creation_user_name],")
                .AppendLine("[change_date],")
                .AppendLine("[change_user_id],")
                .AppendLine("[change_user_name],")
                .AppendLine("[exclusion_date],")
                .AppendLine("[exclusion_user_id],")
                .AppendLine("[exclusion_user_name],")
                .AppendLine("[record_status],")
                .AppendLine("[record_status_name]")
                .AppendLine(" )")
                .AppendLine(" VALUES")
                .AppendLine(" (")
                .AppendLine("@id,")
                .AppendLine("@code,")
                .AppendLine("@name,")
                .AppendLine("@access_key,")
                .AppendLine("@password,")
                .AppendLine("@email,")
                .AppendLine("@phone,")
                .AppendLine("@profile_id,")
                .AppendLine("@profile_name,")
                .AppendLine("@access_count,")
                .AppendLine("@note,")
                .AppendLine("@creation_date,")
                .AppendLine("@creation_user_id,")
                .AppendLine("@creation_user_name,")
                .AppendLine("@change_date,")
                .AppendLine("@change_user_id,")
                .AppendLine("@change_user_name,")
                .AppendLine("@exclusion_date,")
                .AppendLine("@exclusion_user_id,")
                .AppendLine("@exclusion_user_name,")
                .AppendLine("@record_status,")
                .AppendLine("@record_status_name")
                .AppendLine(" )");

            using var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();
            cm.CommandText = commandText.ToString();

            var byteS = Cryptography.CreateHash(user.Password);
            string password = Convert.ToBase64String(byteS);

            cm.Parameters.Add(new SqlParameter("@code", InsertCode()));
            cm.Parameters.Add(new SqlParameter("@password", password));
            if(user.ProfileId != null)
            {
                cm.Parameters.Add(new SqlParameter("@profile_id", user.ProfileId.GetDbValue()));
            }
            else
            {
                cm.Parameters.Add(new SqlParameter("@profile_id", "3FA85F64-5717-4562-B3FC-2C963F66AFA6"));
            }

            if (user.ProfileName != null)
            {
                cm.Parameters.Add(new SqlParameter("@profile_name", user.ProfileId.GetDbValue()));
            }
            else
            {
                cm.Parameters.Add(new SqlParameter("@profile_name", "Admin"));
            }

         
            cm.Parameters.Add(new SqlParameter("@access_count", 1));
            cm.Parameters.Add(new SqlParameter("@creation_date", DateTime.UtcNow));
            cm.Parameters.Add(new SqlParameter("@creation_user_id", user.Id));
            cm.Parameters.Add(new SqlParameter("@creation_user_name", user.Name));

            SetParameters(user, cm);

            var verify = VerifyAccessKey(user.AccessKey);

            if (verify == true)
            {
                cm.ExecuteNonQuery();
            }
            else
            {
                return null;
            }

            return user;
        }

        public async Task<User> Get(string accessKey)
        {
            try
            {
                var commandText = new StringBuilder()
                .AppendLine(" SELECT * FROM [User]")
                .AppendLine(" WHERE [access_key] = @access_key");

                using var connection = new SqlConnection(_connectionProvider.ConnectionString);
                connection.Open();
                var cm = connection.CreateCommand();

                cm.CommandText = commandText.ToString();

                cm.Parameters.Add(new SqlParameter("@access_key", accessKey));

                var dataReader = cm.ExecuteReader();

                User user = null;

                while (dataReader.Read())
                {
                    user = LoadDataReader(dataReader);
                }

                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<User> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        private void SetParameters(User user, SqlCommand cm)
        {
            cm.Parameters.Add(new SqlParameter("@id", user.Id.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@name", user.Name.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@access_key", user.AccessKey.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@email", user.Email.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@phone", user.Phone.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@note", user.Note.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_date", user.ChangeDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_id", user.ChangeUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_name", user.ChangeUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_date", user.ExclusionDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_id", user.ExclusionUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_name", user.ExclusionUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@record_status", user.RecordStatus.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@record_status_name", user.RecordStatusName.GetDbValue()));
        }

        private static User LoadDataReader(SqlDataReader dataReader)
        {
            var user = new User();

            user.Id = dataReader.GetGuid("id");
            user.Code = dataReader.GetInt32("code");
            user.Name = dataReader.GetString("name");
            user.AccessKey = dataReader.GetString("access_key");
            user.Password = dataReader.GetString("password");
            user.Email = dataReader.GetString("email");
            user.Phone = dataReader.GetString("phone");
            user.ProfileId = dataReader.GetGuid("profile_id");
            user.ProfileName = dataReader.GetString("profile_name");
            user.AccessCount = dataReader.GetInt32("id");
            user.Note = dataReader.GetString("id");
            user.CreationDate = dataReader.GetDateTime("id");
            user.CreationUserId = dataReader.GetGuid("id");
            user.CreationUserName = dataReader.GetString("id");
            user.ChangeDate = dataReader.GetDateTime("id");
            user.ChangeUserName = dataReader.GetString("id");
            user.ExclusionDate = dataReader.GetDateTime("id");
            user.ExclusionUserId = dataReader.GetGuid("id");
            user.ExclusionUserName = dataReader.GetString("id");
            user.RecordStatus = dataReader.GetInt16("id");

            return user;
        }

        private int InsertCode()
        {
            var sb = new StringBuilder()
                .AppendLine(" SELECT COUNT([code])")
                .AppendLine(" FROM [User]");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = sb.ToString();

            var code = Convert.ToInt32(cm.ExecuteScalar());

            code++;

            return code;
        }

        private bool VerifyAccessKey(string accessKey)
        {
            var sb = new StringBuilder()
                .AppendLine("SELECT * from [User]")
                .AppendLine("WHERE [access_key] = @access_key");

            using var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = sb.ToString();

            cm.Parameters.Add(new SqlParameter("@access_key", accessKey));

            var result = cm.ExecuteScalar();

            if (result != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
