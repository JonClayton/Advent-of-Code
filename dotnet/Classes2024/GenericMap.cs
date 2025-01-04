namespace OldAdventOfCode.Classes2024;

public class GenericMap<TType, TType2> where TType : GenericLocation<TType2>, new()
{
    public static Dictionary<Direction, (int, int)> Moves = new()
    {
        [Direction.N] = (0, -1),
        [Direction.Ne] = (1, -1),
        [Direction.E] = (1, 0),
        [Direction.Se] = (1, 1),
        [Direction.S] = (0, 1),
        [Direction.Sw] = (-1, 1),
        [Direction.W] = (-1, 0),
        [Direction.Nw] = (-1, -1),
    };

    public bool TryMove(Direction direction, TType location, out TType destination)
    {
        return Locations.TryGetValue((location.Column + Moves[direction].Item1, location.Row + Moves[direction].Item2),
            out destination);
    }
    
    public bool TryMove((int, int) vector ,TType location, out TType destination)
    {
        return Locations.TryGetValue((location.Column + vector.Item1, location.Row + vector.Item2), out destination);
    }

    public readonly Dictionary<(int, int), TType> Locations = new();


    public GenericMap()
    {
        
    }

    public GenericMap(IEnumerable<string> lines)
    {
        Locations = lines.Aggregate((new Dictionary<(int, int), TType>(),0), (pair, line) =>
        {
            Enumerable.Range(0, line.Length).ToList().ForEach(column => pair.Item1.Add((column, pair.Item2), CreateLocation(column, pair.Item2, line[column])));
            pair.Item2++;
            return pair;
        }).Item1;
        foreach (var location in Locations) AssignToNeighbors(location);
    }

    private void AssignToNeighbors(KeyValuePair<(int, int), TType> location)
    {
        foreach (var move in Moves)
        {
            // if (!TryMove(move.Value, location.Key, out var destination)) continue;
        }
    }

    protected TType CreateLocation(int column, int row, char value)
    {
        var result = new TType
        {
            Column = column,
            Row = row,
            Value = ConvertChar(column, row, value)
        };
        return result;
    }
    
    protected TType2 ConvertChar(int column, int row, char value)
    {
        return value is TType2 type2 ? type2 : default;
    }

    public static Direction Right90(Direction direction) => direction switch
    {
        Direction.N => Direction.E,
        Direction.Ne => Direction.Se,
        Direction.E => Direction.S,
        Direction.Se => Direction.Sw,
        Direction.S => Direction.W,
        Direction.Sw => Direction.Nw,
        Direction.W => Direction.N,
        Direction.Nw => Direction.Ne,
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
    };
}