namespace AdventOfCode.Solutions2015.RPG;

public class Character(List<Item> items, int hitPoints, int damage = 0, int armor = 0, int mana = 0)
{
    public Character(Character character) : this([..character.Items], character.HitPoints, character.Damage,
        character.ArmorClass, character.Mana)
    {
    }

    public int HitPoints { get; set; } = hitPoints;

    public int Mana { get; set; } = mana;
    private int Damage { get; } = damage + items.Select(item => item.Damage).Sum();
    private int ArmorClass { get; } = armor + items.Select(item => item.ArmorClass).Sum();

    private List<Item> Items { get; } = items;

    public void Hits(Character character, bool isShieldActive = false)
    {
        var damage = Damage - character.ArmorClass - (isShieldActive ? 7 : 0);
        character.HitPoints -= damage > 1 ? damage : 1;
    }
}