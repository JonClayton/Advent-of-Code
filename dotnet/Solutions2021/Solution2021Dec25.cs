using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;

public class Solution2021Dec25 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var herdingGround = new HerdingGround(lines);
        while (herdingGround.MovedInLastStep) herdingGround.TakeStep();
        return herdingGround.StepsTaken;
    }

    protected override long SecondSolution(List<string> lines) => 5;

    private class HerdingGround
    {
        private const char Dot = '.';
        private const char East = '>';
        private const char South = 'v';
        
        private readonly int _cols;
        private readonly List<char[]> _locations;
        private readonly int _rows;

        private readonly Dictionary<(int, int), char> _pendingLocationChanges = new() { { (0, 0), Dot } };
        public bool MovedInLastStep => _pendingLocationChanges.Any();
        public long StepsTaken { get; private set; }

        public HerdingGround(List<string> lines)
        {
            _cols = lines.First().Length;
            _locations = lines.Select(line => line.ToCharArray()).ToList();
            _rows = lines.Count;
        }

        public void TakeStep()
        {
            _pendingLocationChanges.Clear();
            for (var r = 0; r < _rows; r++)
            for (var c = 0; c < _cols; c++)
            {
                if (!_locations[r][c].Equals(Dot)) continue;
                if (!TryMoveEast(r, c-1)) 
                    MoveSouth(r - 1, c);
            }
            
            foreach (var ((row, col), value) in _pendingLocationChanges) _locations[row][col] = value;
            StepsTaken++;
        }

        private bool TryMoveEast(int row, int col)
        {
            var destination = col + 1;
            col = col == -1 ? _cols - 1 : col;
            if (!_locations[row][col].Equals(East)) return false;
            _pendingLocationChanges.Add((row, col), Dot);
            _pendingLocationChanges[(row, destination)] = East;
            MoveSouth(row -1, col);
            return true;
        }
        
        private void MoveSouth(int row, int col)
        {
            var destination = row + 1;
            row = row == -1 ? _rows - 1 : row;
            if (!_locations[row][col].Equals(South)) return; 
            _pendingLocationChanges.Add((row, col), Dot);
            _pendingLocationChanges[(destination, col)] = South;
        }
    }
}