namespace AdventOfCode.Solutions2015.RPG;

public class Item(int cost, int damage, int armorClass)
{
    public int Cost { get; } = cost;
    public int Damage { get; } = damage;
    public int ArmorClass { get; } = armorClass;
}