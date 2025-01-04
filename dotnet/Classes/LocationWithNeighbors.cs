namespace OldAdventOfCode.Classes;

public class LocationWithNeighbors
{
    public HashSet<LocationWithNeighbors> Neighbors = [];
    public int Column;
    public int Row;
    public int Value;
}