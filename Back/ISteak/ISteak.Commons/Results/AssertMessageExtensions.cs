using System.Text.RegularExpressions;

namespace ISteak.Commons.Results
{
    public class AssertMessageExtensions
    {
        public static ResultMessage Length(string value, int minimum, int maximum, ResultMessage message)
        {
            var length = 0;
            if (!string.IsNullOrEmpty(value))
                length = value.Trim().Length;

            return (length < minimum || length > maximum) ? message : null;
        }

        public static ResultMessage LengthMinimum(string value, int minimum, ResultMessage message)
        {
            var length = 0;
            if (!string.IsNullOrEmpty(value))
                length = value.Trim().Length;

            return (length < minimum) ? message : null;
        }

        public static ResultMessage Matches(string pattern, string value, ResultMessage message)
        {
            var regex = new Regex(pattern);

            return (!regex.IsMatch(value)) ? message : null;
        }

        public static ResultMessage NotNullOrEmpty(string value, ResultMessage message)
        {
            return (string.IsNullOrEmpty(value)) ? message : null;
        }

        public static ResultMessage Null(object obj, ResultMessage message)
        {
            return (obj != null) ? message : null;
        }

        public static ResultMessage NotNull(object obj, ResultMessage message)
        {
            return (obj == null) ? message : null;
        }

        public static ResultMessage True(bool boolValue, ResultMessage message)
        {
            return (!boolValue) ? message : null;
        }

        public static ResultMessage False(bool boolValue, ResultMessage message)
        {
            return (boolValue) ? message : null;
        }

        public static ResultMessage Equals(string value1, string value2, ResultMessage message)
        {
            return (value1 != value2) ? message : null;
        }

        public static ResultMessage Equals(int value1, int value2, ResultMessage message)
        {
            return (value1 != value2) ? message : null;
        }

        public static ResultMessage Equals(short value1, short value2, ResultMessage message)
        {
            return (value1 != value2) ? message : null;
        }

        public static ResultMessage Equals(long value1, long value2, ResultMessage message)
        {
            return (value1 != value2) ? message : null;
        }

        public static ResultMessage Equals(double value1, double value2, ResultMessage message)
        {
            return (value1 != value2) ? message : null;
        }

        public static ResultMessage Equals(Guid value1, Guid value2, ResultMessage message)
        {
            return (value1 != value2) ? message : null;
        }

        public static ResultMessage NotEquals(string value1, string value2, ResultMessage message)
        {
            return (value1 == value2) ? message : null;
        }

        public static ResultMessage NotEquals(int value1, int value2, ResultMessage message)
        {
            return (value1 == value2) ? message : null;
        }

        public static ResultMessage NotEquals(short value1, short value2, ResultMessage message)
        {
            return (value1 == value2) ? message : null;
        }

        public static ResultMessage NotEquals(long value1, long value2, ResultMessage message)
        {
            return (value1 == value2) ? message : null;
        }

        public static ResultMessage NotEquals(double value1, double value2, ResultMessage message)
        {
            return (value1 == value2) ? message : null;
        }

        public static ResultMessage NotEquals(Guid value1, Guid value2, ResultMessage message)
        {
            return (value1 == value2) ? message : null;
        }

        public static ResultMessage Greater(int value1, int value2, ResultMessage message)
        {
            return (!(value1 > value2)) ? message : null;
        }

        public static ResultMessage Greater(double value1, double value2, ResultMessage message)
        {
            return (!(value1 > value2)) ? message : null;
        }

        public static ResultMessage Greater(DateTime value1, DateTime value2, ResultMessage message)
        {
            return (!(value1 > value2)) ? message : null;
        }

        public static ResultMessage GreaterOrEqual(int value1, int value2, ResultMessage message)
        {
            return (!(value1 >= value2)) ? message : null;
        }

        public static ResultMessage GreaterOrEqual(int value1, short value2, ResultMessage message)
        {
            return (!(value1 >= value2)) ? message : null;
        }

        public static ResultMessage GreaterOrEqual(long value1, long value2, ResultMessage message)
        {
            return (!(value1 >= value2)) ? message : null;
        }

        public static ResultMessage GreaterOrEqual(float value1, float value2, ResultMessage message)
        {
            return (!(value1 >= value2)) ? message : null;
        }

        public static ResultMessage GreaterOrEqual(decimal value1, decimal value2, ResultMessage message)
        {
            return (!(value1 >= value2)) ? message : null;
        }

        public static ResultMessage GreaterOrEqual(double value1, double value2, ResultMessage message)
        {
            return (!(value1 >= value2)) ? message : null;
        }

        public static ResultMessage LessOrEqual(int value1, int value2, ResultMessage message)
        {
            return (!(value1 <= value2)) ? message : null;
        }

        public static ResultMessage LessOrEqual(short value1, short value2, ResultMessage message)
        {
            return (!(value1 <= value2)) ? message : null;
        }

        public static ResultMessage LessOrEqual(long value1, long value2, ResultMessage message)
        {
            return (!(value1 <= value2)) ? message : null;
        }

        public static ResultMessage LessOrEqual(float value1, float value2, ResultMessage message)
        {
            return (!(value1 <= value2)) ? message : null;
        }

        public static ResultMessage LessOrEqual(decimal value1, decimal value2, ResultMessage message)
        {
            return (!(value1 <= value2)) ? message : null;
        }

        public static ResultMessage LessOrEqual(double value1, double value2, ResultMessage message)
        {
            return (!(value1 <= value2)) ? message : null;
        }

        public static ResultMessage Range(double value, double minimum, double maximum, ResultMessage message)
        {

            return (!(value >= minimum && value <= maximum)) ? message : null;
        }

        public static ResultMessage Range(float value, float minimum, float maximum, ResultMessage message)
        {
            return (!(value >= minimum && value <= maximum)) ? message : null;
        }

        public static ResultMessage Range(int value, int minimum, int maximum, ResultMessage message)
        {
            return (!(value >= minimum && value <= maximum)) ? message : null;
        }

        public static ResultMessage Range(long value, long minimum, long maximum, ResultMessage message)
        {
            return (!(value >= minimum && value <= maximum)) ? message : null;
        }
    }
}
