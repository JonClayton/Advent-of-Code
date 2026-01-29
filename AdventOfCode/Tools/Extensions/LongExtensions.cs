namespace AdventOfCode.Tools.Extensions;

public static class LongExtensions
{
    extension(long)
    {
        /// <summary>Converts the string representation of a number in invariant culture to its 64-bit signed integer equivalent.</summary>
        /// <param name="s">A string containing a number to convert.</param>
        /// <returns>A 64-bit signed integer equivalent to the number specified in s.</returns>
        public static long Read(string s)
        {
            return long.Parse(s, CultureInfo.InvariantCulture);
        }
    }
}