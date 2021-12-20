using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Classes;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec11 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var map = new OctopusCavern(lines);
        Enumerable.Range(0, 100).ToList().ForEach(_ => map.RunStep());
        return map.FlashCount;
    }

    protected override long SecondSolution(List<string> lines)
    {
        var map = new OctopusCavern(lines);
        var step = 1;
        while (!map.RunStepAndCheckForAllFlash()) step++;
        return step;
    }
}

public class Octopus : LocationWithNeighbors
{
    public int FlashCount;

    public void CaptureEnergy()
    {
        Value++;
        if (Value == 10) Flash();
    }

    public void EndStep()
    {
        if (Value > 9) Value = 0;
    }

    private void Flash()
    {
        FlashCount++;
        foreach (var neighbor in Neighbors)
            ((Octopus)neighbor).CaptureEnergy();
    }
}

public class OctopusCavern : MapWithNeighbors<Octopus>
{
    private readonly List<(int, int)> DiagonalNeighborMoves = new() { (-1, 1), (-1, -1), (1, -1), (1, 1) };

    public OctopusCavern(IReadOnlyList<string> lines) : base(lines)
    {
    }

    public int FlashCount => Locations.Sum(location => location.Value.FlashCount);

    protected override void AssignNeighbors(Octopus octopus)
    {
        NeighborMoves.AddRange(DiagonalNeighborMoves);
        base.AssignNeighbors(octopus);
    }

    public void RunStep()
    {
        foreach (var location in Locations) location.Value.CaptureEnergy();
        foreach (var location in Locations) location.Value.EndStep();
    }

    public bool RunStepAndCheckForAllFlash()
    {
        RunStep();
        return Locations.All(location => location.Value.Value == 0);
    }
}