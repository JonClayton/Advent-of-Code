using System.Collections.Generic;
using System.Linq;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec08 : Solution
{
    private static readonly List<int> _uniqueCharacterCounts = new() { 2, 3, 4, 7 };

    protected override long FirstSolution(List<string> lines)
    {
        return lines.Select(SecondHalf).SelectMany(CharacterCount).Where(DigitIsKnownFromCharacterCount).Count();
    }

    protected override long SecondSolution(List<string> lines)
    {
        return lines.Select(line => new BustedDisplay(line).InstallAdapter().InterpretReading()).Sum();
    }

    private static IEnumerable<int> CharacterCount(IEnumerable<string> strings)
    {
        return strings.Select(s => s.Length);
    }

    private static bool DigitIsKnownFromCharacterCount(int arg)
    {
        return _uniqueCharacterCounts.Contains(arg);
    }

    private static IEnumerable<string> SecondHalf(string line)
    {
        return line.Split(" | ").Last().Split();
    }
}

public class BustedDisplay
{
    private static readonly Dictionary<string, int> CorrectWiringScheme = new()
    {
        { "ABCEFG", 0 },
        { "CF", 1 },
        { "ACDEG", 2 },
        { "ACDFG", 3 },
        { "BCDF", 4 },
        { "ABDFG", 5 },
        { "ABDEFG", 6 },
        { "ACF", 7 },
        { "ABCDEFG", 8 },
        { "ABCDFG", 9 }
    };

    private readonly List<string> _encodedReading;
    private readonly List<string> _sortedSamples;
    private readonly Dictionary<char, char> _wiringInterface = new();

    public BustedDisplay(string line)
    {
        var parts = line.Split(" | ").Select(part => part.Split().ToList()).ToList();
        _sortedSamples = parts.First().Select(Sort).ToList();
        _encodedReading = parts.Last().Select(Sort).ToList();
    }

    public BustedDisplay InstallAdapter()
    {
        var segmentTally = _sortedSamples.SelectMany(s => s).GroupBy(s => s).ToList();
        AddSegmentsWithUniqueCountsToAdapter(segmentTally);
        AddSegmentsWithSevenTallies(segmentTally);
        AddSegmentsWithEightTallies(segmentTally);
        return this;
    }

    public int InterpretReading()
    {
        return _encodedReading
            .Select(ApplyWiringInterface)
            .Select(ConvertSegmentsToDigit)
            .ToList()
            .ToInteger();
    }

    private string ApplyWiringInterface(string s)
    {
        return new(s.Select(c => _wiringInterface[c])
            .OrderBy(c => c)
            .ToArray());
    }

    private static int ConvertSegmentsToDigit(string s)
    {
        return CorrectWiringScheme[s];
    }

    private void AddSegmentsWithEightTallies(IEnumerable<IGrouping<char, char>> segmentTally)
    {
        var options = segmentTally.Where(t => t.Count().Equals(8)).Select(g => g.Key).ToList();
        var segmentsInDigit1 = _sortedSamples.Single(s => s.Length.Equals(2));
        _wiringInterface.Add(options.Single(x => segmentsInDigit1.Contains(x)), 'C');
        _wiringInterface.Add(options.Single(x => !segmentsInDigit1.Contains(x)), 'A');
    }

    private void AddSegmentsWithSevenTallies(IEnumerable<IGrouping<char, char>> segmentTally)
    {
        var options = segmentTally.Where(t => t.Count().Equals(7)).Select(g => g.Key).ToList();
        var segmentsInDigit4 = _sortedSamples.Single(s => s.Length.Equals(4));
        _wiringInterface.Add(options.Single(x => segmentsInDigit4.Contains(x)), 'D');
        _wiringInterface.Add(options.Single(x => !segmentsInDigit4.Contains(x)), 'G');
    }

    private void AddSegmentsWithUniqueCountsToAdapter(List<IGrouping<char, char>> segmentTally)
    {
        _wiringInterface.Add(segmentTally.Single(t => t.Count().Equals(6)).Key, 'B');
        _wiringInterface.Add(segmentTally.Single(t => t.Count().Equals(4)).Key, 'E');
        _wiringInterface.Add(segmentTally.Single(t => t.Count().Equals(9)).Key, 'F');
    }

    private static string Sort(string str)
    {
        return string.Concat(str.OrderBy(c => c));
    }
}