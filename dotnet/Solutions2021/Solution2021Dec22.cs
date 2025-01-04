using System.Numerics;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec22 : Solution
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(IEnumerable<string> lines, bool isLimited)
    {
        var steps = lines
            .Select((line, index) => new RebootStep(line))
            .Where(StepInLimits)
            .ToList();
        var remainders = new List<RebootStep>();
        foreach (var step in steps)
        {
                remainders = remainders.SelectMany(existing => existing.RemoveCuboid(step)).ToList();
                remainders.Add(step);
        }

        return remainders.Select(s => s.CountCubesOn()).Sum();

        bool StepInLimits(RebootStep step)
        {
            if (!isLimited) return true;
            return step.MinVector.X < 51 && step.MinVector.Y < 51 && step.MinVector.Z < 51 && step.MaxVector.X > -51 &&
                   step.MaxVector.Y > -51 && step.MaxVector.Z > -51;
        } 
    }

    private class RebootStep
    {
        private bool _isOn;
        public Vector3 MinVector;
        public Vector3 MaxVector;

        private RebootStep()
        {
        }

        public RebootStep(string line)
        {
            var spaceParts = line.Split(" ");
            _isOn = spaceParts[0] == "on";
            var coordinateRanges = spaceParts[1].Split(",").Select(s => s.Split("=")[1].Split(".."))
                .Select(arr => arr.Select(int.Parse).OrderBy(i => i).ToList()).ToList();
            MinVector = new Vector3(coordinateRanges[0][0], coordinateRanges[1][0], coordinateRanges[2][0]);
            MaxVector = new Vector3(coordinateRanges[0][1], coordinateRanges[1][1], coordinateRanges[2][1]);
        }
        
        
        public long CountCubesOn()
        {
            if (!_isOn) return 0;
            var vector = Vector3.Subtract(MaxVector, MinVector);
            return ((long)vector.X + 1) * ((long)vector.Y + 1) * ((long)vector.Z + 1);
        }

        public IEnumerable<RebootStep> RemoveCuboid(RebootStep step)
        {
            var minVector = Vector3.Max(MinVector, step.MinVector);
            var maxVector = Vector3.Min(MaxVector, step.MaxVector);
            var subtractedVector = Vector3.Subtract(maxVector, minVector);
            if (!subtractedVector.Equals(Vector3.Abs(subtractedVector))) return new List<RebootStep> { this };
            if (subtractedVector.Equals(Vector3.Subtract(MaxVector, MinVector))) return new List<RebootStep>();
            var result = new List<RebootStep>();
            if (minVector.X > MinVector.X)
                result.Add(new RebootStep
                {
                    MinVector = MinVector,
                    MaxVector = new Vector3(minVector.X - 1, MaxVector.Y, MaxVector.Z)
                });
            if (maxVector.X < MaxVector.X)
                result.Add(new RebootStep
                {
                    MinVector = new Vector3(maxVector.X + 1, MinVector.Y, MinVector.Z),
                    MaxVector = MaxVector
                });
            if (minVector.Y > MinVector.Y)
                result.Add(new RebootStep
                {
                    MinVector = new Vector3(minVector.X, MinVector.Y, MinVector.Z),
                    MaxVector = new Vector3(maxVector.X, minVector.Y - 1, MaxVector.Z)
                });
            if (maxVector.Y < MaxVector.Y)
                result.Add(new RebootStep
                {
                    MinVector = new Vector3(minVector.X, maxVector.Y + 1, MinVector.Z),
                    MaxVector = new Vector3(maxVector.X, MaxVector.Y, MaxVector.Z)
                });
            if (minVector.Z > MinVector.Z)
                result.Add(new RebootStep
                {
                    MinVector = new Vector3(minVector.X, minVector.Y, MinVector.Z),
                    MaxVector = new Vector3(maxVector.X, maxVector.Y, minVector.Z - 1)
                });
            if (maxVector.Z < MaxVector.Z)
                result.Add(new RebootStep
                {
                    MinVector = new Vector3(minVector.X, minVector.Y, maxVector.Z + 1),
                    MaxVector = new Vector3(maxVector.X, maxVector.Y, MaxVector.Z)
                });
            result.ForEach(r => r._isOn = _isOn);
            return result;
        }
    }
}