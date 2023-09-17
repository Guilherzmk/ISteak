using System.Data.SqlClient;

namespace ISteak.Repositories.Shared.Sql
{
    public interface IConnectionProvider
    {
        SqlConnection CreateConnection();
        Task<SqlConnection> CreateConnectionAsync();
    }
}