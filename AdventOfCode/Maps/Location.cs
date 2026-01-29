namespace AdventOfCode.Maps;

public class Location<TType>(TType value)
{
    public Dictionary<Direction, Location<TType>> Neighbors { get; } = new();
    public Vector2 Coordinates { get; set; } = Vector2.Zero;
    public TType Value { get; set; } = value;
}