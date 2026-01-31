using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace AdventOfCode.Solutions2015;

public class Day04 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var result = 0L;
        var byteZero = byte.Parse("0", CultureInfo.InvariantCulture);
        while (true)
        {
            var bytes = UseBrokenAlgorithm(result, lines);
            if (bytes[0] == byteZero
                && bytes[1] == byteZero
                && (isFirstSolution ? Convert.ToHexString(bytes[2..3]).StartsWith('0') : bytes[2] == byteZero))
                return result;
            result++;
        }
    }

    [SuppressMessage("Microsoft.Performance", "CA5351: Do Not Use Broken Cryptographic Algorithms")]
    private static byte[] UseBrokenAlgorithm(long result, List<string> lines)
    {
        return MD5.HashData(Encoding.UTF8.GetBytes($"{lines.First()}{result}"));
    }
}