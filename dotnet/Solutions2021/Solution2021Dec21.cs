namespace AdventOfCode.Solutions2021;

public class Solution2021Dec21 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var die = new Die();
        var players = lines.Select(line => new Player(line)).ToList();
        while(true)
        {
            if (players.First().TakeTurnAndCheckForWin(die)) return players.Last().Score * die.LastThrow;
            if (players.Last().TakeTurnAndCheckForWin(die)) return players.First().Score * die.LastThrow;
        }
    }

    protected override long SecondSolution(List<string> lines)
    { 
        var players = lines.Select(line => new Player(line)).ToList();
        var gameStates = new Dictionary<(int, int, int, int, int), long>()
        {
            { (0, players[0].Location, 0, players[1].Location, 0), 1 }
        };
        while (gameStates.Where(gs => gs.Key.Item1 == 0).Sum(gs => gs.Value) > 0)
        {
            var gameStatesList = gameStates.Where(gs => gs.Key.Item1 ==0 && gs.Value > 0).ToList();
            foreach (var (key, count) in gameStatesList)
            {
                var (_, player1Location, player1Score, player2Location, player2Score) = key;
                Die.ThrowDirac.ForEach(move =>
                {
                    var newLocation = (player1Location + move) % 10;
                    var newScore = player1Score + (newLocation == 0 ? 10 : newLocation);
                    var newKey = (newScore >= 21 ? 1 : 0, newLocation, newScore, player2Location, player2Score);
                    if (!gameStates.TryAdd(newKey, count)) gameStates[newKey] += count;
                });
                gameStates.Remove(key);
            }

            gameStatesList = gameStates.Where(gs => gs.Key.Item1 == 0).ToList();
            foreach (var (key, count) in gameStatesList)
            {
                var (_, player1Location, player1Score, player2Location, player2Score) = key;
                Die.ThrowDirac.ForEach(move =>
                {
                    var newLocation = (player2Location + move) % 10;
                    var newScore = player2Score + (newLocation == 0 ? 10 : newLocation);
                    var newKey = (newScore >= 21 ? 2 : 0, player1Location, player1Score, newLocation, newScore);
                    if (!gameStates.TryAdd(newKey, count)) gameStates[newKey] += count;
                });
                gameStates.Remove(key);
            }
            var total = gameStates.Select(gs => gs.Value).Sum();
            Console.WriteLine(total);
        }


        return gameStates.GroupBy(gs => gs.Key.Item1).Select(g => g.Select(g => g.Value).Sum()).Max();
    }

    private class Die
    {
        public int LastThrow = 0;

        public int ThrowDeterministic()
        {
            LastThrow++;
            return LastThrow;
        }

        public static List<int> ThrowDirac => new() { 3,4,4,4,5,5,5,5,5,5,6,6,6,6,6,6,6,7,7,7,7,7,7,8,8,8,9 };
    }

    private class Player
    {
        public int Location = 0;
        public int Score = 0;
        public int Turn = 0;
        public Dictionary<(int, int), long> LocationScoreUniverses = new();
        public Dictionary<int, long> TurnCountWins = new();

        public Player(string line)
        {
            Location = int.Parse(line.Split(":")[1].Trim());
            LocationScoreUniverses.Add((Location, 0), 1);
        }

        public bool TakeTurnAndCheckForWin(Die die)
        {
            var diceThrow = die.ThrowDeterministic() + die.ThrowDeterministic() + die.ThrowDeterministic();
            Location = (Location + diceThrow) % 10;
            Score += Location;
            if (Location == 0) Score += 10;
            return Score >= 1000;
        }

        public void TakeTurnAndUpdateScoreUniverses()
        {
            Turn++;
            var list = LocationScoreUniverses.ToList();
            foreach (var ((location, score), value) in list)
            {
                Die.ThrowDirac.ForEach(move =>
                {
                    var newLocation = (location + move) % 10;
                    var newScore = score + (newLocation == 0 ? 10 : newLocation);
                    if (newScore >= 21)
                    {
                        if (!TurnCountWins.TryAdd(Turn, value))
                            TurnCountWins[Turn] += value;
                    }
                    else
                    {
                        if (!LocationScoreUniverses.TryAdd((newLocation, newScore), value))
                            LocationScoreUniverses[(newLocation, newScore)] += value;
                    }
                });
                LocationScoreUniverses[(location, score)] = 0;
            }
        }
    }
}