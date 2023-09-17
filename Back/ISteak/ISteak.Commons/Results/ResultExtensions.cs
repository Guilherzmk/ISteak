using System.Text;

namespace ISteak.Commons.Results
{
    public static class ResultExtensions
    {
        public static IEnumerable<string> GetTexts(this IList<ResultMessage> results)
        {
            return results.Select(i => i.Text).ToList();
        }

        public static string ToText(this IList<ResultMessage> results)
        {
            var sb = new StringBuilder();

            foreach (var item in results)
            {
                sb.AppendLine(item.Text);
            }

            return sb.ToString();
        }

        public static bool AddMessages(this IList<ResultMessage> results, ResultMessage result)
        {
            if (result == null)
                return false;

            results.Add(result);

            return true;
        }

        public static IList<string> GetListTextMessages(this IList<ResultMessage> messages)
        {
            var l = new List<string>();

            if (messages == null)
                return l;

            l.AddRange(messages.Select(item => item.Text));

            return l;

        }

        public static string GetTextMessages(this IList<ResultMessage> messages)
        {
            try
            {
                var sb = new StringBuilder();

                if (messages == null)
                    return string.Empty;

                foreach (var item in messages)
                {
                    sb.Append(item.Text + "; ");
                }

                var result = sb.ToString();

                if (string.IsNullOrWhiteSpace(result))
                {
                    return string.Empty;
                }

                result = result.Trim();

                result = result.Substring(0, result.Length - 1)?.Trim();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string GetCodeMessages(this IList<ResultMessage> messages)
        {
            try
            {
                var sb = new StringBuilder();

                if (messages == null)
                    return string.Empty;

                foreach (var item in messages)
                {
                    sb.Append(item.Code + "; ");
                }

                var result = sb.ToString();

                if (string.IsNullOrWhiteSpace(result))
                {
                    return string.Empty;
                }

                result = result.Trim();

                result = result.Substring(0, result.Length - 1)?.Trim();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
