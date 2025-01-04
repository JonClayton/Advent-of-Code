namespace AdventOfCode.Solutions2015;

public class Solution2015Dec01 : Solution<long?>
{
    private int _floor;
    protected override long? FirstSolution(List<string> lines) => lines.First().Aggregate(0, MoveToNextFloor);
    
    protected override long? SecondSolution(List<string> lines) => lines.First().TakeWhile(NotInBasement).Count() + 1;

    protected override void SolutionReset() => _floor = 0;

    private static int MoveToNextFloor(int lastFloor, char move) => lastFloor + (move == '(' ? 1 : -1);
    
    private bool NotInBasement(char c, int i)
    {
        _floor += c == '(' ? 1 : -1;
        return _floor != -1;
    }
}