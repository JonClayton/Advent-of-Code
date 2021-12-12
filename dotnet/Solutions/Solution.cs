using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AdventOfCode.Solutions
{

    public abstract class Solution
    {
        private readonly List<string> _actualLines;
        private readonly DateTimeOffset _createdAt;
        private readonly string _solution;
        private readonly int _firstTestResult;
        private int _result;
        private readonly int _secondTestResult;
        private readonly List<string> _testLines;

        protected Solution()
        {
            _createdAt = DateTimeOffset.UtcNow;
            _solution = GetType().Name;
            var jsonString = File.ReadAllText($"../../../../inputs/inputs_{_solution.Last()}.json");
            var inputs = JsonSerializer.Deserialize<Inputs>(jsonString) ?? new Inputs();
            _actualLines = inputs.ActualInput.Split("\n").ToList();
            _firstTestResult = inputs.FirstTestResult;
            _secondTestResult = inputs.SecondTestResult;
            _testLines = inputs.TestInput.Split("\n").ToList();
        }

        public void StatusReport()
        {
            _result = FirstSolution(_testLines);
            if (_result != _firstTestResult) ReportFailedTest(1, _secondTestResult);
            else
            {
                var firstResult = FirstSolution(_actualLines);
                _result = SecondSolution(_testLines);
                if (_result != _secondTestResult)
                {
                    Console.WriteLine($"{_solution} part 1 is {firstResult}");
                    ReportFailedTest(2, _secondTestResult);
                }
                else
                {
                    var secondResult = SecondSolution(_actualLines);
                    var elapsedTime = Math.Round((DateTimeOffset.UtcNow - _createdAt).TotalMilliseconds / 1000, 3);
                    Console.WriteLine(
                        $"{_solution} solved in {elapsedTime}s: first = {firstResult} and second = {secondResult}");
                }
            }
        }

        protected static List<int> ConvertToIntegers(IEnumerable<string> strings) => strings.Select(int.Parse).ToList();

        private void ReportFailedTest(int part, int expectedResult)
        {
            Console.WriteLine(
                $"Test for {_solution} part {part}: failed with actual={_result} and expected={expectedResult}");
        }

        protected abstract int FirstSolution(List<string> lines);
        protected abstract int SecondSolution(List<string> lines);
    }
}