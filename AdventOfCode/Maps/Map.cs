using System.Numerics;

namespace AdventOfCode.Maps;

public class Map<TType, TType2>(IEnumerable<TType> locations) where TType : Location<TType2>
{
    public Dictionary<Vector2,TType> Locations { get; } = locations.ToDictionary(location => new Vector2(location.X, location.Y), location => location);

}