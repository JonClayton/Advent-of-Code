using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode.Tools;

public abstract partial class Solution<TType>
{
    [GeneratedRegex(@"^Solution\d{4}Dec\d{2}$")]
    private static partial Regex MyRegex();

    private readonly List<string> _puzzleInput;
    private readonly string _solutionName;
    private readonly Stopwatch _stopWatch = new();
    private readonly List<TType> _test1ExpectedList;
    private readonly List<TType> _test2ExpectedList;
    private readonly List<List<string>> _test1InputsList;
    private readonly List<List<string>> _test2InputsList;

    protected Solution()
    {
        _solutionName = GetType().Name;
        ConsoleInColor($"Solving {_solutionName}", ConsoleColor.Blue);
        var inputs = GetInputs();
        _puzzleInput = inputs.PuzzleInput.Split("\n").ToList();
        _test1ExpectedList = inputs.Test1Result is not null
            ? [inputs.Test1Result]
            : inputs.Test1Results;
        _test2ExpectedList = inputs.Test2Result is not null
            ? [inputs.Test2Result]
            : inputs.Test2Results;
        _test1InputsList = string.IsNullOrEmpty(inputs.TestInput)
            ? inputs.Test1Inputs.Select(input => input.Split("\n").ToList()).ToList()
            : [inputs.TestInput.Split("\n").ToList()];
        _test2InputsList = string.IsNullOrEmpty(inputs.TestInput)
            ? inputs.Test2Inputs.Select(input => input.Split("\n").ToList()).ToList()
            : _test1InputsList;
        if (_test1InputsList.Count==0) _test1InputsList.Add(inputs.Test1Input.Split("\n").ToList());
        if (_test2InputsList.Count==0) _test2InputsList.Add(inputs.Test2Input.Split("\n").ToList());
    }

    public void Run(bool skip = false)
    {
        if (skip)
        {
            ConsoleInColor("Skipping because this puzzle takes meaningful time to run", ConsoleColor.Yellow);
            return;
        }
        if (!TrySolution(true, out var firstSolution)) return;
        if (TrySolution(false, out var secondSolution))
        {
            ConsoleInColor(
                $"{_solutionName} solved in {_stopWatch.ElapsedMilliseconds}ms: first = {firstSolution} and second = {secondSolution}",
                ConsoleColor.DarkGreen);
            return;
        }

        ConsoleInColor(
            $"{_solutionName} part 1 solved in {_stopWatch.ElapsedMilliseconds}ms with solution: {firstSolution}",
            ConsoleColor.Yellow);
    }

    protected abstract TType FirstSolution(List<string> lines);
    protected abstract TType SecondSolution(List<string> lines);
    protected virtual void SolutionReset()
    {
    }

    private static void ConsoleInColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private Inputs<TType> GetInputs()
    {
        var result = new Inputs<TType>();
        if (!MyRegex().IsMatch(_solutionName))
            throw new ArgumentException("Invalid solution class name, must be 'Solution####Dec##'");
        try
        {
            var path = $"../../../../inputs/{_solutionName[8..12]}/inputs_{_solutionName[15..17]}.json";
            result = JsonSerializer.Deserialize<Inputs<TType>>(File.ReadAllText(path)) ?? result;
            result.Day = int.Parse(_solutionName[15..17]);
            if (result.Test1Result is null && result.Test1Results is null)
                throw new ArgumentException("test_1_result and test_1_results cannot both be null");
            if (result.Test2Result is null && result.Test2Results is null)
                throw new ArgumentException("test_2_result and test_2_results cannot both be null");
            if (string.IsNullOrEmpty(result.TestInput) 
                && ((result.Test1Inputs.Count == 0 && string.IsNullOrEmpty(result.Test1Input)) 
                    || (result.Test2Inputs.Count == 0 && string.IsNullOrEmpty(result.Test2Input))))
                throw new ArgumentException("test input must be present");
        }
        catch (DirectoryNotFoundException)
        {
            ConsoleInColor($"Directory {_solutionName[8..12]} not found in inputs directory", ConsoleColor.Red);
        }
        catch (FileNotFoundException)
        {
            ConsoleInColor($"File 'inputs_{_solutionName[15..17]}.json' not found in inputs directory",
                ConsoleColor.Red);
        }

        return result;
    }

    private void ReportFailedTest(bool isPart1, TType result, TType expectedResult, int iteration) => ConsoleInColor(
        $"Test for {_solutionName} part {(isPart1 ? 1 : 2)}: failed with actual={result} and expected={expectedResult}{(iteration > 0 ? $" on example #{iteration + 1}" : string.Empty)}",
        ConsoleColor.Red);

    private bool TrySolution(bool isPart1, out TType result)
    {
        Func<List<string>, TType> solution = isPart1 ? FirstSolution : SecondSolution;
        var inputs = isPart1 ? _test1InputsList : _test2InputsList;
        var expectedList = isPart1 ? _test1ExpectedList : _test2ExpectedList;
        for (var i = 0; i < inputs.Count; i++)
        {
            SolutionReset();
            result = solution(inputs[i]);
            var expected = expectedList[i];
            if (result.Equals(expected)) continue;
            ReportFailedTest(isPart1, result, expected, i);
            return false;
        }
        
        SolutionReset();
        _stopWatch.Start();
        result = solution(_puzzleInput);
        _stopWatch.Stop();
        return true;
    }

    // protected static List<int> ConvertToIntegerList(IEnumerable<string> strings) =>  strings.Select(int.Parse).ToList();
    //
    // protected static IEnumerable<List<int>> ConvertToIntegerLists(IEnumerable<string> strings, string separator = ",")
    // {
    //     var chunkedIntegers = new List<List<int>> { new() };
    //     foreach (var str in strings)
    //     {
    //         if (string.IsNullOrEmpty(str)) chunkedIntegers.Add([]);
    //         else
    //         {
    //             if (int.TryParse(str, out var value)) chunkedIntegers[^1].Add(value);
    //             else chunkedIntegers.Add(str.Split(separator).Select(int.Parse).ToList());
    //         }
    //     }
    //     if (chunkedIntegers.First().Count == 0) chunkedIntegers.RemoveAt(0);
    //     return chunkedIntegers;
    // }
}