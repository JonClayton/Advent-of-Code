#nullable enable
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec24 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, 96918996924991);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, 91811241911641);
    
    private long GeneralSolution(List<string> lines, long modelNumber)
    {
        var instructions = lines.Select(line => new Instruction(line)).ToList();
        var alu = new ArithmeticLogicUnit(instructions);
        return alu.Validate($"{modelNumber}") ? modelNumber : lines.Count;
    }

    private class ArithmeticLogicUnit
    {
        private readonly List<List<Instruction>> _instructionSets = new();

        private readonly Dictionary<string, long> _memory =
            new List<string> { "w", "x", "y", "z" }.ToDictionary(x => x, _ => (long)0);

        public ArithmeticLogicUnit(List<Instruction> instructions)
        {
            var nextInstructionSet = new List<Instruction>();
            foreach (var instruction in instructions)
            {
                if (instruction.Action.Equals("inp"))
                {
                    if (nextInstructionSet.Any()) _instructionSets.Add(nextInstructionSet);
                    nextInstructionSet = new List<Instruction> { instruction };
                }
                else nextInstructionSet.Add(instruction);
            }

            _instructionSets.Add(nextInstructionSet);
        }

        public bool Validate(string modelNumber)
        {
            var inputs = new Stack<int>(modelNumber.ToCharArray().Reverse().Select(c => int.Parse(c.ToString())));
            foreach (var instructionSet in _instructionSets)
            {
                var zValue = _memory["z"];
                // Console.WriteLine(
                //     $"Z starts at: {zValue} and only input of {zValue % 26 + int.Parse(instructionSet[5].B)} will reduce the Z value");
                foreach (var instruction in instructionSet)
                {
                    if (instruction.Action.Equals("inp"))
                    {
                        _memory[instruction.A] = inputs.Pop();
                        // Console.WriteLine($"Using value {_memory[instruction.A]}");
                    }
                    else
                    {
                        if (!_memory.TryGetValue(instruction.B, out var inputB)) inputB = int.Parse(instruction.B);
                        _memory[instruction.A] = instruction.Action switch
                        {
                            "add" => _memory[instruction.A] + inputB,
                            "mul" => _memory[instruction.A] * inputB,
                            "div" => _memory[instruction.A] / inputB,
                            "mod" => _memory[instruction.A] % inputB,
                            "eql" => _memory[instruction.A].Equals(inputB) ? 1 : 0,
                            _ => throw new ArgumentOutOfRangeException(instruction.Action)
                        };
                    }
                }
            }

            // Console.WriteLine($"Z value is {_memory["z"]}");
            return _memory["z"].Equals(0);
        }
    }

    private class Instruction
    {
        public readonly string Action;
        public readonly string A;
        public readonly string B;

        public Instruction(string line)
        {
            var parts = line.Split(" ");
            Action = parts[0];
            A = parts[1];
            B = Action == "inp" ? "" : parts[2];
        }

        public override string? ToString() => $"{Action} {A} {B}".Trim();
    }
}