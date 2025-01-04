namespace OldAdventOfCode.Classes;

internal abstract class CohortCounter<TType>
{
    protected Dictionary<TType, long> CohortCounts = new();

    protected void ReadInitialValues(IEnumerable<(TType, long)> inputTuples) => 
        CohortCounts = inputTuples
            .GroupBy(t => t.Item1)
            .ToDictionary(
                x => x.Key, 
                x => x.Select(t => t.Item2).Sum());

    public CohortCounter<TType> Cycle(int cycleCount)
    {
        Enumerable.Range(0, cycleCount).ToList().ForEach(_ => Cycle());
        return this;
    }

    protected void AddToCohortCounts(TType item, long value) 
    {
        if (!CohortCounts.TryAdd(item, value))
            CohortCounts[item] += value;
    }

    protected virtual void Cycle()
    {
    }
    
    public abstract long Result();
}