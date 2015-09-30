namespace Reference.Common.Utils
{
    public static class Pluralizer
    {
        public static string Pluralize(this string noun)
        {
            if (noun.EndsWith("e")) return noun + "r";
            if (noun.EndsWith("er")) return noun + "e";
            return noun + "er";
        }
    }
}