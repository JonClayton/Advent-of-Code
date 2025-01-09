using System.Text.RegularExpressions;
using AdventOfCode.Solutions2015.RPG;

namespace AdventOfCode.Solutions2015;

public partial class Solution2015Dec22 : Solution<long?>
{
    [GeneratedRegex(Utilities.RegexForNumberFind)]
    private static partial Regex NumsRegex();
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);
    
    private long _minManaUsed = long.MaxValue;

    private long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var nums = lines.Select(line => int.Parse(NumsRegex().Match(line).Value)).ToList();
        var hitPoints = nums[0];
        var wizard = hitPoints == 14 ? new Character([], 10, 0, 0, 250) : new Character([], 50, 0, 0, 500);
        var battle = new Battle(wizard, new Character([], hitPoints, nums[1], 0));
        SeeIfWizardWinsWithLowerManaUse(battle, 0);
        return _minManaUsed;
    }

    private void SeeIfWizardWinsWithLowerManaUse(Battle battle, long manaUsed)
    {
        if (manaUsed > _minManaUsed) return;
        foreach (var spell in battle.AvailableSpells)
        {
            var newManaUsed = manaUsed + Battle.SpellCosts[spell];
            if (manaUsed >= _minManaUsed)
            {
                Console.WriteLine($"Too much mana with{newManaUsed}");
                continue;
            }
            var nextState = new Battle(battle);
            var result = nextState.WizardWinsWithCast(spell);
            if (result.HasValue && result.Value)
            {
                Console.WriteLine($"Won with{newManaUsed}");
                _minManaUsed = newManaUsed;
            }
            if (!result.HasValue) SeeIfWizardWinsWithLowerManaUse(nextState, newManaUsed);
            // Console.WriteLine($"Lost");
        }
    }
}