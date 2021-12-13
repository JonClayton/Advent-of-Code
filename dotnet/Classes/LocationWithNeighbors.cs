using System.Collections.Generic;

namespace AdventOfCode2021.Classes;

public class LocationWithNeighbors
{
    public int Row;
    public int Column;
    public int Value;
    public readonly HashSet<LocationWithNeighbors> Neighbors = new();
}