using System.Security;
public abstract class CharacterClass
{
    public int Level { get; set; }

    public CharacterClass(int level = 0)
    {
        Level = level;
    }
    
    public abstract int GetExtraAttack(int turn, Player player, Combatant enemy);
    public abstract int GetExtraDamage(int turn, Player player, int attackerStrength, int incomingDamage);
    public abstract int GetHpPerLevel();
    public abstract Weapon GetStartingWeapon();
}