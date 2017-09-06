namespace HumanArc.Compliance.Shared.Helpers
{
    public static class StringHelper
    {
        public static string RemoveDomainPrefix(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
                
            return str.Replace(@"ACOR\", string.Empty);
        }
    }
}
