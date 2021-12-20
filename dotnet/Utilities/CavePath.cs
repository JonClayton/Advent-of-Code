using System.Collections.Generic;
using AdventOfCode.Solutions2021;

namespace AdventOfCode.Utilities;

public class CavePath
{
    public HashSet<Cave> SmallCavesVisited;
    public bool IsSingleSecondVisitComplete;
    public Cave CurrentCave;

    public CavePath(Cave cave, bool isSecondVisitAllowed, HashSet<Cave> smallCavesVisited = null)
    {
        IsSingleSecondVisitComplete = !isSecondVisitAllowed;
        SmallCavesVisited = smallCavesVisited ?? new HashSet<Cave>();
        if (SmallCavesVisited.Contains(cave)) IsSingleSecondVisitComplete = true;
        if (cave.IsSmall) SmallCavesVisited.Add(cave);
        CurrentCave = cave;
    }

    public bool TryNextCave(Cave cave, out CavePath nextCavePath)
    {
        if (!cave.IsSmall || !SmallCavesVisited.Contains(cave) || !IsSingleSecondVisitComplete)
        {
            nextCavePath = new CavePath(cave, IsSingleSecondVisitComplete, SmallCavesVisited);
            return true;
        }

        nextCavePath = null;
        return false;
    }
}
