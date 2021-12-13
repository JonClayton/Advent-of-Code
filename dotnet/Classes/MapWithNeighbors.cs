using System.Collections.Generic;

namespace AdventOfCode2021.Classes;

public class MapWithNeighbors<TType> where TType : LocationWithNeighbors, new()
{
    protected readonly Dictionary<(int, int), TType> Locations = new();
    protected readonly List<(int, int)> NeighborMoves = new() { (0, 1), (0, -1), (1, 0), (-1, 0) };

    protected MapWithNeighbors(IReadOnlyList<string> lines)
    {
        for (var row = 0; row < lines.Count; row++)
        for (var col = 0; col < lines[0].Length; col++)
            Locations.Add((row, col), new TType
            {
                Row = row,
                Column = col,
                Value = int.Parse($"{lines[row][col]}")
            });

        foreach (var point in Locations.Values) AssignNeighbors(point);
    }

    protected virtual void AssignNeighbors(TType location)
    {
        NeighborMoves.ForEach(move =>
        {
            var (item1, item2) = move;
            if (Locations.TryGetValue((location.Row + item1, location.Column + item2), out var neighbor))
                location.Neighbors.Add(neighbor);
        });
    }
}