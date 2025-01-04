namespace AdventOfCode.Maps;

public class Location<TType>(int x, int y, TType value)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public TType Value { get; set; } = value;
}