using System;
using System.ComponentModel;

public class Battle
{
    private readonly Combatant _attacker;
    private readonly Combatant _defender;
    private int _turn = 1;
    private readonly Random _random = new();

    public Battle(Combatant player, Combatant enemy)
    {
        // Определяем, кто ходит первым
        if (player.Agility >= enemy.Agility)
        {
            _attacker = player;
            _defender = enemy;
        }
        else
        {
            _attacker = enemy;
            _defender = player;
        }
    }

    public bool Fight()
    {
        _attacker.ResetAttackCount();
        _defender.ResetAttackCount();
        Combatant currentAttacker = _attacker;
        Combatant currentDefender = _defender;

        while (_attacker.IsAlive() && _defender.IsAlive())
        {
            performAttack(currentAttacker, currentDefender);

            if (!_attacker.IsAlive() || !_defender.IsAlive())
                break;

            // Меняем стороны
            (currentAttacker, currentDefender) = (currentDefender, currentAttacker);
            _turn++;
        }

        return _attacker is Player ? _attacker.IsAlive() : _defender.IsAlive();
    }

    private void performAttack(Combatant attacker, Combatant defender)
    {
        string attackerName = attacker is Player ? "Игрок" : attacker.GetType().Name;
        string defenderName = defender is Player ? "Игрок" : defender.GetType().Name;

        attacker.OnAttack();

        Console.WriteLine($"\n[Ход {_turn}] {attackerName} атакует {defenderName}!");
        Console.WriteLine($"  Ловкость: {attacker.Agility} vs {defender.Agility}");

        int totalAgility = attacker.Agility + defender.Agility;
        int roll = _random.Next(1, totalAgility + 1);
        Console.WriteLine($"  Бросок для попадания: {roll} (из 1–{totalAgility})");

        if (roll >= defender.Agility)
        {
            Console.WriteLine("  → Атака попала!");

            int baseWeaponDamage = attacker.Weapon.Damage;
            int strengthDamage = attacker.Strength;
            int bonusDamage = attacker.GetExtraAttack(_turn, _defender);
            int resistance = defender.GetDamageReduction(baseWeaponDamage + strengthDamage, _turn);
            Console.WriteLine($"  Урон: {baseWeaponDamage} (оружие) + {strengthDamage} (сила) + {bonusDamage} (бонусы) - {resistance}");

            int finalDamage = defender.takeDamage(
                baseWeaponDamage: baseWeaponDamage,
                strengthDamage: strengthDamage,
                bonusDamage: bonusDamage,
                weaponType: attacker.Weapon.DamageType,
                turn: _turn
            );

            Console.WriteLine($"  Итоговый урон после всех модификаторов: {finalDamage}");
            Console.WriteLine($"  У {defenderName} осталось HP: {Math.Max(0, defender.CurrentHp)}");
        }
        else
        {
            Console.WriteLine("  → Атака промахнулась!");
        }
    }
}