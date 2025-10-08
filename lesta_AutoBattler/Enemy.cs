using System;
using System.Collections.Generic;

public abstract class Enemy : Combatant
{

    private readonly List<IEnemyDeathListener> _deathListeners = new();

    protected Enemy(int hp, Weapon weapon, int strength, int agility, int stamina)
    {
        MaxHp = hp;
        CurrentHp = hp;
        Weapon = weapon;
        Strength = strength;
        Agility = agility;
        Stamina = stamina;
    }

    public void AddDeathListener(IEnemyDeathListener listener)
    {
        _deathListeners.Add(listener);
    }

    protected void NotifyDeath()
    {
        foreach (var listener in _deathListeners)
        {
            listener.OnEnemyDeath(this);
        }
    }

    public abstract Weapon GetRewardWeapon();

    // Модифицирует входящий урон с учётом особенностей (Слайм, Скелет и т.д.)
    public virtual (int WeaponDamage, int BonusDamage) ModifyIncomingDamage(
        int baseWeaponDamage, int strengthDamage, int bonusDamage, string weaponType, int turn)
    {
        // По умолчанию — ничего не меняется
        return (baseWeaponDamage, bonusDamage + strengthDamage);
    }

    // Снижение урона (защита: Каменная кожа, Щит и т.п.)
    public   virtual int GetDamageReduction(int totalDamage, int turn)
    {
        return 0;
    }

    // Доп. урон при атаке (Дракон, Призрак)
    public virtual int GetExtraAttack(int turn, int targetAgility)
    {
        return 0;
    }

    public override int takeDamage(int baseWeaponDamage, int strengthDamage, int bonusDamage, string weaponType, int turn)
    {
        var (modifiedWeapon, modifiedOther) = ModifyIncomingDamage(baseWeaponDamage, strengthDamage, bonusDamage, weaponType, turn);
        int total = modifiedWeapon + modifiedOther;
        int reduction = GetDamageReduction(total, turn);
        int finalDamage = Math.Max(0, total - reduction);

        CurrentHp -= finalDamage;

        if (CurrentHp <= 0)
        {
            NotifyDeath();
        }
        return finalDamage;
    }
}