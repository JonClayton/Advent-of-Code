using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions2015;

public class Solution2015Dec19 : Solution<long?>
{
    private List<(string, string)> _replacements = [];
    private long _currentLow = long.MaxValue;
    private HashSet<string> _alreadyChecked = [];
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var parts = Separate(lines);
        _replacements = parts[0].Select(line => line.Split(" => ")).Select(pair => (pair[0], pair[1])).ToList();
        var molecule = parts[1][0];
        if (isFirstSolution)
            return _replacements.Aggregate(new HashSet<string>(),
                (set, replacement) => set.AddRangeAndReturn(new Regex(replacement.Item1).Matches(molecule).Select(
                    match =>
                        molecule[..match.Index] + replacement.Item2 + molecule[(match.Index + match.Length)..]))).Count;
        _currentLow = 250;
        _alreadyChecked.Clear();
        ReduceToE(molecule);
        return _currentLow;
    }

    private void ReduceToE(string molecule, int reductionCount = 0)
    {
        Console.WriteLine($"{molecule.Length}, {reductionCount}");
        if (molecule == "e")
        {
            _currentLow = _currentLow > reductionCount ? reductionCount : _currentLow;
            return;
        }
        
        if (reductionCount >= _currentLow || !_alreadyChecked.Add(molecule)) return;

        foreach (var (replacement, reduction) in _replacements)
        {
            var matches = new Regex(reduction).Matches(molecule);
            foreach (Match match in matches)
            {
                ReduceToE(molecule[..match.Index] + replacement + molecule[(match.Index + match.Length)..],
                    reductionCount + 1);
            }
        }
    }
}