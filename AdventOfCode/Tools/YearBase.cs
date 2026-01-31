namespace AdventOfCode.Tools;

public abstract class YearBase
{
    public void Run(bool skip = false)
    {
        if (skip) return;
        Console.WriteLine();
        Console.WriteLine($"Running Year: {GetType().Name[^4..]}");
        RunAllSolutions();
    }

    protected abstract void RunAllSolutions();
}