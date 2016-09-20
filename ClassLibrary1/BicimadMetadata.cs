namespace Bicimad.Helpers
{
    public static class BicimadMetadata
    {
        private static int _basePrice = 5;

        public static int BicimadBasePrice
        {
            get { return BasePrice; }
        }

        public static int BasePrice
        {
            get { return _basePrice; }
            set { _basePrice = value; }
        }
    }
}