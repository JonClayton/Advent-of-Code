using System.Collections.Generic;
using System.Linq;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec18 : Solution
{
    private readonly HashSet<string> NonNumbers = new() { "[", "]", "," };
    protected override long FirstSolution(List<string> lines)
    {
        var sum = lines.Aggregate(SnailFishAdd);
        return CalculateMagnitude(sum.ToCharArray().Select(c => c.ToString()).ToList());
    }

    private string SnailFishAdd(string snailA, string snailB)
    {
        var result = $"[{snailA},{snailB}]".ToCharArray().Select(c => c.ToString()).ToList();
        while (TryReduce(result, out var reducedValue)) result = reducedValue;
        return string.Join(string.Empty, result);
    }

    private bool TryReduce(List<string> snailNum, out List<string> reducedSnailNum) => 
        TryExplode(snailNum, out reducedSnailNum) || TrySplit(snailNum, out reducedSnailNum);

    private bool TryExplode(List<string> snailNum, out List<string> reducedSnailNum)
    {
        reducedSnailNum = snailNum;
        var i = 0;
        var depth = 0;
        while (i < snailNum.Count)
        {
            depth += snailNum[i] switch
            {
                "[" => 1,
                "]" => -1,
                _ => 0
            };
            if (depth == 5)
            {
                var leftNum = int.Parse(snailNum[i + 1]);
                var rightNum = int.Parse(snailNum[i + 3]);
                snailNum.RemoveRange(i,4);
                snailNum[i] = "0";
                var l = i;
                while (l > 0)
                {
                    l--;
                    if (NonNumbers.Contains(snailNum[l])) continue;
                    snailNum[l] = $"{leftNum + int.Parse(snailNum[l])}";
                    break;
                }
                while (i < snailNum.Count - 1)
                {
                    i++;
                    if (NonNumbers.Contains(snailNum[i])) continue;
                    snailNum[i] = $"{rightNum + int.Parse(snailNum[i])}";
                    break;
                }
                reducedSnailNum = snailNum;
                return true;
            }
            i++;
        }
        return false;
    }

    private bool TrySplit(List<string> snailNum, out List<string> reducedSnailNum)
    {
        reducedSnailNum = snailNum;
        var i = 0;
        while (i < snailNum.Count - 1)
        {
            i++;
            if (NonNumbers.Contains(reducedSnailNum[i])) continue;
            var num = int.Parse(reducedSnailNum[i]);
            if (num < 10) continue;
            reducedSnailNum.RemoveAt(i);
            var a = num / 2;
            reducedSnailNum.InsertRange(i, new List<string> { "[", $"{a}", ",", $"{num - a}", "]" });
            return true;
        }

        return false;
    }

    private long CalculateMagnitude(List<string> snailNum)
    {
        while (true)
        {
            var i = 0;
            while (i < snailNum.Count-4)
            {
                if (IsNumPair(snailNum.GetRange(i, 5)))
                {
                    var magnitude = 3 * int.Parse(snailNum[i + 1]) + 2 * int.Parse(snailNum[i + 3]);
                    snailNum[i] = magnitude.ToString();
                    snailNum.RemoveRange(i+1, 4);
                    break;
                }

                i++;
            }

            if (snailNum.Count == 1) return int.Parse(snailNum.First());
        }
    }

    private bool IsNumPair(List<string> segment)
    {
        if (segment.Count != 5) return false;
        if (segment[0] != "[") return false;
        if (NonNumbers.Contains(segment[1])) return false;
        if (segment[2] != ",") return false;
        if (NonNumbers.Contains(segment[3])) return false;
        return segment[4] == "]";
    }

    protected override long SecondSolution(List<string> lines)
    {
        long max = 0;
        for (var i = 0; i < lines.Count; i++)
        {
            for (var j = 0; j < lines.Count; j++)
            {
                if (i == j) continue;
                var snailNum = SnailFishAdd(lines.ElementAt(i), lines[j]);
                var sum = CalculateMagnitude(snailNum.ToCharArray().Select(s => s.ToString()).ToList());
                max = sum > max ? sum : max;
            }
        }

        return max;
    }
}
