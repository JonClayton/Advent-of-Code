namespace AdventOfCode.Maps;

public class Map<TType, TType2> where TType : Location<TType2>
{
    public Map(IEnumerable<string> input, Func<char, TType> locationFactory, bool assignNeighbors = false)
    {
        var lines = input.ToList();
        YRange = lines.Count;
        XRange = lines[0].Length;
        Locations = CreateDirectory(lines.SelectMany((line, y) =>
            line.Select((c, x) => CreateLocation(x, y, c, locationFactory))));
        if (assignNeighbors) AssignNeighbors();
    }

    public Map(int xRange, int yRange, Func<TType> locationFactory, bool assignNeighbors = false)
    {
        XRange = xRange;
        YRange = yRange;
        Locations = CreateDirectory(Enumerable.Range(0, xRange).SelectMany(x =>
            Enumerable.Range(0, yRange).Select(y => CreateLocation(x, y, ' ', _ => locationFactory()))));
        if (assignNeighbors) AssignNeighbors();
    }

    public int XRange { get; }
    public int YRange { get; }

    public Dictionary<Vector2, TType> Locations { get; }

    private static TType CreateLocation(int x, int y, char c, Func<char, TType> locationFactory)
    {
        var location = locationFactory(c);
        location.Coordinates = new Vector2(x, y);
        return location;
    }

    private static Dictionary<Vector2, TType> CreateDirectory(IEnumerable<TType> locations)
    {
        return locations.ToDictionary(location => location.Coordinates, location => location);
    }

    // public bool TryMove(TType location, Direction direction, out TType? result) =>
    //     Locations.TryGetValue(location.Coordinates + Tools.Moves[direction], out result);

    private void AssignNeighbors()
    {
        foreach (var location in Locations.Values)
        foreach (var (direction, vector) in Tools.Moves)
            if (Locations.TryGetValue(location.Coordinates - vector, out var neighbor))
                neighbor.Neighbors.Add(direction, location);
    }
}