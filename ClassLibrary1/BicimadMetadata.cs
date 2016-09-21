namespace Bicimad.Helpers
{
    public static class BicimadMetadata
    {
        private static double _basePrice = 5;

       public static double BasePrice
        {
            get { return _basePrice; }
            set { _basePrice = value; }
        }
    }
}