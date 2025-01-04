namespace OldAdventOfCode.Classes;

public class Map<TType> where TType : Location, new()
{
    protected readonly Dictionary<(int, int), TType> Locations = new();

    protected Map()
    {
        
    }

    protected Map(IEnumerable<string> lines)
    {
        Locations = lines.Aggregate((new Dictionary<(int, int), TType>(),0), (pair, line) =>
        {
            Enumerable.Range(0, line.Length).ToList().ForEach(CreateLocation);
            return pair;
            void CreateLocation(int column) => pair.Item1.Add((column, pair.Item2), new TType { Column = column, Row = pair.Item2, Value = int.Parse($"{line[column]}") });
        }).Item1;
    }
}