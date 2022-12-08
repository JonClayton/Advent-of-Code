namespace AdventOfCode.Solutions2022;

public class Solution2022Dec08 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var trees = Tree.ReadTrees(lines);
        return trees.Count(tree =>
        {
            var rowTrees = trees.Where(t => t.X.Equals(tree.X)).ToList();
            if (!rowTrees.Any(t => t.Y < tree.Y && t.Height >= tree.Height)) return true;
            if (!rowTrees.Any(t => t.Y > tree.Y && t.Height >= tree.Height)) return true;
            var colTrees = trees.Where(t => t.Y.Equals(tree.Y)).ToList();
            if (!colTrees.Any(t => t.X < tree.X && t.Height >= tree.Height)) return true;
            return !colTrees.Any(t => t.X > tree.X && t.Height >= tree.Height);
        });
    }

    protected override long SecondSolution(List<string> lines)
    {
        var trees = Tree.ReadTrees(lines);
        var maxY = lines.Count - 1;
        var maxX = lines.First().Length - 1;
        return trees.Select(tree =>
        {
            var blockingTrees = trees
                .Where(t => t.Height >= tree.Height && (t.X == tree.X || t.Y == tree.Y)).ToList();
            var upY = (blockingTrees.Where(t => t.X == tree.X && t.Y > tree.Y).MinBy(t => t.Y)?.Y ?? maxY) - tree.Y;
            var downY = tree.Y - (blockingTrees.Where(t => t.X == tree.X && t.Y < tree.Y).MaxBy(t => t.Y)?.Y ?? 0);
            var upX = (blockingTrees.Where(t => t.Y == tree.Y && t.X > tree.X).MinBy(t => t.X)?.X ?? maxX) - tree.X;
            var downX = tree.X - (blockingTrees.Where(t => t.Y == tree.Y && t.X < tree.X).MaxBy(t => t.X)?.X ?? 0);
            return upX * upY * downX * downY;
        }).Max();
    }
}
    public class Tree
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }

        public Tree(int x, int y, char h)
        {
            X = x;
            Y = y;
            Height = int.Parse(h.ToString());
        }
        
        public static List<Tree> ReadTrees(List<string> lines) => lines.Aggregate((0, new List<Tree>()), (pair, line) =>
        {
            var (row, trees) = pair;
            for (var col = 0; col < line.Length; col++)
            {
                trees.Add(new Tree(col, row, line[col]));
            }

            row++;
            return (row, trees);
        }).Item2;
    }