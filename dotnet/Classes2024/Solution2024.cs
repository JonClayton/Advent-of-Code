using System.Text.Json;

namespace OldAdventOfCode.Classes2024;

public abstract class Solution2024
{
    private readonly DateTimeOffset _createdAt;
    private readonly List<string> _inputsList;
    private readonly string _solutionName;
    private readonly List<string> _testInputsList;
    private readonly AdventOfCodeJson _data;

    protected Solution2024()
    {
        _createdAt = DateTimeOffset.UtcNow;
        _solutionName = GetType().Name;
        ConsoleInColor($"Testing {_solutionName}", ConsoleColor.Blue);
        var jsonString = File.ReadAllText($"../../../../inputs/{_solutionName[8..12]}/inputs_{_solutionName[15..17]}.json");
        _data = JsonSerializer.Deserialize<AdventOfCodeJson>(jsonString) ?? new AdventOfCodeJson();
        _inputsList = _data.Input.Split("\n").ToList();
        _testInputsList = _data.TestInput.Split("\n").ToList();
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
        var testResult = FirstSolution(_testInputsList);
        if (testResult.Equals(_data.Test1Result)) return FirstSolution(_inputsList);
        ReportFailedTest("1", testResult, _data.Test1Result);
        return false;
    }

    private dynamic CalculateSecondSolution()
    {
        var testResult = SecondSolution(_testInputsList);
        if (testResult.Equals(_data.Test2Result)) return SecondSolution(_inputsList);
        ReportFailedTest("1", testResult, _data.Test2Result);
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

    private void ReportFailedTest(string part, long result, long expectedResult) => ConsoleInColor(
        $"Test for {_solutionName} part {part}: failed with actual={result} and expected={expectedResult}",
        ConsoleColor.Red);
    
    protected abstract long FirstSolution(List<string> lines);
    protected abstract long SecondSolution(List<string> lines);
}