using System.Text.Json;
using OldAdventOfCode.Classes;

namespace OldAdventOfCode.Utilities;

public abstract class Solution
{
    private readonly List<string> _actualInput;
    private readonly DateTimeOffset _createdAt;
    private readonly List<List<string>> _firstTestInputsList;
    private readonly List<long> _firstTestResults;
    private readonly string _firstTestResultString;
    // private readonly List<string> _secondActualInput;
    private readonly List<List<string>> _secondTestInputsList;
    private readonly List<long> _secondTestResults;
    private readonly string _secondTestResultString;
    private readonly string _solutionName;

    protected Solution()
    {
        _solutionName = GetType().Name;
        ConsoleInColor($"Testing {_solutionName}", ConsoleColor.Blue);

        var jsonString = File.ReadAllText($"../../../../inputs/{_solutionName[8..12]}/inputs_{_solutionName[15..17]}.json");
        var inputs = JsonSerializer.Deserialize<AdventOfCodeInputs>(jsonString) ?? new AdventOfCodeInputs();
        _actualInput = inputs.ActualInput.Split("\n").ToList();
        _firstTestInputsList = inputs.TestInputs.Select(input => input.Split("\n").Select(s => s.Replace("\r", string.Empty)).ToList()).ToList();
        _firstTestResults = inputs.FirstTestResults;
        _firstTestResultString = inputs.FirstTestResultString;
        _secondTestInputsList = inputs.SecondTestInputs?.Select(input => input.Split("\n").ToList()).ToList();
        _secondTestResultString = inputs.SecondTestResultString;
        _secondTestResults = inputs.SecondTestResults ?? [inputs.SecondTestResult];
        if (_firstTestInputsList.Count == 0)
        {
            _firstTestInputsList.Add(inputs.TestInput?.Split("\n").ToList());
            _firstTestResults.Add(inputs.FirstTestResult);
        }
        _createdAt = DateTimeOffset.UtcNow;
        if (_secondTestInputsList != null) return;
        _secondTestInputsList = inputs.SecondTestInput != null
            ? [inputs.SecondTestInput?.Split("\n").ToList()]
            : _firstTestInputsList;
    }

    public void StatusReport()
    {
        var firstSolution = CalculateFirstSolution();
        if (firstSolution is false) return;
        var secondSolution = CalculateSecondSolution();
        var elapsedTime = Math.Round((DateTimeOffset.UtcNow - _createdAt).TotalMilliseconds / 1000, 3);
        if (secondSolution is not bool)
            ConsoleInColor(
                $"{_solutionName} solved in {elapsedTime}s: first = {firstSolution} and second = {secondSolution}",
                ConsoleColor.DarkGreen);
        else ConsoleInColor($"{_solutionName} part 1 is {firstSolution}", ConsoleColor.Yellow);
    }

    private dynamic CalculateFirstSolution()
    {
        var testResult = FirstSolution(_firstTestInputsList[0]);
        if (testResult.Equals(_firstTestResults[0])) return FirstSolution(_actualInput);
        ReportFailedTest("1", testResult.ToString(), _firstTestResults[0].ToString());
        return false;
    }

    private dynamic CalculateSecondSolution()
    {
        var testResult = SecondSolution(_secondTestInputsList[0]);
        if (testResult.Equals(_secondTestResults[0])) return SecondSolution(_actualInput);
        ReportFailedTest("2", testResult.ToString(), _secondTestResults[0].ToString());
        return false;
    }

    private static void ConsoleInColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    protected static List<int> ConvertToIntegerList(IEnumerable<string> strings) =>  strings.Select(int.Parse).ToList();
    
    protected static IEnumerable<List<int>> ConvertToIntegerLists(IEnumerable<string> strings, string separator = ",")
    {
        var chunkedIntegers = new List<List<int>> { new() };
        foreach (var str in strings)
        {
            if (string.IsNullOrEmpty(str)) chunkedIntegers.Add([]);
            else
            {
                if (int.TryParse(str, out var value)) chunkedIntegers[^1].Add(value);
                else chunkedIntegers.Add(str.Split(separator).Select(int.Parse).ToList());
            }
        }
        if (chunkedIntegers.First().Count == 0) chunkedIntegers.RemoveAt(0);
        return chunkedIntegers;
    }

    private void ReportFailedTest(string part, string result, string expectedResult) => ConsoleInColor(
        $"Test for {_solutionName} part {part}: failed with actual={result} and expected={expectedResult}",
        ConsoleColor.Red);
    
    protected abstract long FirstSolution(List<string> lines);
    protected abstract long SecondSolution(List<string> lines);
    protected virtual string FirstStringSolution(List<string> lines) => null;
    protected virtual string SecondStringSolution(List<string> lines) => null;
}