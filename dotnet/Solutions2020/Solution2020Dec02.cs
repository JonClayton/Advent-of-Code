using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2020;

public class Solution2020Dec02 : Solution
{
    protected override long FirstSolution(List<string> lines) =>
        lines.Select(line => new PasswordChecker(line)).Count(pc => pc.IsValidA());

    protected override long SecondSolution(List<string> lines) => 
        lines.Select(line => new PasswordChecker(line)).Count(pc => pc.IsValidB());

    private class PasswordChecker
    {
        private string _letter;
        private int _secondNum;
        private int _firstNum;
        private string _password;

        public PasswordChecker(string input)
        {
            var parts = input.Split(":");
            var parts1 = parts[0].Split(" ");
            var nums = parts1[0].Split("-").Select(int.Parse).ToList();
            _firstNum = nums[0];
            _secondNum = nums[1];
            _letter = parts1[1];
            _password = parts[1].Trim();
        }

        public bool IsValidA()
        {
            var count = _password.ToCharArray().Count(c => _letter.Equals(c.ToString()));
            return count >= _firstNum && count <= _secondNum;
        }

        public bool IsValidB()
        {
            var condition1 = _password[_firstNum - 1].ToString() == _letter;
            var condition2 = _password[_secondNum - 1].ToString() == _letter;
            return condition1 && !condition2 || !condition1 && condition2;
        }
    }
}
