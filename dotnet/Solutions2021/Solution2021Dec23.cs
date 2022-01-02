namespace AdventOfCode.Solutions2021;

public class Solution2021Dec23 : Solution
{
    public const char Dot = '.';
    public const char A = 'A';
    public const char B = 'B';
    public const char C = 'C';
    public const char D = 'D';

    public static readonly Dictionary<char, int> RoomLocations = new()
    {
        { A, 2 },
        { B, 4 },
        { C, 6 },
        { D, 8 }
    };

    protected override long FirstSolution(List<string> lines)
    {
        var members = lines.GetRange(2,2).Select(s => s.Replace("#", string.Empty).Trim().ToCharArray()).ToList();
        var roomOccupants = members.First().Zip(members.Last()).Select(z => new List<char>(){z.First, z.Second}).ToList();
        var rooms = new Dictionary<char, List<char>>
        {
            [A] = roomOccupants[0],
            [B] = roomOccupants[1],
            [C] = roomOccupants[2],
            [D] = roomOccupants[3]
        };
        var map = new Map(Enumerable.Repeat('.', 11).ToList(), rooms);
        return map.IsPathValid() ? 10 : 20;
    }

    protected override long SecondSolution(List<string> lines) => 5;

    private class Map
    {
        private static readonly Dictionary<char, int> RoomLocations = new()
        {
            { A, 2 },
            { B, 4 },
            { C, 6 },
            { D, 8 }
        };
        
        private readonly List<char> _hallway;
        private readonly Dictionary<char, List<char>> _rooms;

        public Map(List<char> hallway, Dictionary<char, List<char>> rooms)
        {
            _hallway = hallway;
            _rooms = rooms;
            CleanMap();
        }

        public bool IsPathValid()
        {
            if (_hallway.All(h => h.Equals(Dot)) && _rooms.All(r => !r.Value.Any())) return true;
            List<Map> nextMaps = GetNextMaps();
            return nextMaps.Any(map => map.IsPathValid());
        }

        private List<Map> GetNextMaps()
        {
            var result = new List<Map>();
            var occupiedRooms = _rooms.ToList().Where(r => r.Value.Count > 0);
            foreach (var (designation, occupants) in occupiedRooms)
            {
                for (var i = 0; i < 11; i++)
                {
                    if (_hallway[i] == Dot && HallwayIsOpen(i, RoomLocations[designation]))
                    {
                        var newHallway = _hallway.ToList();
                        newHallway[i] = occupants.First();
                        var newRooms = new Dictionary<char, List<char>>(_rooms);
                        newRooms[designation].Remove(occupants.First());
                        result.Add(new Map(newHallway, newRooms));
                    }
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
                    _hallway[i] = Dot;
            }
        }
        
        private bool HallwayIsOpen(int locA, int locB)
        {
            var delta = Math.Abs(locA - locB);
            return delta == 1 || _hallway.GetRange(Math.Min(locA, locB) + 1, delta - 1).All(loc => loc.Equals(Dot));
        }
    }
}