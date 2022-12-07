namespace AdventOfCode.Solutions2022;

public class Solution2022Dec07 : Solution
{
    protected override long FirstSolution(List<string> lines) => 
        GetDirectories(lines)
            .Where(directory => directory.Size <= 100000)
            .Select(directory => directory.Size).Sum();

    protected override long SecondSolution(List<string> lines)
    {
        return GetDirectories(lines).Aggregate(new Tuple<long, long>(0, 0), (tuple, directory) =>
        {
            if (tuple.Item1 == 0) return new Tuple<long, long>(directory.Size - 40000000, 70000000);
            if (tuple.Item2 > directory.Size && tuple.Item1 < directory.Size)
                return new Tuple<long, long>(tuple.Item1, directory.Size);
            return tuple;
        }).Item2;
    }
    
    private static IEnumerable<Directory> GetDirectories(IEnumerable<string> lines)
    {
        var root = new Directory();
        var currentDirectory = root;
        var directoryList = new List<Directory> { root };
        foreach (var parts in lines.Select(line => line.Split(" ")))
        {
            if (parts[0].Equals("$")) ProcessCommand(parts);
            else ProcessDirectoryEntry(parts);
        }

        return directoryList;

        void ProcessCommand(IReadOnlyList<string> parts)
        {
            switch (parts[^1])
            {
                case "/":
                    currentDirectory = root;
                    break;
                case "..":
                    currentDirectory = currentDirectory.Parent;
                    break;
                case "ls":
                    break;
                default:
                    CreateSubDirectoryIfNotExists(parts[2]);
                    currentDirectory = currentDirectory.Directories[parts[2]];
                    break;
            }
        }

        void ProcessDirectoryEntry(IReadOnlyList<string> parts)
        {
            if (parts[0].Equals("dir")) CreateSubDirectoryIfNotExists(parts[1]);
            else CreateFileIfNotExists(parts[1], long.Parse(parts[0]));
        }
        
        void CreateFileIfNotExists(string name, long size) => currentDirectory.Files.TryAdd(name, size); 
       
        void CreateSubDirectoryIfNotExists(string name)
        {
            if (currentDirectory.Directories.TryAdd(name, new Directory { Parent = currentDirectory }))
                directoryList.Add(currentDirectory.Directories[name]);
        }
    }
}

public class Directory
{
    public Dictionary<string, Directory> Directories { get; set; } = new();
    public Dictionary<string, long> Files { get; set; } = new();
    public Directory Parent { get; set; }
    public long Size => Files.Select(file => file.Value).Sum() +
                        Directories.Select(directory => directory.Value.Size).Sum();
}