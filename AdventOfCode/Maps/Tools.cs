using System.Numerics;

namespace AdventOfCode.Maps;

public class Tools
{
    public static readonly Dictionary<Direction, Vector2> Moves = new()
    {
        { Direction.N, -Vector2.UnitY },
        { Direction.Ne, Vector2.UnitX - Vector2.UnitY },
        { Direction.E, Vector2.UnitX },
        { Direction.Se, Vector2.UnitX + Vector2.UnitY },
        { Direction.S, Vector2.UnitY },
        { Direction.Sw, Vector2.UnitY - Vector2.UnitX },
        { Direction.W, -Vector2.UnitX },
        { Direction.Nw, -Vector2.UnitX - Vector2.UnitY }
    };
}