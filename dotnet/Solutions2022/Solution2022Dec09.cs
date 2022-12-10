using System.Numerics;

namespace AdventOfCode.Solutions2022;

public class Solution2022Dec09 : Solution
{
    private readonly Dictionary<char, Vector2> _moves = new Dictionary<char, Vector2>()
    {
        {'D', new Vector2(0,-1)},
        {'L', new Vector2(-1,0)},
        {'R', new Vector2(1,0)},
        {'U', new Vector2(0,1)},
    };
    
    private readonly Vector2 _negativeOne = new Vector2(-1, -1);
    private readonly Vector2 _positiveOne = new Vector2(1, 1);

    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, 2);
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, 10);
    
    private long GeneralSolution(List<string> lines, int knotCount)
    {

        var knots = Enumerable.Range(0, knotCount).Select(i => new Vector2(0, 0)).ToList();
        var tailPositions = new HashSet<Vector2> { knots.Last() };
        lines.SelectMany(line =>
        {
            {
                var moveCountArray = Enumerable.Range(0, int.Parse(line.Split(" ")[1])); 
                var result = moveCountArray.Select(i => _moves[line[0]]);
                return result;
            }
        }).ToList().ForEach(move => 
        {
            for (var i = 0; i < knotCount - 1; i++)
            {
                knots[i] = Vector2.Add(knots[i], move);
                var nextKnotRelativePosition = knots[i] - knots[i + 1];
                var nextKnotMove = new Vector2(0, 0);
                while (nextKnotRelativePosition.Length() >= 2)
                {
                    var incrementalMove = Vector2.Clamp(nextKnotRelativePosition, _negativeOne, _positiveOne);
                    nextKnotMove = Vector2.Add(nextKnotMove, incrementalMove);
                    nextKnotRelativePosition = Vector2.Add(nextKnotRelativePosition, -incrementalMove);
                    if (i == knotCount - 2)
                    {
                        tailPositions.Add(knots[i] - nextKnotRelativePosition);
                       // if (knotCount > 3) Console.WriteLine($"Last: {knots[i] - nextKnotRelativePosition}");
                    }
                }

                move = nextKnotMove;
            }
            knots[^1] += move;

        });
        return tailPositions.Count;
    }
}
