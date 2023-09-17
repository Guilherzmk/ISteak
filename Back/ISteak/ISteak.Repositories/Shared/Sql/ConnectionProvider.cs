using System.Data.SqlClient;

namespace ISteak.Repositories.Shared.Sql
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly string connectionString;

        public ConnectionProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ConnectionProvider(
            string server,
            string database,
            string user,
            string password)
        {
            this.connectionString = "server=" + server + ";database=" + database + ";user=" + user + ";password=" + password;
        }

        public SqlConnection CreateConnection()
        {
            var cn = new SqlConnection(this.connectionString);
            cn.Open();
            return cn;
        }

        public async Task<SqlConnection> CreateConnectionAsync()
        {
            var cn = new SqlConnection(this.connectionString);
            await cn.OpenAsync();
            return cn;
        }
    }
}



