namespace AdventOfCode.Solutions2022;

public class Solution2022Dec07 : Solution
{
    protected override long FirstSolution(List<string> lines) => 
        GetDirectories(lines)
            .Where(directory => directory.Size <= 100000)
            .Select(directory => directory.Size).Sum();

    protected override long SecondSolution(List<string> lines)
    {
        var directories = GetDirectories(lines);
        var spaceRequired = directories.First().Size - 40000000;
        return directories.Select(directory => directory.Size)
            .Where(size => size >= spaceRequired)
            .MinBy(size => size);
    }
    
    private static List<Directory> GetDirectories(IEnumerable<string> lines)
    {
        var root = new Directory();
        var directoryList = new List<Directory> { root };
        var currentDirectory = root;
        foreach (var parts in lines.Select(line => line.Split(" ")))
        {
            if (parts.First().Equals("$")) ProcessCommand(parts);
            else ProcessDirectoryEntry(parts);
        }

        return directoryList;

        void ProcessCommand(string[] parts)
        {
            switch (parts.Last())
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
                    CreateSubDirectoryIfNotExists(parts.Last());
                    currentDirectory = currentDirectory.Directories[parts.Last()];
                    break;
            }
        }

        void ProcessDirectoryEntry(string[] parts)
        {
            if (parts.First().Equals("dir")) CreateSubDirectoryIfNotExists(parts.Last());
            else CreateFileIfNotExists(parts.Last(), long.Parse(parts.First()));
        }
        
        void CreateFileIfNotExists(string name, long size) => currentDirectory.Files.TryAdd(name, size); 
        void CreateSubDirectoryIfNotExists(string name)
        {
            if (currentDirectory.Directories.TryAdd(name, new Directory { Parent = currentDirectory })) directoryList.Add(currentDirectory.Directories[name]);
        }
    }
}

public class Directory
{
    public Dictionary<string, long> Files { get; set; } = new();
    public Dictionary<string, Directory> Directories { get; set; } = new();
    public Directory Parent { get; set; }
    public long Size => Files.Select(file => file.Value).Sum() + Directories.Select(directory => directory.Value.Size).Sum();
}