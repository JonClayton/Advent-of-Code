using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utilities;
namespace AdventOfCode2021.Solutions;

public class Solution03 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var result = MostCommonAtEachIndex(lines);
        var gamma = CalculateBinaryValue(result);
        var epsilon = CalculateBinaryValue(InvertBinary(result));
        return gamma * epsilon;
    }

    protected override long SecondSolution(List<string> lines)
    {
        var oxygenGeneratorRating = FindMostSimilar(lines, false);
        var c02ScrubberRating = FindMostSimilar(lines, true);
        return CalculateBinaryValue(oxygenGeneratorRating) * CalculateBinaryValue(c02ScrubberRating);
    }

    private static int CalculateBinaryValue(string str) =>
        str.Select(c => c.Equals('1') ? (byte)1 : (byte)0).ToList().ToInteger();

    private static string FindMostSimilar(List<string> candidates, bool isInverted)
    {
        var i = 0;
        while (candidates.Count > 1)
        {
            var target = isInverted
                ? InvertBinary(MostCommonAtEachIndex(candidates))
                : MostCommonAtEachIndex(candidates);
            candidates = candidates.Where(s => s[i].Equals(target[i])).ToList();
            i++;
        }

        return candidates.First();
    }

    private static string InvertBinary(string binaries) =>
        new string(binaries.Select(b => b.Equals('1') ? '0' : '1').ToArray());

    private static string MostCommonAtEachIndex(List<string> observations) =>
        new (observations.First().Select((_, index) => observations.Count(observation =>
            observation[index].Equals('1')) >= observations.Count / 2.0
            ? '1'
            : '0').ToArray());
}