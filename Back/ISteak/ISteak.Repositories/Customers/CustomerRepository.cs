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



        public async Task<Customer> InsertAsync(Customer customer)
        {
            var commandText = new StringBuilder()
              .AppendLine(" INSERT INTO [customer]")
              .AppendLine(" (")
              .AppendLine(" [id],")
              .AppendLine(" [code],")
              .AppendLine(" [name],")
              .AppendLine(" [birth_date],")
              .AppendLine(" [person_type_code],")
              .AppendLine(" [person_type_name],")
              .AppendLine(" [identity],")
              .AppendLine(" [note],")
              .AppendLine(" [creation_date],")
              .AppendLine(" [creation_user_id],")
              .AppendLine(" [creation_user_name],")
              .AppendLine(" [change_date],")
              .AppendLine(" [change_user_id],")
              .AppendLine(" [change_user_name],")
              .AppendLine(" [exclusion_date],")
              .AppendLine(" [exclusion_user_id],")
              .AppendLine(" [exclusion_user_name],")
              .AppendLine(" [record_status_code],")
              .AppendLine(" [record_status_name]")
              .AppendLine(" )")
              .AppendLine(" VALUES")
              .AppendLine(" (")
              .AppendLine(" @id,")
              .AppendLine(" @code,")
              .AppendLine(" @name,")
              .AppendLine(" @nickname,")
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
    }
}
