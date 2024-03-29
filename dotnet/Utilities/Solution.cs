using System.Text.Json;

namespace AdventOfCode.Utilities;

public abstract class Solution
{
    private readonly List<string> _actualInput;
    private readonly DateTimeOffset _createdAt;
    private readonly List<List<string>> _firstTestInputsList;
    private readonly List<long> _firstTestResults;
    private readonly string _firstTestResultString;
    private readonly List<string> _secondActualInput;
    private readonly List<List<string>> _secondTestInputsList;
    private readonly List<long> _secondTestResults;
    private readonly string _secondTestResultString;
    private readonly string _solutionName;

    protected Solution()
    {
        _solutionName = GetType().Name;
        var jsonString = File.ReadAllText($"../../../../inputs/{_solutionName[8..12]}/inputs_{_solutionName[15..17]}.json");
        var inputs = JsonSerializer.Deserialize<AdventOfCodeInputs>(jsonString) ?? new AdventOfCodeInputs();
        _actualInput = inputs.ActualInput.Split("\n").ToList();
        _secondActualInput = inputs.SecondActualInput?.Split("\n").ToList() ?? _actualInput;
        _firstTestInputsList = inputs.TestInputs.Select(input => input.Split("\n").Select(s => s.Replace("\r", string.Empty)).ToList()).ToList();
        _firstTestResults = inputs.FirstTestResults;
        _firstTestResultString = inputs.FirstTestResultString;
        _secondTestInputsList = inputs.SecondTestInputs?.Select(input => input.Split("\n").ToList()).ToList();
        _secondTestResultString = inputs.SecondTestResultString;
        _secondTestResults = inputs.SecondTestResults ?? new List<long>{inputs.SecondTestResult};
        if (!_firstTestInputsList.Any())
        {
            _firstTestInputsList.Add(inputs.TestInput?.Split("\n").ToList());
            _firstTestResults.Add(inputs.FirstTestResult);
        }
        _createdAt = DateTimeOffset.UtcNow;
        if (_secondTestInputsList != null) return;
        _secondTestInputsList = inputs.SecondTestInput != null
            ? new List<List<string>> { inputs.SecondTestInput?.Split("\n").ToList() }
            : _firstTestInputsList;
    }

    public void StatusReport()
    {
        if (FirstTestsFail()) return;
        var didSecondTestsFail = SecondTestsFail();
        var firstResult = string.IsNullOrWhiteSpace(_firstTestResultString)
            ? FirstSolution(_actualInput).ToString()
            : FirstStringSolution(_actualInput);
        if (didSecondTestsFail)
        {
            ConsoleInColor($"{_solutionName} part 1 is {firstResult}", ConsoleColor.Yellow);
            return;
        }

        var secondResult = string.IsNullOrWhiteSpace(_secondTestResultString)
            ? SecondSolution(_secondActualInput).ToString()
            : SecondStringSolution(_secondActualInput);
        var elapsedTime = Math.Round((DateTimeOffset.UtcNow - _createdAt).TotalMilliseconds / 1000, 3);
        ConsoleInColor(
            $"{_solutionName} solved in {elapsedTime}s: first = {firstResult} and second = {secondResult}",
            ConsoleColor.DarkGreen);
    }

    private bool FirstTestsFail()
    {
        if (!string.IsNullOrEmpty(_firstTestResultString))
        {
            var result = FirstStringSolution(_firstTestInputsList[0]); 
            if (result.Equals(_firstTestResultString)) return false;
            ReportFailedTest("1", result, _firstTestResultString);
            return true;
        }
        for (var i = 0; i < _firstTestInputsList.Count(); i++)
        {
            var result = FirstSolution(_firstTestInputsList[i]);
            if (result == _firstTestResults[i]) continue;
            var part = _firstTestInputsList.Count() > 1 ? $"1.{i}" : "1";
            ReportFailedTest(part, result.ToString(), _firstTestResults[i].ToString());
            return true;
        }

        return false;
    }
    
    private bool SecondTestsFail()
    {
        if (!string.IsNullOrEmpty(_firstTestResultString))
        {
            var result = SecondStringSolution(_secondTestInputsList[0]); 
            if (result.Equals(_secondTestResultString)) return false;
            ReportFailedTest("2", result, _secondTestResultString);
            return true;
        }
        for (var i = 0; i < _secondTestInputsList.Count(); i++)
        {
            var result = SecondSolution(_secondTestInputsList[i]);
            if (result == _secondTestResults[i]) continue;
            var part = _secondTestInputsList.Count() > 1 ? $"2.{i}" : "2";
            ReportFailedTest(part, result.ToString(), _secondTestResults[i].ToString());
            return true;
        }

        return false;
    }

    private static void ConsoleInColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    protected static List<int> ConvertToIntegers(IEnumerable<string> strings)
    {
        return strings.Select(int.Parse).ToList();
    }
    
    protected static IEnumerable<List<int>> ConvertToChunkedIntegers(IEnumerable<string> strings)
    {
        var chunkedIntegers = new List<List<int>> { new() };
        foreach (var str in strings)
            if (string.IsNullOrEmpty(str)) chunkedIntegers.Add(new List<int>());
            else chunkedIntegers[^1].Add(int.Parse(str));
        return chunkedIntegers;
    }

    private void ReportFailedTest(string part, string result, string expectedResult)
    {
        if (expectedResult.Equals(-1))
            ConsoleInColor($"{_solutionName} part {part} json has not been initialized yet", ConsoleColor.DarkGray);
        else
            ConsoleInColor(
                $"Test for {_solutionName} part {part}: failed with actual={result} and expected={expectedResult}",
                ConsoleColor.Red);
    }

    protected abstract long FirstSolution(List<string> lines);
    protected abstract long SecondSolution(List<string> lines);
    protected virtual string FirstStringSolution(List<string> lines) => null;
    protected virtual string SecondStringSolution(List<string> lines) => null;
}