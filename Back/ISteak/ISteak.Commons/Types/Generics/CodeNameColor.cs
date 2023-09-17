using System.Reflection;

namespace ISteak.Commons.Types.Generics
{
    public class CodeNameColor : IComparable
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public CodeNameColor() { }

        public CodeNameColor(int code, string name, string color)
        {
            (Code, Name, Color) = (code, name, color);
        }

         
        public override string ToString() => Name;

        protected static IEnumerable<T> GetAll<T>() where T : CodeNameColor =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();


        protected static T Get<T>(int? code) where T : CodeNameColor
        {
            return GetAll<T>()?.FirstOrDefault(_code => _code == code);
        }

        public override bool Equals(object obj)
        {
            if (obj is not CodeNameColor otherValue)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Code.Equals(otherValue.Code);

            return typeMatches && valueMatches;
        }

        public static implicit operator int(CodeNameColor _this)
        {
            return _this?.Code ?? 0;
        }

        public static implicit operator string(CodeNameColor _this)
        {
            return _this?.Name;
        }

        public int CompareTo(object other) => Code.CompareTo(((CodeNameColor)other).Code);
    }
}



