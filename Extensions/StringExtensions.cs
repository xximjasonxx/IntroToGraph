namespace GraphDemo.Extensions
{
    public static class StringExtensions
    {
        public static int? AsInt(this string str)
        {
            int intValue;
            if (int.TryParse(str, out intValue) == false)
                return null;

            return intValue;
        }
    }
}