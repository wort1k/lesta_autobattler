// Rogue.cs
public class Rogue : CharacterClass
{
    public Rogue(int level = 0) : base(level) { }

    public override int GetExtraAttack(int turn, Player player, Combatant enemy)
    {
        if (Level >= 1 && player.Agility > enemy.Agility) return 1;
        if (Level >= 3) return turn - 1; // Яд: +1 на 2-м ходу, +2 на 3-м и т.д.
        return 0;
    }

    public override int GetExtraDamage(int turn,Player player, int attackerStrength, int incomingDamage) => 0;
    public override int GetHpPerLevel() => 4;
    public override Weapon GetStartingWeapon() => Weapons.Dagger;
}

// Warrior.cs
public class Warrior : CharacterClass
{
    public Warrior(int level = 0) : base(level) { }

    public override int GetExtraAttack(int turn,Player player, Combatant enemy)
    {
        if (Level >= 1 && turn == 1) return player.Weapon.Damage; // Порыв: +урон = урон оружия
        return 0;
    }

    public override int GetExtraDamage(int turn,Player player, int attackerStrength, int incomingDamage)
    {
        if (Level >= 2 && player.Strength > attackerStrength) return -3; // Щит
        return 0;
    }

    public override int GetHpPerLevel() => 5;
    public override Weapon GetStartingWeapon() => Weapons.Sword;
}

// Barbarian.cs
public class Barbarian : CharacterClass
{
    public Barbarian(int level = 0) : base(level) { }

    public override int GetExtraAttack(int turn, Player player, Combatant enemy)
    {
        if (Level >= 1)
            return turn <= 3 ? 2 : -1; // Ярость
        return 0;
    }

    public override int GetExtraDamage(int turn,Player player, int attackerStrength, int incomingDamage)
    {
        if (Level >= 2) return -player.Stamina; // Каменная кожа
        return 0;
    }

    public override int GetHpPerLevel() => 6;
    public override Weapon GetStartingWeapon() => Weapons.Mace;
}