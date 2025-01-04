using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions2015;

public partial class Solution2015Dec07 : Solution<long?>
{
    private readonly Dictionary<string, uint> _wireSignals = new();
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines.Append("46065 -> b").ToList());

    private long GeneralSolution(List<string> lines)
    {
        _wireSignals.Clear();
        var instructions = lines.Select(line =>line.Split(" -> ")).Select(halves => new Instruction(halves)).ToList();
        while (instructions.Count > 0)
            instructions = instructions.Where(instruction => !TryFollowInstruction(instruction)).ToList();
        return _wireSignals["a"];
    }

    private static uint TruncateTo16Bit(uint value) => value % uint.MaxValue;

    private uint GetValue(string input) => uint.TryParse(input, out var result) ? result : _wireSignals[input];

    private bool TryFollowInstruction(Instruction instruction)
    {
        try
        {
            _wireSignals[instruction.Target] = TruncateTo16Bit(instruction.Action switch
            {
                "RSHIFT" => GetValue(instruction.Remainder[0]) >> int.Parse(instruction.Remainder[1]), // OK
                "LSHIFT" => GetValue(instruction.Remainder[0]) << int.Parse(instruction.Remainder[1]), // OK
                "NOT" => 65535 - GetValue(instruction.Remainder[1]),
                "AND" => GetValue(instruction.Remainder[0]) & GetValue(instruction.Remainder[1]), // OK
                "OR" => GetValue(instruction.Remainder[0]) | GetValue(instruction.Remainder[1]),
                _ => GetValue(instruction.Remainder[0])
            });
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private partial class Instruction
    {
        [GeneratedRegex("([A-Z]+)")]
        private static partial Regex MyRegex();
        public Instruction(string[] inputs)
        {
            Target = inputs[1];
            var match = MyRegex().Match(inputs[0]);
            Action = match.Success ? match.Groups[1].Value : string.Empty;
            Remainder = inputs[0].Split(Action).Select(str => str.Trim()).ToList();
        }

        public string Action { get; }
        public List<string> Remainder { get; }
        public string Target { get; }
    }
}