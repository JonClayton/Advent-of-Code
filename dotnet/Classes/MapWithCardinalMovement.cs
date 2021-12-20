using System;
using System.Collections.Generic;

namespace AdventOfCode.Classes;

public class MapWithCardinalMovement
{
    public enum Direction
    {
        N,
        E,
        S,
        W
    }

    private Direction _direction;
    public (int, int) Location;

    private readonly Dictionary<Direction, (int, int)> Vectors = new()
    {
        { Direction.N, (0, 1) },
        { Direction.E, (1, 0) },
        { Direction.S, (0, -1) },
        { Direction.W, (-1, 0) },
    };

    public MapWithCardinalMovement()
    {
    }
    
    public MapWithCardinalMovement((int, int) startingPoint)
    {
        Location = startingPoint;
    }

    public void Move(int distance) =>
        Move((Vectors[_direction].Item1 * distance, Vectors[_direction].Item2 * distance));

    public void Move((int, int) vector) => Location = (Location.Item1 + vector.Item1, Location.Item2 + vector.Item2);

    public void SetDirection(string input)
    {
        var shortCap = input[0..1].ToUpper();
        switch (shortCap)
        {
            case "U":
            case "N":
                _direction = Direction.N;
                break;
            case "D":
            case "S":
                _direction = Direction.S;
                break;
            case "L":
            case "W":
                _direction = Direction.W;
                break;
            case "E":
            case "R":
                _direction = Direction.E;
                break;
            default:
                throw new Exception("invalid direction argument");
        }
    }

    public void SetDirection(Direction direction)
    {
        _direction = direction;
    }

    public void RotateLeft() => _direction = _direction switch
    {
        Direction.E => Direction.N,
        Direction.N => Direction.W,
        Direction.W => Direction.S,
        Direction.S => Direction.E,
        _ => throw new ArgumentOutOfRangeException()
    };

    public void RotateRight() => _direction = _direction switch
    {
        Direction.E => Direction.S,
        Direction.N => Direction.E,
        Direction.W => Direction.N,
        Direction.S => Direction.W,
        _ => throw new ArgumentOutOfRangeException()
    };

    public double DistanceFromOrigin(bool isDiagonalAllowed = true) => Distance(Location, (0, 0), isDiagonalAllowed);

    public static double Distance((int, int) point1, (int, int) point2, bool isDiagonalAllowed = true)
    {
        var (x1, y1) = point1;
        var (x0, y0) = point2;
        if (!isDiagonalAllowed) return (double)Math.Abs(x1 - x0) + Math.Abs(y1 - y0);
        var x = x1 - x0;
        var y = y1 - y0;
        return Math.Sqrt(x * x + y * y);
    }
}