// Goblin.cs
public class Goblin : Enemy
{
    public Goblin() : base(5, new Weapon("", 2, ""), 1, 1, 1)
    {

     }
    public override Weapon GetRewardWeapon() => Weapons.Dagger;
}

// Skeleton.cs
public class Skeleton : Enemy
{
    public Skeleton() : base(10, new Weapon("", 2, ""), 2, 2, 1) { }
    public override Weapon GetRewardWeapon() => Weapons.Mace;

    public override (int, int) ModifyIncomingDamage(int w, int s, int b, string type, int turn)
    {
        if (type == "Дробящий")
            w *= 2; // уязвимость
        return (w, s + b);
    }
}

// Slime.cs
public class Slime : Enemy
{
    public Slime() : base(8, new Weapon("", 1, ""), 3, 1, 2) { }
    public override Weapon GetRewardWeapon() => Weapons.Spear;

    public override (int, int) ModifyIncomingDamage(int w, int s, int b, string type, int turn)
    {
        if (type == "Рубящий")
            w = 0; // рубящее оружие не работает
        return (w, s + b);
    }
}

// Ghost.cs
public class Ghost : Enemy
{
    public Ghost() : base(6, new Weapon("", 3, ""), 1, 3, 1) { }
    public override Weapon GetRewardWeapon() => Weapons.Sword;

    public override int GetExtraAttack(int turn, int targetAgility)
    {
        return Agility > targetAgility ? 1 : 0; // Скрытая атака
    }
}

// Golem.cs
public class Golem : Enemy
{
    public Golem() : base(10, new Weapon("", 1, ""), 3, 1, 3) { }
    public override Weapon GetRewardWeapon() => Weapons.Axe;

    public override int GetDamageReduction(int totalDamage, int turn)
    {
        return Stamina; // Каменная кожа
    }
}

// Dragon.cs
public class Dragon : Enemy
{
    public Dragon() : base(20, new Weapon("", 4, ""), 3, 3, 3) { }
    public override Weapon GetRewardWeapon() => Weapons.LegendarySword;

    public override int GetExtraAttack(int turn, int targetAgility)
    {
        return AttackCount % 3 == 0 ? 3 : 0; // Огненное дыхание
    }
}