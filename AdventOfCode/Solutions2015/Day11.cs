namespace AdventOfCode.Solutions2015;

public partial class Day11 : Solution<string>
{
    [GeneratedRegex("i|o|l")]
    private static partial Regex HasBadRegex();

    [GeneratedRegex(@"(.)\1")]
    private static partial Regex HasPairRegex();

    protected override string FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines, true);
    }

    protected override string SecondSolution(List<string> lines)
    {
        return GeneralSolution(lines, false);
    }

    private static string GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var password = lines.First();
        while (true)
        {
            password = Increment(password);
            if (!IsValid(password)) continue;
            if (!isFirstSolution)
                isFirstSolution = true;
            else
                return password;
            // return 0;
        }
    }

    private static string Increment(string password)
    {
        if (password.Length == 1) return Increment(password[0]).ToString();
        return password[^1] != 'z'
            ? $"{password[..^1]}{Increment(password[^1])}"
            : $"{Increment(password[..^1])}a";
    }

    private static char Increment(char c)
    {
        return (char)(c + 1);
    }

    private static bool IsValid(string password)
    {
        return !HasBadRegex().IsMatch(password) && HasTwoPairs(password) && HasStraight(password);
    }

    private static bool HasTwoPairs(string password)
    {
        return HasPairRegex().Matches(password).Select(x => x.Value).ToHashSet().Count > 1;
    }

    private static bool HasStraight(string password)
    {
        for (var i = 1; i < password.Length - 1; i++)
            if (password[i] - password[i - 1] == 1 && password[i + 1] - password[i] == 1)
                return true;
        return false;
    }
}