using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AdventOfCode2021.Classes;

namespace AdventOfCode2021.Utilities
{

    public abstract class Solution
    {
        private readonly List<string> _actualLines;
        private readonly DateTimeOffset _createdAt;
        private readonly string _solution;
        private readonly long _firstTestResult;
        private long _result;
        private readonly long _secondTestResult;
        private readonly List<string> _testLines;

        protected Solution()
        {
            _createdAt = DateTimeOffset.UtcNow;
            _solution = GetType().Name;
            var jsonString = File.ReadAllText($"../../../../inputs/inputs_{_solution.Substring(8,2)}.json");
            var inputs = JsonSerializer.Deserialize<Inputs>(jsonString) ?? new Inputs();
            _actualLines = inputs.ActualInput.Split("\n").ToList();
            _firstTestResult = inputs.FirstTestResult;
            _secondTestResult = inputs.SecondTestResult;
            _testLines = inputs.TestInput.Split("\n").ToList();
        }

        public void StatusReport()
        {
            _result = FirstSolution(_testLines);
            if (_result != _firstTestResult) ReportFailedTest(1, _firstTestResult);
            else
            {
                var firstResult = FirstSolution(_actualLines);
                _result = SecondSolution(_testLines);
                if (_result != _secondTestResult)
                {
                    ConsoleInColor($"{_solution} part 1 is {firstResult}", ConsoleColor.Yellow);
                    ReportFailedTest(2, _secondTestResult);
                }
                else
                {
                    var secondResult = SecondSolution(_actualLines);
                    var elapsedTime = Math.Round((DateTimeOffset.UtcNow - _createdAt).TotalMilliseconds / 1000, 3);
                    ConsoleInColor($"{_solution} solved in {elapsedTime}s: first = {firstResult} and second = {secondResult}", ConsoleColor.DarkGreen);
                }
            }
        }

        private static void ConsoleInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text); 
            Console.ResetColor(); 
        }

        protected static List<int> ConvertToIntegers(IEnumerable<string> strings) => strings.Select(int.Parse).ToList();

        private void ReportFailedTest(int part, long expectedResult)
        {
            if (expectedResult.Equals(42)) ConsoleInColor($"{_solution} json has not been initialized yet", ConsoleColor.DarkGray);
            else ConsoleInColor(
                $"Test for {_solution} part {part}: failed with actual={_result} and expected={expectedResult}", ConsoleColor.Red);
        }

        protected abstract long FirstSolution(List<string> lines);
        protected abstract long SecondSolution(List<string> lines);
    }
}