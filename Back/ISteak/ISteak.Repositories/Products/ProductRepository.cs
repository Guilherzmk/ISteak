using ISteak.Core.Customer;
using ISteak.Core.Products;
using ISteak.Repositories.Shared.Sql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISteak.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {

        private readonly SqlConnectionProvider _connectionProvider;

        public ProductRepository(SqlConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<Product> InsertAsync(Product product)
        {
            var commandText = new StringBuilder()
              .AppendLine(" INSERT INTO [Product]")
              .AppendLine(" (")
              .AppendLine(" [id],")
              .AppendLine(" [code],")
              .AppendLine(" [name],")
              .AppendLine(" [note],")
              .AppendLine(" [preco],")
              .AppendLine(" [status_code],")
              .AppendLine(" [status_name],")
              .AppendLine(" [quantity],")
              .AppendLine(" [image],")
              .AppendLine(" [creation_date],")
              .AppendLine(" [creation_user_id],")
              .AppendLine(" [creation_user_name],")
              .AppendLine(" [change_date],")
              .AppendLine(" [change_user_id],")
              .AppendLine(" [change_user_name],")
              .AppendLine(" [exclusion_date],")
              .AppendLine(" [exclusion_user_id],")
              .AppendLine(" [exclusion_user_name],")
              .AppendLine(" [record_status],")
              .AppendLine(" [record_status_name]")
              .AppendLine(" )")
              .AppendLine(" VALUES")
              .AppendLine(" (")
              .AppendLine(" @id,")
              .AppendLine(" @code,")
              .AppendLine(" @name,")
              .AppendLine(" @note,")
              .AppendLine(" @price,")
              .AppendLine(" @status_code,")
              .AppendLine(" @status_name,")
              .AppendLine(" @quantity,")
              .AppendLine(" @image,")
              .AppendLine(" @creation_date,")
              .AppendLine(" @creation_user_id,")
              .AppendLine(" @creation_user_name,")
              .AppendLine(" @change_date,")
              .AppendLine(" @change_user_id,")
              .AppendLine(" @change_user_name,")
              .AppendLine(" @exclusion_date,")
              .AppendLine(" @exclusion_user_id,")
              .AppendLine(" @exclusion_user_name,")
              .AppendLine(" @record_status,")
              .AppendLine(" @record_status_name")
              .AppendLine(" )");

            using var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();
            cm.CommandText = commandText.ToString();

            cm.Parameters.Add(new SqlParameter("@code", InsertCode()));

            SetParameters(product, cm);

            cm.ExecuteNonQuery();

            return product;
        }

        public Task<long> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }



        public Task<Product> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        private int InsertCode()
        {
            var sb = new StringBuilder()
                .AppendLine(" SELECT COUNT([code])")
                .AppendLine(" FROM Product");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = sb.ToString();

            var code = Convert.ToInt32(cm.ExecuteScalar());

            code++;

            return code;
        }

        private void SetParameters(Product product, SqlCommand cm)
        {
            cm.Parameters.Add(new SqlParameter("@id", product.Id.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@name", product.Name.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@note", product.Note.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@price", product.Price.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@status_code", product.StatusCode.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@status_name", product.StatusName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@quantity", product.Quantity.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@image", product.Image.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_date", product.CreationDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_user_id", product.CreationUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_user_name", product.CreationUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_date", product.ChangeDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_id", product.ChangeUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_name", product.ChangeUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_date", product.ExclusionDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_id", product.ExclusionUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_name", product.ExclusionUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@record_status", product.RecordStatus.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@record_status_name", product.RecordStatusName.GetDbValue()));
        }
    }
}
