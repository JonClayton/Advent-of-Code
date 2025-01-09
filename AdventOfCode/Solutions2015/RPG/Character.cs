namespace AdventOfCode.Solutions2015.RPG;
    
public class Character(List<Item> items, int hitPoints, int damage = 0, int armor = 0, int mana = 0)
{
    public Character(Character character) : this([..character.Items], character.HitPoints, character.Damage,
        character.ArmorClass, character.Mana)
    {
        
    }

    public int HitPoints { get; set; } = hitPoints;
    
    public int Mana { get; set; } = mana;
    public int Damage { get; set; } = damage + items.Select(item => item.Damage).Sum();
    public int ArmorClass { get; set; } = armor + items.Select(item => item.ArmorClass).Sum();

    public List<Item> Items { get; } = items;
    public void Hits(Character character, bool isShieldActive = false)
    {
        var damage = Damage - character.ArmorClass - (isShieldActive ? 7 : 0);
        character.HitPoints -= damage > 1 ? damage : 1;
    }




}