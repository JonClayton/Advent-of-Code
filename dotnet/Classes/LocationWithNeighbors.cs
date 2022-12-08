namespace AdventOfCode.Classes;

public class LocationWithNeighbors
{
    public HashSet<LocationWithNeighbors> Neighbors = new();
    public int Column;
    public int Row;
    public int Value;
}