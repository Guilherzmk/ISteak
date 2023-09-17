using System.Reflection;

namespace ISteak.Commons.Types.Generics
{
    public class CodeName : IComparable
    {
        public int Code { get; set; }
        public string Name { get; set; }

        public CodeName() { }

        public CodeName(int code, string name)
        {
            (Code, Name) = (code, name);
        }
 
        public override string ToString() => Name;

        protected static IEnumerable<T> GetAll<T>() where T : CodeName =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
 
        protected static T Get<T>(int? code) where T : CodeName
        {
            return GetAll<T>()?.FirstOrDefault(_code => _code == code);
        }

        public override bool Equals(object obj)
        {
            if (obj is not CodeName otherValue)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Code.Equals(otherValue.Code);

            return typeMatches && valueMatches;
        }

        public static implicit operator int(CodeName _this)
        {
            return _this?.Code ?? 0;
        }

        public static implicit operator string(CodeName _this)
        {
            return _this?.Name;
        }
 
        public int CompareTo(object other) => Code.CompareTo(((CodeName)other).Code);
    }
}