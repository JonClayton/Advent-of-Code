namespace AdventOfCode.Solutions2021;

public class Solution2021Dec23 : Solution
{
    public const char Dot = '.';
    public const char A = 'A';
    public const char B = 'B';
    public const char C = 'C';
    public const char D = 'D';

    public static readonly List<int> HallwayLocations = new() { 0, 1, 3, 5, 7, 9, 10 };
    public static readonly Dictionary<char, int> RoomLocations = new()
    {
        { A, 2 },
        { B, 4 },
        { C, 6 },
        { D, 8 }
    };
    public static readonly Dictionary<char, int> MovementCost = new()
    {
        { A, 1 },
        { B, 10 },
        { C, 100 },
        { D, 1000 }
    };

    public static long LowestValue = long.MaxValue;
    public static int Counter;


    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines);
    
    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines);
    
    private long GeneralSolution(List<string> lines)
    {
        LowestValue = long.MaxValue;
        var members = lines.GetRange(2, 2).Select(s => s.Replace("#", string.Empty).Trim().ToCharArray()).ToList();
        var roomOccupants = members.First().Zip(members.Last()).Select(z => new List<char>() { z.First, z.Second })
            .ToList();
        var rooms = new Dictionary<char, List<char>>
        {
            [A] = roomOccupants[0],
            [B] = roomOccupants[1],
            [C] = roomOccupants[2],
            [D] = roomOccupants[3]
        };
        var startingScore = rooms
            .Where(entry => entry.Key != entry.Value.Last())
            .Select(entry => MovementCost[entry.Value.Last()] + MovementCost[entry.Key])
            .Sum();
        var map = new Map(Enumerable.Repeat('.', 11).ToList(), rooms, startingScore, new List<string>());
        var (success, winner)= map.IsPathBest();
        Console.WriteLine($"Solved with score {winner.Score}");
        Console.WriteLine(string.Join("\n", winner.Path));
        return winner.Score;
    }

    private class Map
    {
        private readonly List<char> _hallway;
        private readonly Dictionary<char, List<char>> _rooms;
        public long Score;
        public List<string> Path;

        public Map(List<char> hallway, Dictionary<char, List<char>> rooms, long score, List<string> path)
        {
            // Counter++;
            _hallway = hallway;
            Path = path;
            _rooms = rooms;
            Score = score;
            CleanMap();
        }

        public (bool, Map) IsPathBest()
        {
            if (Score >= LowestValue) return (false, null);
            if (_hallway.All(h => h.Equals(Dot)) && _rooms.All(r => !r.Value.Any()))
            {
                LowestValue = Score;
                return (true, this);
            } 
            var winners = GetNextMaps()
                .Select(nextMap => nextMap.IsPathBest())
                .Where(result => result.Item1)
                .OrderBy(result => result.Item2.Score)
                .ToList();
            return winners.Any() ? winners.First() : (false, null);
        }

        private List<Map> GetNextMaps()
        {
            var result = new List<Map>();
            var occupiedRooms = _rooms.ToList().Where(r => r.Value.Count > 0);
            foreach (var (designation, occupants) in occupiedRooms)
            {
                if (!occupants.Any()) continue;
                foreach (var i in HallwayLocations)
                {
                    if (_hallway[i] != Dot || !HallwayIsOpen(i, RoomLocations[designation])) continue;
                    var newHallway = _hallway.ToList();
                    newHallway[i] = occupants.First();
                    var newRooms = new Dictionary<char, List<char>>(_rooms);
                    foreach (var (key, value) in newRooms) newRooms[key] = value.ToList();
                    newRooms[designation].Remove(occupants.First());
                    var newPath = Path.ToList();
                    var newScore = Score + Cost(i, designation, newHallway[i]);
                    newPath.Add($"{newHallway[i]} at {i} from {designation}");
                    result.Add(new Map(newHallway, newRooms, newScore, newPath));
                }
            }

            return result;
        }

        private void CleanMap()
        {
            foreach (var room in _rooms)
            {
                var (designation, occupants) = room;
                if (occupants.All(o => o.Equals(designation))) room.Value.Clear();
            }

            for (var i = 0; i < 11; i++)
            {
                var standee = _hallway[i];
                if (_hallway[i] != Dot && !_rooms[standee].Any() && HallwayIsOpen(i, RoomLocations[standee]))
                {
                    Score += Cost(i, standee, standee);
                    Path.Add($"{_hallway[i]} at {i} home");
                    _hallway[i] = Dot;
                    i = 0;
                }
            }
        }

        private static long Cost(int location, char room, char type)
        {
            return (Math.Abs(location - RoomLocations[room]) + 1) * MovementCost[type];
        }

        private bool HallwayIsOpen(int locA, int locB)
        {
            var delta = Math.Abs(locA - locB);
            if (delta == 0) return false;
            return delta == 1 || _hallway.GetRange(Math.Min(locA, locB) + 1, delta - 1).All(loc => loc.Equals(Dot));
        }
    }
}