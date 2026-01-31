namespace AdventOfCode.Solutions2015;

public class Day01(bool skip = false) : Solution<long?>(skip)
{
    private int _floor;

    protected override long? FirstSolution(List<string> lines)
    {
        return lines.First().Aggregate(0, MoveToNextFloor);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return lines.First().TakeWhile(NotInBasement).Count() + 1;
    }

    protected override void SolutionReset()
    {
        _floor = 0;
    }

    private static int MoveToNextFloor(int lastFloor, char move)
    {
        return lastFloor + (move == '(' ? 1 : -1);
    }

    private bool NotInBasement(char c, int i)
    {
        _floor += c == '(' ? 1 : -1;
        return _floor != -1;
    }
}