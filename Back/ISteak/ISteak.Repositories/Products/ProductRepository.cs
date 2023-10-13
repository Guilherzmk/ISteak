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
            cm.Parameters.Add(new SqlParameter("@record_status", 1));
            cm.Parameters.Add(new SqlParameter("@record_status_name", "Ativo"));

            SetParameters(product, cm);

            cm.ExecuteNonQuery();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var commandText = new StringBuilder()
                .AppendLine(" UPDATE [Product]")
                .AppendLine(" SET")
                .AppendLine(" [id] = @id,")
                .AppendLine(" [code] = @code,")
                .AppendLine(" [name] = @name,")
                .AppendLine(" [note] = @note,")
                .AppendLine(" [preco] = @price,")
                .AppendLine(" [status_code] = @status_code,")
                .AppendLine(" [status_name] = @status_name,")
                .AppendLine(" [quantity] = @quantity,")
                .AppendLine(" [image] = @image,")
                .AppendLine(" [creation_date] = @creation_date,")
                .AppendLine(" [creation_user_id] = @creation_user_id,")
                .AppendLine(" [creation_user_name] = @creation_user_name,")
                .AppendLine(" [change_date] = @change_date,")
                .AppendLine(" [change_user_id] = @change_user_id,")
                .AppendLine(" [change_user_name] = @change_user_name,")
                .AppendLine(" [exclusion_date] = @exclusion_date,")
                .AppendLine(" [exclusion_user_id] = @exclusion_user_id,")
                .AppendLine(" [exclusion_user_name] = @exclusion_user_name,")
                .AppendLine(" [record_status] = @record_status,")
                .AppendLine(" [record_status_name] = @record_status_name")
                .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            SetParameters(product, cm);

            cm.ExecuteNonQuery();

            return product;
        }   

        public async Task<long> DeleteAsync(Guid id)
        {
            try
            {
                var commandText = new StringBuilder()
                    .AppendLine(" UPDATE [Product]")
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


        public async Task<Product> GetAsync(Guid id)
        {
            var commandText = GetSelectQuery()
                  .AppendLine(" WHERE [id] = @id");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();
            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            cm.Parameters.Add(new SqlParameter("@id", id));

            var dataReader = cm.ExecuteReader();

            Product product = null;

            while (dataReader.Read())
            {
                product = LoadDataReader(dataReader);
            }

            return product;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var list = new List<Product>();

            var commandText = GetSelectQuery()
                .AppendLine(" WHERE [record_status] = 1")
                .AppendLine(" ORDER BY [code] ASC");

            var connection = new SqlConnection(_connectionProvider.ConnectionString);
            connection.Open();

            var cm = connection.CreateCommand();

            cm.CommandText = commandText.ToString();

            var dataReader = cm.ExecuteReader();

            while (dataReader.Read())
            {
                var product = LoadDataReader(dataReader);
                list.Add(product);
            }

            return list;
        }

        private StringBuilder GetSelectQuery()
        {
            var sb = new StringBuilder()
            .AppendLine(" SELECT")
            .AppendLine(" A.[id],")
            .AppendLine(" A.[code],")
            .AppendLine(" A.[name],")
            .AppendLine(" A.[note],")
            .AppendLine(" A.[preco],")
            .AppendLine(" A.[status_code],")
            .AppendLine(" A.[status_name],")
            .AppendLine(" A.[quantity],")
            .AppendLine(" A.[creation_date],")
            .AppendLine(" A.[creation_user_id],")
            .AppendLine(" A.[creation_user_name],")
            .AppendLine(" A.[change_date],")
            .AppendLine(" A.[change_user_id],")
            .AppendLine(" A.[change_user_name],")
            .AppendLine(" A.[exclusion_date],")
            .AppendLine(" A.[exclusion_user_id],")
            .AppendLine(" A.[exclusion_user_name],")
            .AppendLine(" A.[record_status],")
            .AppendLine(" A.[record_status_name]")
            .AppendLine(" FROM [Product] AS A");

            return sb;
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
            if(product.Image == null)
            {
                cm.Parameters.Add(new SqlParameter("@image", 1));
            }
            else
            {
                cm.Parameters.Add(new SqlParameter("@image", System.Data.SqlDbType.Image, product.Image.ToString().Length).Value = product.Image);
            }
    
            cm.Parameters.Add(new SqlParameter("@creation_date", product.CreationDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_user_id", product.CreationUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@creation_user_name", product.CreationUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_date", product.ChangeDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_id", product.ChangeUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@change_user_name", product.ChangeUserName.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_date", product.ExclusionDate.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_id", product.ExclusionUserId.GetDbValue()));
            cm.Parameters.Add(new SqlParameter("@exclusion_user_name", product.ExclusionUserName.GetDbValue()));
        }

        private static Product LoadDataReader(SqlDataReader dataReader)
        {
            var product = new Product();

            product.Id = dataReader.GetGuid("id");
            product.Code = dataReader.GetInt32("code");
            product.Name = dataReader.GetString("name");
            product.Note = dataReader.GetString("note");
            product.Price = dataReader.GetDouble("preco");
            product.StatusCode = dataReader.GetInt32("status_code");
            product.StatusName = dataReader.GetString("status_name");
            product.Quantity = dataReader.GetInt32("quantity");
            product.CreationDate = dataReader.GetDateTime("creation_date");
            product.CreationUserId = dataReader.GetGuid("creation_user_id");
            product.CreationUserName = dataReader.GetString("creation_user_name");
            product.ChangeDate = dataReader.GetDateTime("change_date");
            product.ChangeUserId = dataReader.GetGuid("change_user_id");
            product.ChangeUserName = dataReader.GetString("change_user_name");
            product.ExclusionDate = dataReader.GetDateTime("exclusion_date");
            product.ExclusionUserId = dataReader.GetGuid("exclusion_user_id");
            product.ExclusionUserName = dataReader.GetString("exclusion_user_name");
            product.RecordStatus = dataReader.GetInt32("record_status");
            product.RecordStatusName = dataReader.GetString("record_status_name");

            return product;
        }
    }
}
