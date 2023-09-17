namespace ISteak.Commons.Types.Generics
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public IList<KeyValue> Values { get; set; }

        public KeyValue()
        {
            Values = new List<KeyValue>();
        }

        public KeyValue(string key, string value)
        {
            Values = new List<KeyValue>();
            Key = key;
            Value = value;
        }

        public KeyValue(string key, string value, IList<KeyValue>? values)
        {
            Key = key;
            Value = value;
            Values = values ?? new List<KeyValue>();
        }
    }
}
