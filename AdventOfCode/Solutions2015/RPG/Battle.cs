using System.ComponentModel;

namespace AdventOfCode.Solutions2015.RPG;

public class Battle(Character hero, Character monster)
{

    public Battle(Battle battle) : this( new Character(battle.Hero), new Character(battle.Monster))
    {
    }
    private Character Hero { get; } = hero;
    private Character Monster { get; } = monster;

    public int PoisonTimer { get; set; }
    public int ShieldTimer { get; set; }
    public int RechargeTimer { get; set; }

    public bool? WizardWinsWithCast(Spell spell)
    {
        var isShieldActive = false;
        ApplyEffects();
        switch (spell)
        {
            case Spell.Drain:
                Hero.Mana -= 53;
                Monster.HitPoints -= 2;
                Hero.HitPoints += 2;
                break;
            case Spell.Missile:
                Hero.Mana -= 73;
                Monster.HitPoints -= 4;
                break;
            case Spell.Poison:
                Hero.Mana -= 173;
                PoisonTimer = 6;
                break;
            case Spell.Recharge:
                Hero.Mana -= 229;
                break;
            case Spell.Shield:
                Hero.Mana -= 113;
                ShieldTimer = 6;
                isShieldActive = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(spell), spell, null);
        }

        ApplyEffects();
        return DoesMonsterLose(isShieldActive);

        void ApplyEffects()
        {
            if (PoisonTimer > 0)
            {
                PoisonTimer--;
                Monster.HitPoints -= 3;
            }

            if (RechargeTimer > 0)
            {
                RechargeTimer--;
                Hero.Mana += 101;
            }

            if (ShieldTimer == 0) return;
            ShieldTimer--;
            isShieldActive = true;
        }
    }

    public bool IsWonByHero()
    {
        while (true)
        {
            Hero.Hits(Monster);
            var result = DoesMonsterLose(false);
            if (result is not null) return result.Value;
        }
    }
    
    private bool? DoesMonsterLose(bool isShieldActive)
    {
        if (Monster.HitPoints <= 0) return true;
        Monster.Hits(Hero, isShieldActive);
        if (Hero.HitPoints <= 0) return false;
        return null;
    }

    public List<Spell> AvailableSpells => SpellCosts
        .Where(entry => entry.Value <= Hero.Mana)
        .Where(entry => entry.Key != Spell.Poison || PoisonTimer < 2)
        .Where(entry => entry.Key != Spell.Recharge || RechargeTimer < 2)
        .Where(entry => entry.Key != Spell.Shield || ShieldTimer < 2)
        .Select(entry => entry.Key).ToList();
    
    public static readonly Dictionary<Spell, int> SpellCosts = new()
    {
        { Spell.Drain, 73 },
        { Spell.Missile, 53 },
        { Spell.Poison, 173 },
        { Spell.Recharge, 229 },
        { Spell.Shield, 113 }
    };
}