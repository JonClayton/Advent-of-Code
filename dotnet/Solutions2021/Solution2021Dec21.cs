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
        var initialUniverseState = new GameState
        {
            Player1Location = players[0].Location,
            Player1Score = players[0].Score,
            Player2Location = players[1].Location,
            Player2Score = players[1].Score,
        };
        var multiverse = new Multiverse 
        {
            GameStates = new Dictionary<GameState, long> { { initialUniverseState, 1 } },
        };
        while (multiverse.GameStates.Any())
        {
            foreach(var (gameState, universeCount) in multiverse.GameStates.ToList()) ReadyPlayerOne(gameState, universeCount);
            foreach(var (gameState, universeCount) in multiverse.GameStates.ToList()) ReadyPlayerTwo(gameState, universeCount);
            multiverse.Round++;
            Console.WriteLine(multiverse.Report);
        }
        return Math.Max(multiverse.Player1Wins, multiverse.Player2Wins);

        void ReadyPlayerOne(GameState gameState, long universeCount)
        {
            foreach (var (value, frequency) in Die.DiracThrowOutcomeFrequencies)
            {
                var newLocation = (gameState.Player1Location + value) % 10;
                var newScore = gameState.Player1Score + (newLocation == 0 ? 10 : newLocation);
                if (newScore < 21)
                    multiverse.GameStates.Add(new GameState
                    {
                        Player1Location = newLocation,
                        Player1Score = newScore,
                        Player2Location = gameState.Player2Location,
                        Player2Score = gameState.Player2Score
                    }, frequency * universeCount);
                else multiverse.Player1Wins += frequency * universeCount;
            }

            multiverse.GameStates.Remove(gameState);
        }

        void ReadyPlayerTwo(GameState gameState, long universeCount)
        {
            foreach (var (value, frequency) in Die.DiracThrowOutcomeFrequencies)
            {
                var newLocation = (gameState.Player2Location + value) % 10;
                var newScore = gameState.Player2Score + (newLocation == 0 ? 10 : newLocation);
                if (newScore < 21)
                    multiverse.GameStates.Add(new GameState
                    {
                        Player1Location = gameState.Player1Location,
                        Player1Score = gameState.Player1Score,
                        Player2Location = newLocation,
                        Player2Score = newScore
                    }, frequency * universeCount);
                else multiverse.Player2Wins += frequency * universeCount;
            }

            multiverse.GameStates.Remove(gameState);
        }
    }

    private class Die
    {
        public int LastThrow = 0;

        public int ThrowDeterministic()
        {
            LastThrow++;
            return LastThrow;
        }

        public static Dictionary<int, int> DiracThrowOutcomeFrequencies => new()
        {
            { 3, 1 },
            { 4, 3 },
            { 5, 6 },
            { 6, 7 },
            { 7, 6 },
            { 8, 3 },
            { 9, 1 }
        };
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
    }

    private class GameState
    {
        public int Player1Location;
        public int Player1Score;
        public int Player2Location;
        public int Player2Score;
    }

    private class Multiverse
    {
        public int Round;
        public long Player1Wins;
        public long Player2Wins;
        public Dictionary<GameState, long> GameStates;

        public string Report =>
            $"After Round {Round}, Player1 Wins: {Player1Wins}, Player2 Wins: {Player2Wins}, and {GameStates.Count} gameStates alive.";
    }
}