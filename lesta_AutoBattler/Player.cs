using System;
using System.Collections.Generic;

public class Player : Combatant
{

    public Dictionary<string, CharacterClass> Classes { get; } = new()
    {
        ["Разбойник"] = new Rogue(),
        ["Воин"] = new Warrior(),
        ["Варвар"] = new Barbarian()
    };

    public void PrintPlayerInfo()
{
    Console.WriteLine("=== Характеристики персонажа ===");
    Console.WriteLine($"Сила: {Strength}");
    Console.WriteLine($"Ловкость: {Agility}");
    Console.WriteLine($"Выносливость: {Stamina}");
    Console.WriteLine($"Здоровье: {CurrentHp} / {MaxHp}");
    Console.WriteLine($"Оружие: {Weapon.Name} ({Weapon.Damage} урона, {Weapon.DamageType})");
    Console.WriteLine();

    Console.WriteLine("=== Уровни классов ===");
    foreach (var kvp in Classes)
    {
        string className = kvp.Key;
        int level = kvp.Value.Level;
        Console.WriteLine($"{className}: {level}");
    }
    Console.WriteLine();
}

    public Player(string initialClass)
    {
        var random = new Random();
        Strength = random.Next(1, 4);
        Agility = random.Next(1, 4);
        Stamina = random.Next(1, 4);

        Classes[initialClass].Level = 1;
        RecalculateMaxHp();
        CurrentHp = MaxHp;
        Weapon = Classes[initialClass].GetStartingWeapon();
    }

    private void RecalculateMaxHp()
    {
        int hp = Stamina;
        foreach (var cls in Classes.Values)
        {
            hp += cls.GetHpPerLevel() * cls.Level;
        }
        MaxHp = hp;
    }

    public override int GetExtraAttack(int turn, Combatant target)
    {
        int total = 0;
        foreach (var cls in Classes.Values)
        {
            if (cls.Level > 0)
            {
                total += cls.GetExtraAttack(turn, this, target);
            }
        }
        return total;
    }

    public int GetExtraDamage(int turn, int attackerStrength, int incomingDamage)
    {
        int total = 0;
        foreach (var cls in Classes.Values)
        {
            if (cls.Level > 0)
            {
                total += cls.GetExtraDamage(turn, this, attackerStrength, incomingDamage);
            }
        }
        return total;
    }

    public override int GetDamageReduction(int totalDamage, int turn)
    {
        int mod = 0;
        foreach (var cls in Classes.Values)
            if (cls.Level > 0)
                mod += cls.GetExtraDamage(turn, this, Strength, totalDamage);

        return -mod;
    }

    public bool LevelUp(string newClass)
    {
        int totalLevel = 0;
        foreach (var cls in Classes.Values) totalLevel += cls.Level;
        if (totalLevel >= 3) return false;

        Classes[newClass].Level++;
        RecalculateMaxHp();
        CurrentHp = MaxHp;
        return true;
    }

    public bool CanLevelUp()
    {
        int total = 0;
        foreach (var cls in Classes.Values) total += cls.Level;
        return total < 3;
    }

    public override int takeDamage(int baseWeaponDamage, int strengthDamage, int bonusDamage, string weaponType, int turn)
    {
        CurrentHp -= baseWeaponDamage + GetDamageReduction(baseWeaponDamage, turn);
        return baseWeaponDamage + GetDamageReduction(baseWeaponDamage, turn);
    }
}