using System.Text.RegularExpressions;

namespace ISteak.Commons.Results
{
    public class Assert
    {
        public static bool Length(string value, int minimum, int maximum)
        {
            var length = 0;
            if (!string.IsNullOrEmpty(value))
                length = value.Trim().Length;

            return (length >= minimum || length <= maximum);
        }

        public static bool LengthMinimum(string value, int minimum)
        {
            var length = 0;
            if (!string.IsNullOrEmpty(value))
                length = value.Trim().Length;

            return (length >= minimum);
        }

        public static bool Matches(string pattern, string value)
        {
            var regex = new Regex(pattern);

            return (regex.IsMatch(value));
        }

        public static bool NotNullOrEmpty(string value)
        {
            return (!string.IsNullOrEmpty(value));
        }

        public static bool Null(object obj)
        {
            return (obj == null);
        }

        public static bool NotNull(object obj)
        {
            return (obj != null);
        }

        public static bool True(bool boolValue)
        {
            return (boolValue);
        }

        public static bool False(bool boolValue)
        {
            return (!boolValue);
        }

        public static bool Equals(string value1, string value2)
        {
            return (value1 == value2);
        }

        public static bool Equals(int value1, int value2)
        {
            return (value1 == value2);
        }

        public static bool Equals(int? value1, int value2)
        {
            return (value1 == value2);
        }

        public static bool Equals(short value1, short value2)
        {
            return (value1 == value2);
        }

        public static bool Equals(short? value1, short value2)
        {
            return (value1 == value2);
        }

        public static bool Equals(long value1, long value2)
        {
            return (value1 == value2);
        }

        public static bool Equals(double value1, double value2)
        {
            return (value1 == value2);
        }

        public static bool Equals(Guid value1, Guid value2)
        {
            return (value1 == value2);
        }

        public static bool NotEquals(string value1, string value2)
        {
            return (value1 != value2);
        }

        public static bool NotEquals(int value1, int value2)
        {
            return (value1 != value2);
        }

        public static bool NotEquals(short value1, short value2)
        {
            return (value1 != value2);
        }

        public static bool NotEquals(long value1, long value2)
        {
            return (value1 != value2);
        }

        public static bool NotEquals(double value1, double value2)
        {
            return (value1 != value2);
        }

        public static bool NotEquals(Guid value1, Guid value2)
        {
            return (value1 != value2);
        }

        public static bool Greater(int value1, int value2)
        {
            return ((value1 > value2));
        }

        public static bool Greater(int? value1, int? value2)
        {
            return ((value1 > value2));
        }

        public static bool Greater(double value1, double value2)
        {
            return ((value1 > value2));
        }

        public static bool Greater(double? value1, double? value2)
        {
            return ((value1 > value2));
        }

        public static bool Greater(DateTime value1, DateTime value2)
        {
            return ((value1 > value2));
        }

        public static bool GreaterOrEqual(int value1, int value2)
        {
            return ((value1 >= value2));
        }

        public static bool GreaterOrEqual(short value1, short value2)
        {
            return ((value1 >= value2));
        }

        public static bool GreaterOrEqual(long value1, long value2)
        {
            return ((value1 >= value2));
        }

        public static bool GreaterOrEqual(float value1, float value2)
        {
            return ((value1 >= value2));
        }

        public static bool GreaterOrEqual(decimal value1, decimal value2)
        {
            return ((value1 >= value2));
        }

        public static bool GreaterOrEqual(double value1, double value2)
        {
            return ((value1 >= value2));
        }

        public static bool GreaterOrEqual(DateTime value1, DateTime value2)
        {
            return ((value1 >= value2));
        }

        public static bool LessOrEqual(DateTime value1, DateTime value2)
        {
            return ((value1 <= value2));
        }

        public static bool LessOrEqual(int value1, int value2)
        {
            return ((value1 <= value2));
        }

        public static bool LessOrEqual(short value1, short value2)
        {
            return ((value1 <= value2));
        }

        public static bool LessOrEqual(long value1, long value2)
        {
            return ((value1 <= value2));
        }

        public static bool LessOrEqual(float value1, float value2)
        {
            return ((value1 <= value2));
        }

        public static bool LessOrEqual(decimal value1, decimal value2)
        {
            return ((value1 <= value2));
        }

        public static bool LessOrEqual(double value1, double value2)
        {
            return ((value1 <= value2));
        }

        public static bool Range(double value, double minimum, double maximum)
        {
            return ((value >= minimum && value <= maximum));
        }

        public static bool Range(float value, float minimum, float maximum)
        {
            return ((value >= minimum && value <= maximum));
        }

        public static bool Range(int value, int minimum, int maximum)
        {
            return ((value >= minimum && value <= maximum));
        }

        public static bool Range(long value, long minimum, long maximum)
        {
            return ((value >= minimum && value <= maximum));
        }

        public static bool Exists(string value, IList<string> items)
        {
            return items != null && items.Any(item => item == value);
        }

        public static bool Exists(int value, IList<int> items)
        {
            return items != null && items.Any(item => item == value);
        }

        public static bool Exists(long value, IList<long> items)
        {
            return items != null && items.Any(item => item == value);
        }

        public static bool Exists(short value, IList<short> items)
        {
            return items != null && items.Any(item => item == value);
        }

        public static bool NotGuidEmpty(Guid value)
        {
            return value != Guid.Empty;
        }

        public static bool NotGuidNullOrEmpty(Guid? value)
        {
            return (value ?? Guid.Empty) != Guid.Empty;
        }

        public static bool NotGuidNullOrEmpty(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            if (!Guid.TryParse(value, out var guid))
                return false;

            return guid != Guid.Empty;
        }
    }
}
