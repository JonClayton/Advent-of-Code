namespace AdventOfCode.Solutions2015;

public partial class Day21 : Solution<long?>
{
    private const int Budget = 100;


    private static readonly Dictionary<string, Item> Weapons = new()
    {
        { "Dagger", new Item(8, 4, 0) },
        { "WarHammer", new Item(10, 5, 0) },
        { "ShortSword", new Item(25, 6, 0) },
        { "Longsword", new Item(40, 7, 0) },
        { "GreatAxe", new Item(75, 8, 0) }
    };

    private static readonly Dictionary<string, Item> Armor = new()
    {
        { "None", new Item(0, 0, 0) },
        { "Leather", new Item(13, 0, 1) },
        { "Chainmail", new Item(31, 0, 2) },
        { "SplintMail", new Item(53, 0, 3) },
        { "BandedMail", new Item(75, 0, 4) },
        { "PlateMail", new Item(102, 0, 5) }
    };

    private static readonly Dictionary<string, Item> Rings = new()
    {
        { "D1", new Item(25, 1, 0) },
        { "D2", new Item(50, 2, 0) },
        { "D3", new Item(100, 3, 0) },
        { "A1", new Item(20, 0, 1) },
        { "A2", new Item(40, 0, 2) },
        { "A3", new Item(80, 0, 3) },
        { "empty right", new Item(0, 0, 0) },
        { "empty left", new Item(0, 0, 0) }
    };

    [GeneratedRegex(Utilities.RegexForNumberFind)]
    private static partial Regex NumsRegex();

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
        var nums = lines.Select(line => int.Read(NumsRegex().Match(line).Value)).ToList();
        var hitPoints = nums[0];
        var builds = isFirstSolution
            ? GetBuildsOrderedByCost(Budget)
            : GetBuildsOrderedByCost(int.MaxValue).OrderByDescending(items => items.Sum(item => item.Cost)).ToList();
        var characterHitPoints = hitPoints == 12 ? 8 : 100;
        return builds
            .First(build => isFirstSolution == new Battle(new Character(build, characterHitPoints),
                new Character([], hitPoints, nums[1], nums[2])).IsWonByHero()).Sum(item => item.Cost);
    }

    private static List<List<Item>> GetBuildsOrderedByCost(int budget)
    {
        return (
            from weapon in Weapons
            from armor in Armor
            from ring1 in Rings
            from ring2 in Rings
            select new List<Item> { weapon.Value, armor.Value, ring1.Value, ring2.Value }
            into build
            where build.Sum(item => item.Cost) <= budget
            select build).Where(items => items[2] != items[3]).OrderBy(build => build.Sum(item => item.Cost)).ToList();
    }
}