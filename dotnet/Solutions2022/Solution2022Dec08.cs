using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2022;

public class Solution2022Dec08 : Solution
{
    protected override long FirstSolution(List<string> lines) => new Forest(lines).CountVisibleTrees();
    protected override long SecondSolution(List<string> lines) => new Forest(lines).FindMaxScenicScore();
}

public class Forest
{
    public readonly Dictionary<(int, int), Tree> Trees = new();
    public readonly int Height;
    public readonly int Width;

    public Forest(IReadOnlyList<string> lines)
    {
        Height = lines.Count;
        Width = lines[0].Length;
        for (var row = 0; row < Height; row++)
        for (var col = 0; col < Width; col++)
            Trees.Add((col, row), new Tree(col, row, lines[row][col], this));
    }

    public int CountVisibleTrees() => Trees.Values.Count(tree => tree.CanBeSeenFromEdge());
    public int FindMaxScenicScore() => Trees.Values.Select(tree => tree.ScenicScore()).Max();
}

public class Tree
{
    private readonly int _x;
    private readonly int _y;
    private readonly int _height;
    private readonly Forest _forest;
    
    public Tree(int x, int y, char c, Forest forest)
    {
        _x = x;
        _y = y;
        _height = int.Parse(c.ToString());
        _forest = forest;
    }
    
    public bool CanBeSeenFromEdge() => Views.Any(view => _height > (view.MaxBy(t => t._height)?._height ?? -1));
    public int ScenicScore() => Views.Aggregate(1, (product, view) =>
    {
        var countToNearestBlockingTree = view.FindIndex(t => t._height >= _height) + 1;
        return product * (countToNearestBlockingTree == 0 ? view.Count : countToNearestBlockingTree);
    });
    
    private List<Tree> DownView =>
        Enumerable.Range(_y + 1, _forest.Height - _y - 1).Select(y => _forest.Trees[(_x, y)]).ToList();
    private List<Tree> LeftView => 
        _x == 0 ? new List<Tree>() : Enumerable.Range(0, _x).Reverse().Select(x => _forest.Trees[(x, _y)]).ToList();
    private List<Tree> RightView =>
        Enumerable.Range(_x + 1, _forest.Width - _x - 1).Select(x => _forest.Trees[(x, _y)]).ToList();
    private List<Tree> UpView =>
        _y == 0 ? new List<Tree>() : Enumerable.Range(0, _y).Reverse().Select(y => _forest.Trees[(_x, y)]).ToList();
    private List<List<Tree>> Views => new() { DownView, LeftView, RightView, UpView };
}