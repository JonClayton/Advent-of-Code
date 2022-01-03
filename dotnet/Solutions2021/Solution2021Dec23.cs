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
    
    private static long GeneralSolution(List<string> lines)
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
            Counter++;
            _hallway = hallway;
            Path = path;
            _rooms = rooms;
            Score = score;
            // if (Counter % 100000 == 0) Console.WriteLine($"Counter: {Counter}");
            CleanMap();
        }

        public (bool, Map) IsPathBest()
        {
            if (Score >= LowestValue) return (false, null);
            if (_hallway.All(h => h.Equals(Dot)) && _rooms.All(r => !r.Value.Any()))
            {
                Console.WriteLine($"New Lowest Score: {Score}");
                LowestValue = Score;
                return (true, this);
            } 
            var nextMaps = GetNextMaps();
            var winners = nextMaps.Select(nextMap => nextMap.IsPathBest())
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
                var occupant = occupants.First();
                foreach (var i in HallwayLocations)
                {
                    if (_hallway[i] != Dot || !HallwayIsOpen(i, RoomLocations[designation])) continue;
                    var newScore = Score + Cost(i, designation, occupant);
                    if (newScore >= LowestValue) continue;
                    var newHallway = _hallway.ToList();
                    newHallway[i] = occupant;
                    var newRooms = _rooms.ToDictionary(x => x.Key, x => x.Value.ToList());
                    newRooms[designation].Remove(occupant);
                    var newPath = Path.ToList();
                    newPath.Add($"{newHallway[i]} at {i} from {designation}");
                    result.Add(new Map(newHallway, newRooms, newScore, newPath));
                }
            }

            return result;
        }

        private void CleanMap()
        {
            CleanRooms();
            CleanHallway();

            void CleanRooms()
            {
                foreach (var designation in RoomLocations.Keys)
                {
                    if (!_rooms[designation].Any()) break;
                    if (_rooms[designation].All(o => o.Equals(designation))) _rooms[designation].Clear();
                }
            }

            void CleanHallway()
            {
                foreach (var i in HallwayLocations)
                {
                    var standee = _hallway[i];
                    if (standee == Dot || _rooms[standee].Any() || !HallwayIsOpen(i, RoomLocations[standee])) continue;
                    Score += Cost(i, standee, standee);
                    Path.Add($"{_hallway[i]} at {i} home");
                    _hallway[i] = Dot;
                    CleanHallway();
                    return;
                }
            }
        }

        private static long Cost(int location, char room, char type) => 
            (Math.Abs(location - RoomLocations[room]) + 1) * MovementCost[type];

        private bool HallwayIsOpen(int locA, int locB)
        {
            var delta = Math.Abs(locA - locB);
            switch (delta)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    var rangeStart = Math.Min(locA, locB) + 1;
                    for (var i = rangeStart; i < rangeStart + delta - 1; i++) if (_hallway[i] != Dot) return false;
                    return true;
            }
        }
    }
}