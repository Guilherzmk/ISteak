namespace ISteak.Repositories.Shared.Sql
{
    public class SqlConnectionProvider 
    {
        public string ConnectionString { get; set; }
        public SqlConnectionProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}


 