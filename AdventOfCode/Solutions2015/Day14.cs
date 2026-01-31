namespace AdventOfCode.Solutions2015;

public class Day14 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var herd = lines.Select(line => new Reindeer(line)).ToList();
        if (isFirstSolution) return herd.Select(reindeer => reindeer.DistanceTraveled(2503)).Max();
        var t = 1;
        while (t <= 2503)
        {
            var scoreboard = herd.ToDictionary(reindeer => reindeer, reindeer => reindeer.DistanceTraveled(t));
            scoreboard.Where(pair => pair.Value == scoreboard.Values.Max()).ToList().ForEach(pair => pair.Key.Score++);
            t++;
        }

        return herd.Select(reindeer => reindeer.Score).Max();
    }

    private class Reindeer
    {
        public Reindeer(string input)
        {
            var chunks = input.Split(" ");
            Speed = int.Read(chunks[3]);
            Endurance = int.Read(chunks[6]);
            Rest = int.Read(chunks[13]);
        }

        private int Endurance { get; }
        private int Speed { get; }
        private int Rest { get; }

        public long Score { get; set; }

        public long DistanceTraveled(int seconds)
        {
            var cycles = seconds / (Endurance + Rest);
            var modulo = seconds % (Endurance + Rest);
            return Speed * (Endurance * cycles + (modulo > Endurance ? Endurance : modulo));
        }
    }
}