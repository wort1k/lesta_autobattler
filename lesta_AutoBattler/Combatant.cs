public abstract class Combatant
{
    public int MaxHp { get; protected set; }
    public int CurrentHp { get; set; }
    public int Strength { get; protected set; }
    public int Agility { get; protected set; }
    public int Stamina { get; protected set; }

    public Weapon Weapon { get; set; }

    public int AttackCount { get; set; } = 0;

    // Виртуальные методы для боевых способностей
    public virtual int GetExtraAttack(int turn, Combatant target)
    {
        return 0;
    }

    public virtual int GetDamageReduction(int totalDamage, int turn)
    {
        return 0;
    }

    public bool IsAlive() => CurrentHp > 0;

    public void HealFull()
    {
        CurrentHp = MaxHp;
    }

    public virtual int takeDamage(int baseWeaponDamage, int strengthDamage, int bonusDamage, string weaponType, int turn)
    {
        return 0;
    }

    public void OnAttack()
    {
        AttackCount++;
    }
    
    public void ResetAttackCount()
    {
        AttackCount = 0;
    }
}