namespace LibraryToTest
{
    public class CountryType
    {
        public static readonly CountryType UnitedStates = new CountryType("USA");
        public static readonly CountryType Canada = new CountryType("CA");
        public static readonly CountryType France = new CountryType("FR");

        private readonly string _value;

        private CountryType(string value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}