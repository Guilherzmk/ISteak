using System.Reflection;

namespace ISteak.Commons.Types.Generics
{
    public class StringCodeName : IComparable
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public StringCodeName()
        {
        }

        public StringCodeName(string code, string name)
        {
            (Code, Name) = (code, name);
        }
 
        public override string ToString() => Name;

        protected static IEnumerable<T> GetAll<T>() where T : StringCodeName =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();


        protected static T Get<T>(string code) where T : StringCodeName
        {
            return GetAll<T>()?.FirstOrDefault(_code => _code == code);
        }


        public override bool Equals(object obj)
        {
            if (obj is not StringCodeName otherValue)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Code.Equals(otherValue.Code);

            return typeMatches && valueMatches;
        }

        public static implicit operator string(StringCodeName _this)
        {
            return _this?.Code;
        }

        public int CompareTo(object other) => string.Compare(Code, ((StringCodeName)other).Code, StringComparison.Ordinal);
    }
}