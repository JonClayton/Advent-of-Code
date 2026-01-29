namespace AdventOfCode.Solutions2015;

public class Solution2015Dec18 : Solution<long?>
{
    private HashSet<Vector2> _corners = [];
    private Map<Location<bool>, bool> _lightGrid = null!;

    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    private long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        _lightGrid = new Map<Location<bool>, bool>(lines, c => new Location<bool>(c == '#'), true);
        _corners =
        [
            Vector2.Zero, Vector2.UnitX * (_lightGrid.XRange - 1), Vector2.UnitY * (_lightGrid.YRange - 1),
            Vector2.UnitX * (_lightGrid.XRange - 1) + Vector2.UnitY * (_lightGrid.YRange - 1)
        ];
        if (!isFirstSolution)
            foreach (var vector in _corners)
                _lightGrid.Locations[vector].Value = true;
        var iterations = _lightGrid.XRange == 6 ? 5 : 100;
        while (iterations > 0)
        {
            iterations--;
            var updates = CalculateUpdates(isFirstSolution);
            ExecuteUpdate(updates);
        }

        return _lightGrid.Locations.Values.Count(location => location.Value);
    }

    private List<Vector2> CalculateUpdates(bool isFirst)
    {
        return _lightGrid.Locations.Values.Where(IsStateChanged)
            .Select(location => location.Coordinates).ToList();

        bool IsStateChanged(Location<bool> location)
        {
            if (!isFirst && _corners.Contains(location.Coordinates))
                return !location.Value;
            var countNeighborsOn = location.Neighbors.Values.Count(neighbor => neighbor.Value);
            if (countNeighborsOn == 3) return !location.Value;
            return countNeighborsOn != 2 && location.Value;
        }
    }

    private void ExecuteUpdate(List<Vector2> updates)
    {
        updates.ForEach(update => _lightGrid.Locations[update].Value ^= true);
    }
}