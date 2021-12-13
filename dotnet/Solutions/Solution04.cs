using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2021.Utilities;

namespace AdventOfCode2021.Solutions;

public class Solution04 : Solution
{
    private List<BingoCard> _cards;
    private IEnumerable<int> _draws;

    protected override long FirstSolution(List<string> lines)
    {
        StartGame(lines);
        foreach (var draw in _draws)
        foreach (var card in _cards.Where(card => card.IsThisRoundAWin(draw)))
            return card.Value() * draw;

        throw new Exception("No solution found");
    }

    protected override long SecondSolution(List<string> lines)
    {
        StartGame(lines);
        foreach (var draw in _draws)
        {
            var potentialLastCard = _cards.ElementAt(0);
            _cards = _cards.Where(card => !card.IsThisRoundAWin(draw)).ToList();
            if (!_cards.Any()) return potentialLastCard.Value() * draw;
        }

        throw new Exception("No solution found");
    }

    private static IEnumerable<BingoCard> MapLinesToCards(List<string> lines)
    {
        return lines
            .GetRange(2, lines.Count - 2)
            .Select(line => line.Split()
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(int.Parse).ToList())
            .Where(l => l.Count.Equals(5))
            .Chunk(5).Select(chunk => new BingoCard(chunk));
    }

    private void StartGame(List<string> lines)
    {
        _draws = lines.First().Split(",").Select(int.Parse);
        _cards = MapLinesToCards(lines).ToList();
    }
}

public class BingoCard
{
    private IEnumerable<IEnumerable<int>> _grid;

    public BingoCard(IEnumerable<int>[] grid)
    {
        _grid = grid;
    }

    public bool IsThisRoundAWin(int ballNumber)
    {
        UpdateCard(ballNumber);
        return CheckForWin();
    }

    public int Value()
    {
        return _grid.SelectMany(row => row).Sum();
    }

    private bool CheckForWin()
    {
        if (_grid.Any(row => row.Sum().Equals(0))) return true;
        for (var col = 0; col < _grid.First().Count(); col++)
            if (_grid.Select(row => row.ElementAt(col)).Sum().Equals(0))
                return true;
        return false;
    }

    private void UpdateCard(int num)
    {
        _grid = _grid.Select(rows => rows.Select(cell => cell.Equals(num) ? 0 : cell));
    }
}