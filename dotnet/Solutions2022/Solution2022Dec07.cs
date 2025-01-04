using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2022;

public class Solution2022Dec07 : Solution
{
    protected override long FirstSolution(List<string> lines) =>
        GetDirectories(lines)
            .Where(directory => directory.Size <= 100000)
            .Select(directory => directory.Size)
            .Sum();

    protected override long SecondSolution(List<string> lines)
    {
        return GetDirectories(lines).Aggregate(((long)0, (long)0), (pair, directory) =>
        {
            var (requiredSize, smallestSeen) = pair;
            if (requiredSize == 0) return (directory.Size - 40000000, 70000000);
            return (requiredSize,
                smallestSeen > directory.Size && requiredSize < directory.Size ? directory.Size : smallestSeen);
        }).Item2;
    }

    private static IEnumerable<Directory> GetDirectories(IEnumerable<string> lines) => lines
        .Aggregate((new List<Directory>(), new Directory()), (pair, line) =>
        {
            var (directoryList, currentDirectory) = pair;
            if (!directoryList.Any()) directoryList.Add(currentDirectory);
            var parts = line.Split(" ");
            switch (parts[0])
            {
                case "$":
                    switch (parts[^1])
                    {
                        case "/":
                            currentDirectory = directoryList[0];
                            break;
                        case "..":
                            currentDirectory = currentDirectory.Parent;
                            break;
                        case "ls":
                            break;
                        default:
                            if (currentDirectory.Directories.TryAdd(parts[2],
                                    new Directory { Parent = currentDirectory }))
                                directoryList.Add(currentDirectory.Directories[parts[2]]);
                            currentDirectory = currentDirectory.Directories[parts[2]];
                            break;
                    }

                    break;
                case "dir":
                    break;
                default:
                    currentDirectory.Files.TryAdd(parts[1], long.Parse(parts[0]));
                    break;
            }

            return (directoryList, currentDirectory);
        }).Item1;

    private class Directory
    {
        public Dictionary<string, Directory> Directories { get; set; } = new();
        public Dictionary<string, long> Files { get; set; } = new();
        public Directory Parent { get; set; }
        public long Size => Files.Select(file => file.Value).Sum() +
                            Directories.Select(directory => directory.Value.Size).Sum();
    }
}