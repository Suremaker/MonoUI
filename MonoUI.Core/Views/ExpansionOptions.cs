namespace MonoUI.Core.Views
{
    public struct ExpansionOptions
    {
        public short Value { get; private set; }
        public static ExpansionOptions Fixed = new ExpansionOptions(0);
        public static ExpansionOptions Expand = new ExpansionOptions(1);
        public static ExpansionOptions WithLevel(byte level) { return new ExpansionOptions((short)(level + 1)); }

        private ExpansionOptions(short value)
            : this()
        {
            Value = value;
        }

        public override string ToString()
        {
            if (Equals(this, Fixed)) return "fixed";
            if (Equals(this, Expand)) return "expand";
            return Value.ToString();
        }

        public bool Equals(ExpansionOptions other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ExpansionOptions && Equals((ExpansionOptions)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(ExpansionOptions p1,ExpansionOptions p2)
        {
            return p1.Value == p2.Value;
        }

        public static bool operator !=(ExpansionOptions p1, ExpansionOptions p2)
        {
            return !(p1 == p2);
        }
    }
}