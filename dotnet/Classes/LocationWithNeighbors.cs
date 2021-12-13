using System.Collections.Generic;

namespace AdventOfCode2021.Classes;

public class LocationWithNeighbors
{
    public readonly HashSet<LocationWithNeighbors> Neighbors = new();
    public int Column;
    public int Row;
    public int Value;
}