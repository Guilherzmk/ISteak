using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ISteak.Repositories.Shared.Sql
{
    public class SqlServerFunctions
    {
        public static async Task InsertDataTableAsync(SqlConnection connection, string tableName, DataTable dt)
        {
            var bulkCopy = new SqlBulkCopy(
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                )
                { DestinationTableName = tableName, BulkCopyTimeout = 0 };

            await bulkCopy.WriteToServerAsync(dt);
        }


        public static async Task ExecuteScriptAsync(SqlConnection connection, string script)
        {
            IEnumerable<string> commands = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
 
            foreach (var cmdText in commands)
            {
                if (string.IsNullOrWhiteSpace(cmdText.Trim()))
                    continue;

                await using var command = new SqlCommand(cmdText, connection);
                await  command.ExecuteNonQueryAsync();
            }
      
        }

        public static object GetDbValue(object value)
        {
            switch (value)
            {
                case null:
                    return null;
                case DateTime:
                {
                    var date = Convert.ToDateTime(value);
                    if (date < new DateTime(1900, 01, 01) || date > new DateTime(3000, 01, 01))
                        return new DateTime(1900, 01, 01).ToString("yyyy-MM-dd HH:mm:ss");

                    return date.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            if (value is string)
            {
                var str = value.ToString();
                if (string.IsNullOrWhiteSpace(str))
                    return null;

                if (str.Length > 8000)
                    str = str[..7999];

                return str;
            }
 
            return value;
        }
    }
}