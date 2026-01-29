namespace AdventOfCode.Tools.Extensions;

public static class StringExtensions
{
    extension(string source)
    {
        public string RemoveWhitespace()
        {
            var builder = new StringBuilder(source.Length);
            foreach (var c in source.Where(c => !char.IsWhiteSpace(c)))
                builder.Append(c);
            return source.Length == builder.Length ? source : builder.ToString();
        }

        public string ConcatInOrder(string b)
        {
            return string.Compare(source, b, StringComparison.Ordinal) < 0 ? source + b : b + source;
        }
    }
}