public class Weapon
{
    public string Name { get; }
    public int Damage { get; }
    public string DamageType { get; } // "Рубящий", "Колющий", "Дробящий"

    public Weapon(string name, int damage, string damageType)
    {
        Name = name;
        Damage = damage;
        DamageType = damageType;
    }
}