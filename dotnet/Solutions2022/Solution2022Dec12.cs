using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2022;

public class Solution2022Dec12 : Solution
{
    protected override long FirstSolution(List<string> lines) => new ClimbingMap(lines).CalculatePathFromEntry(false);
    protected override long SecondSolution(List<string> lines) => new ClimbingMap(lines).CalculatePathFromEntry(true);
}

public class ClimbingMap
{
    private readonly (int, int) _start;
    private readonly (int, int) _goal;
    private readonly Dictionary<(int, int), LocationWithElevation> _locations = new();
    private readonly List<(int, int)> _neighborMoves = new() { (0, 1), (0, -1), (1, 0), (-1, 0) };

    public ClimbingMap(IReadOnlyList<string> lines)
    {
        for (var row = 0; row < lines.Count; row++)
        for (var col = 0; col < lines[0].Length; col++)
        {
            var value = lines[row][col] - 96;
            if (value == -13)
            {
                _start = (col, row);
                value = 1;
            }
            if (value == -27)
            {
                _goal = (col, row);
                value = 26;
            }
            _locations.Add((col, row), new LocationWithElevation
            {
                Row = row,
                Column = col,
                Value = value
            });
        }

        foreach (var point in _locations.Values) AssignNeighbors(point);
    }

    private void AssignNeighbors(LocationWithElevation location)
    {
        _neighborMoves.ForEach(move =>
        {
            var (item1, item2) = move;
            if (_locations.TryGetValue((location.Column + item1, location.Row + item2), out var neighbor))
                location.Neighbors.Add(neighbor);
        });
    }
    public long CalculatePathFromEntry(bool IsAnyStartAllowed)
    {
        var locations = new List<LocationWithElevation> { _locations[_goal] };
        var distance = 0;
        while (true)
        {
            locations.ForEach(location => location.DistanceFromGoal = distance);
            distance++;
            locations = locations.SelectMany(location => location.Neighbors.Where(neighbor =>
                neighbor.Value - location.Value > -2 && !neighbor.DistanceFromGoal.HasValue)).ToHashSet().ToList();
            if (locations.Contains(_locations[_start]) || (IsAnyStartAllowed && locations.Any(neighbor => neighbor.Value == 1))) return distance;
        }
    }
}

public class LocationWithElevation
{
    public readonly HashSet<LocationWithElevation> Neighbors = new();
    public int Column;
    public int Row;
    public int Value;
    public long? DistanceFromGoal;
}