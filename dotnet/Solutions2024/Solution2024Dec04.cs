using OldAdventOfCode.Classes2024;

namespace OldAdventOfCode.Solutions2024;

public class Solution2024Dec04 : Solution2024
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(IEnumerable<string> lines, bool isFirst)
    {
        var list = lines.ToList();
        var height = list.Count;
        var width = list[0].Length;
        List<List<int>> xIndexes = [[width - 1, 1 - width], [width + 1, -1 - width]];
        HashSet<char> msHash = ['M', 'S'];
        var chars = string.Join("", list).ToList();
        var count = 0;
        for (var i = 0; i < chars.Count; i++)
        {
            var lineIndex = i % width;
            var rowIndex = i / width;
            if (isFirst)
            {
                if (chars[i] != 'X') continue;
                var hasSpaceRight = width - lineIndex > 3;
                var hasSpaceLeft = lineIndex > 2;
                var hasSpaceDown = height - rowIndex > 3;
                var hasSpaceUp = rowIndex > 2;
                if (hasSpaceRight)
                {
                    if (IsXmas(i, 1)) count++;
                    if (hasSpaceDown && IsXmas(i, 1 + width)) count++;
                    if (hasSpaceUp && IsXmas(i, 1 - width)) count++;
                }

                if (hasSpaceLeft)
                {
                    if (IsXmas(i, -1)) count++;
                    if (hasSpaceDown && IsXmas(i, width - 1)) count++;
                    if (hasSpaceUp && IsXmas(i, -1 - width)) count++;
                }

                if (hasSpaceDown && IsXmas(i, width)) count++;
                if (hasSpaceUp && IsXmas(i, -width)) count++;
            }
            else
            {
                if (chars[i] != 'A' || width - lineIndex == 1 || lineIndex == 0 || rowIndex == 0 || height - rowIndex == 1) continue;
                if(xIndexes.All(pair => msHash.SetEquals([chars[i + pair[0]], chars[i + pair[1]]]))) count++;
            }
        }

        return count;

        bool IsXmas(int index, int step) => chars[index + step] == 'M' &&
                                            chars[index + 2 * step] == 'A' &&
                                            chars[index + 3 * step] == 'S';
    }
}