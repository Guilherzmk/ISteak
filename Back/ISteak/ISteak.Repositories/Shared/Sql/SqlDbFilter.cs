using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ISteak.Repositories.Shared.Sql
{
    public class SqlDbFilter
    {
        private readonly IDbCommand dbCommand;
        private readonly StringBuilder commandText;

        public SqlDbFilter(IDbCommand dbCommand, StringBuilder commandText)
        {
            this.dbCommand = dbCommand;
            this.commandText = commandText;
        }

        public void SetFilter(string columnName, Guid? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }

            var parameterName = "@" + columnName;

            var operation = isTrue ? " = " : " != ";

            this.commandText.AppendLine(" AND " + columnName + "" + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, IList<Guid> value, bool isTrue = true)
        {
            if (value == null || value.Count <= 0)
            {
                return;
            }

            var operation = isTrue ? " IN " : " NOT IN ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + "(");
            for (var i = 0; i < value.Count; i++)
            {
                var parameterName = "@" + columnName + i;
                this.commandText.AppendLine((i > 0 ? ", " : string.Empty) + parameterName);

                this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value[i]));

            }
            this.commandText.AppendLine(")");
        }

        public void SetFilter(string columnName, int? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }

            var parameterName = "@" + columnName;
            var operation = isTrue ? " = " : " != ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, short? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }

            var parameterName = "@" + columnName;
            var operation = isTrue ? " = " : " != ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, long? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }

            var parameterName = "@" + columnName;
            var operation = isTrue ? " = " : " != ";
            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, bool? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }

            var parameterName = "@" + columnName;
            var operation = isTrue ? " = " : " != ";
            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, IList<int> value, bool isTrue = true)
        {
            if (value == null || value.Count <= 0)
            {
                return;
            }

            var operation = isTrue ? " IN " : " NOT IN ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + "(");
            for (var i = 0; i < value.Count; i++)
            {
                var parameterName = "@" + columnName + i;
                this.commandText.AppendLine((i > 0 ? ", " : string.Empty) + parameterName);

                this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value[i]));

            }
            this.commandText.AppendLine(")");
        }

        public void SetFilter(string columnName, IList<short> value, bool isTrue = true)
        {
            if (value == null || value.Count <= 0)
            {
                return;
            }

            var operation = isTrue ? " IN " : " NOT IN ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + "(");
            for (var i = 0; i < value.Count; i++)
            {
                var parameterName = "@" + columnName + i;
                this.commandText.AppendLine((i > 0 ? ", " : string.Empty) + parameterName);

                this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value[i]));
            }
            this.commandText.AppendLine(")");
        }

        public void SetFilter(string columnName, IList<long> value, bool isTrue = true)
        {
            if (value == null || value.Count <= 0)
            {
                return;
            }

            var operation = isTrue ? " IN " : " NOT IN ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + "(");
            for (var i = 0; i < value.Count; i++)
            {
                var parameterName = "@" + columnName + i;
                this.commandText.AppendLine((i > 0 ? ", " : string.Empty) + parameterName);

                this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value[i]));
            }
            this.commandText.AppendLine(")");
        }

        public void SetFilter(string columnName, string value, bool isTrue = true)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var parameterName = "@" + columnName;
            var operation = isTrue ? " LIKE " : " NOT LIKE ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + " '%' + " + parameterName + " + '%' ");

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterEqual(string columnName, string value, bool isTrue = true)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var parameterName = "@" + columnName;
            var operation = isTrue ? " = " : " != ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, IList<string> value, bool isTrue = true)
        {
            if (value == null || value.Count <= 0)
            {
                return;
            }

            var operation = isTrue ? " IN " : " NOT IN ";

            this.commandText.AppendLine(" AND " + columnName + "  " + operation + "(");

            for (var i = 0; i < value.Count; i++)
            {
                var parameterName = "@" + columnName + i;

                this.commandText.AppendLine((i > 0 ? ", " : string.Empty) + parameterName + i);

                this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value[i]));
            }
            this.commandText.AppendLine(")");
        }

        public void SetFilterBegin(string columnName, int? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_BEGIN";

            var operation = isTrue ? " >= " : " < ";


            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);
            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterEnd(string columnName, short? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_END";
            var operation = isTrue ? " <= " : " > ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterBegin(string columnName, short? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_BEGIN";
            var operation = isTrue ? " >= " : " < ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterEnd(string columnName, int? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_END";
            var operation = isTrue ? " <= " : " > ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterBegin(string columnName, long? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_BEGIN";
            var operation = isTrue ? " >= " : " < ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterEnd(string columnName, long? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_END";
            var operation = isTrue ? " <= " : " > ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, DateTime? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName;
            var operation = isTrue ? " = " : " != ";


            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);
            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterBegin(string columnName, DateTime? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_BEGIN";
            var operation = isTrue ? " >= " : " < ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterEnd(string columnName, DateTime? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_END";
            var operation = isTrue ? " <= " : " > ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilter(string columnName, double? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName;
            var operation = isTrue ? " = " : " != ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterBegin(string columnName, double? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_BEGIN";
            var operation = isTrue ? " >= " : " < ";

            this.commandText.AppendLine(" AND " + columnName + " " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

        public void SetFilterEnd(string columnName, double? value, bool isTrue = true)
        {
            if (!value.HasValue)
            {
                return;
            }
            var parameterName = "@" + columnName + "_END";
            var operation = isTrue ? " <= " : " > ";
            this.commandText.AppendLine(" AND " + columnName + "  " + operation + parameterName);

            this.dbCommand.Parameters.Add(new SqlParameter(parameterName, value));
        }

 

    }
}