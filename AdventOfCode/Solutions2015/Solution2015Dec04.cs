using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions2015;

public class Solution2015Dec04 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var result = 0L;
        var byteZero = byte.Parse("0");
        while (true)
        {
            var bytes = MD5.HashData(Encoding.UTF8.GetBytes($"{lines.First()}{result}"));
            if (bytes[0] == byteZero 
                && bytes[1] == byteZero 
                && (isFirstSolution ? Convert.ToHexString(bytes[2..3]).StartsWith('0') : bytes[2] == byteZero))
                return result;
            result++;
        }
    }
}