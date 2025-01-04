namespace OldAdventOfCode.Classes2024;

public class GenericLocation<TType>
{
    public int Column { get; set; } 
    public int Row { get; set; }
    public TType Value { get; set; }

    public Dictionary<Direction, GenericLocation<TType>> Neighbors { get; } = new();

    public GenericLocation()
    {
        
    }

    public GenericLocation(int column, int row, TType value)
    {
        Column = column;
        Row = row;
        Value = value;
    }
}