namespace AdventOfCode.Solutions2022;

public class Solution2022Dec11 : Solution
{
    protected override long FirstSolution(List<string> lines) =>
        new MonkeyBusiness(lines).RunRounds(20, true).Calculate();
    protected override long SecondSolution(List<string> lines) =>
        new MonkeyBusiness(lines).RunRounds(10000, false).Calculate();

    private class Monkey
    {
        public readonly List<long> Inventory;
        public readonly Func<long,long> Operation;
        public readonly Func<long, int> Test;
        public long Inspections = 0;

        public Monkey(IReadOnlyList<string> chunk)
        {
            Inventory = chunk[1][18..].Split(",").Select(long.Parse).ToList();
            Operation = ReadOperation(chunk[2]);
            Test = (value) => int.Parse(chunk[value % long.Parse(chunk[3][21..]) == 0 ? 4 : 5][29..]);
        }
        
        private static Func<long, long> ReadOperation(string chunk)
        {
            if (chunk[23] == '+') return (value) => value + long.Parse(chunk[25..]);
            if (chunk[25] == 'o') return (value) => value * value;
            return (value) => value * long.Parse(chunk[25..]);
        }
    }

    private class MonkeyBusiness
    {
        private const long ValueDivisor = 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19 * 23;
        private readonly List<Monkey> _monkeys;

        public MonkeyBusiness(IEnumerable<string> lines)
        {
            _monkeys = lines.Chunk(7).Select(chunk => new Monkey(chunk)).ToList();
        }

        public MonkeyBusiness RunRounds(int count, bool isCalm)
        {
            while (count > 0)
            {
                count--;
                _monkeys.ForEach(monkey =>
                {
                    monkey.Inspections += monkey.Inventory.Count;
                    monkey.Inventory.Select(monkey.Operation)
                        .Select(worry => isCalm ? worry / 3 : worry % ValueDivisor).ToList()
                        .ForEach(item => _monkeys[monkey.Test(item)].Inventory.Add(item));
                    monkey.Inventory.Clear();
                });
            }

            return this;
        }

        public long Calculate() =>
            _monkeys.Select(monkey => monkey.Inspections)
                .OrderByDescending(i => i)
                .Take(2)
                .Product();
    }
}