using System.Collections.Generic;
using System.Linq;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec12 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    protected override long SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    private static long GeneralSolution(List<string> lines, bool canVisitSmallCavesTwice)
    {
        var caves = new CaveComplex(lines);
        return caves.FindPaths(canVisitSmallCavesTwice).Count;
    }
}

public class Cave
{
    public HashSet<Cave> Connections = new();
    public bool IsSmall;
    public string Name;

    public Cave(string name, bool isSmall)
    {
        Name = name;
        IsSmall = isSmall;
    }
}

public class CaveComplex
{
    public Dictionary<string, Cave> Caves = new();

    public CaveComplex(List<string> lines)
    {
        var pairs = lines.Select(line => line.Split("-"));
        foreach (var pair in pairs)
        {
            var name0 = pair[0];
            var name1 = pair[1];
            Caves.TryAdd(name0, new Cave(name0, name0 == name0.ToLower()));
            Caves.TryAdd(name1, new Cave(name1, name1 == name1.ToLower()));
            var cave0 = Caves[name0];
            var cave1 = Caves[name1];
            if (cave0.Name != "start") cave1.Connections.Add(cave0);
            if (cave1.Name != "start") cave0.Connections.Add(cave1);
            cave1.Connections.Add(cave0);
        }
    }

    public HashSet<List<Cave>> FindPaths(bool canVisitTwice)
    {
        var paths = new HashSet<List<Cave>> { new() { Caves["start"] } };
        while (paths.Any(path => !path.Last().Equals(Caves["end"])))
            paths = TryAllConnections(paths);
        return paths;

        HashSet<List<Cave>> TryAllConnections(HashSet<List<Cave>> paths)
        {
            var extendedPaths = new List<List<Cave>>();
            foreach (var path in paths)
                if (path.Last().Name.Equals("end")) extendedPaths.Add(path);
                else
                    path.Last().Connections
                        .Where(cave => CanRevisit(path, cave)).ToList()
                        .Select(cave =>
                        {
                            var newPath = new List<Cave>();
                            newPath.AddRange(path.ToList());
                            newPath.Add(cave);
                            return newPath;
                        })
                        .ToList().ForEach(path => extendedPaths.Add(path));
            return extendedPaths.ToHashSet();

            bool CanRevisit(List<Cave> path, Cave cave)
            {
                if (cave.Name == "start") return false;
                if (!cave.IsSmall) return true;
                if (!path.Contains(cave)) return true;
                if (!canVisitTwice) return false;
                return !path.Where(c => c.IsSmall).GroupBy(c => c.Name).Any(g => g.Count() > 1);
            }
        }
    }
}