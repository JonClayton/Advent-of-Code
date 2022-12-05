namespace AdventOfCode.Solutions2022;

public class Solution2022Dec05 : Solution
{
    protected override long FirstSolution(List<string> lines) => throw new NotImplementedException();
    protected override long SecondSolution(List<string> lines) => throw new NotImplementedException();
    protected override string FirstStringSolution(List<string> lines) => GeneralSolution(lines, true);
    protected override string SecondStringSolution(List<string> lines) => GeneralSolution(lines, false);

    private static string GeneralSolution(IEnumerable<string> lines, bool reverse)
    { 
        var stacks = new List<List<char>>();
        var readingStacks = true;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                var reversedStacks = new List<List<char>>();
                stacks.ForEach(stack =>
                {
                    stack.Reverse();
                    reversedStacks.Add(stack);
                });
                stacks = reversedStacks;
                readingStacks = false;
                continue;
            }

            if (readingStacks)
            {
                if (line[1] == '1') continue;
                for (var i = 0; i <= line.Length / 4; i ++)
                {
                    if (stacks.Count <= i) stacks.Add(new List<char>());
                    if (line[i * 4 + 1] != ' ') stacks[i].Add(line[i * 4 + 1]); 
                }
            }
            else
            {
                var parts = line.Split(" ");
                var boxCount = int.Parse(parts[1]);
                var from = stacks[int.Parse(parts[3]) - 1];
                var to = stacks[int.Parse(parts[5]) - 1];
                var boxesMoved = from.TakeLast(boxCount).ToList();
                if(reverse) boxesMoved.Reverse();
                to.AddRange(boxesMoved);
                from.RemoveRange(from.Count - boxCount, boxCount);
            }
        }

        return new string(stacks.Where(stack => stack.Any()).Select(stack => stack.ToList().Last()).ToArray());
    }
}