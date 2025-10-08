using System;

public class Game
{
    private int _consecutiveVictories = 0;
    private readonly Random _random = new();

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Выберите начальный класс:");
            Console.WriteLine("1 - Воин");
            Console.WriteLine("2 - Варвар");
            Console.WriteLine("3 - Разбойник");
            string input = Console.ReadLine();

            string initialClass = input switch
            {
                "1" => "Воин",
                "2" => "Варвар",
                "3" => "Разбойник",
                _ => "Воин"
            };

            var player = new Player(initialClass);
            Console.WriteLine("Стартовые характеристики: ");
            Console.WriteLine("ловкость: " + player.Agility.ToString() +
            "\n Здоровье: " + player.MaxHp.ToString() +
            "\nвынослиость: " + player.Stamina.ToString() + '\n');
            _consecutiveVictories = 0;

            while (_consecutiveVictories < 5 && player.IsAlive())
            {
                var enemy = GenerateRandomEnemy();
                Console.WriteLine($"\nВы встречаете: {enemy.GetType().Name} (HP: {enemy.MaxHp})");

                var battle = new Battle(player, enemy);
                bool won = battle.Fight();

                if (won)
                {
                    _consecutiveVictories++;
                    Console.WriteLine($"Победа! Побед подряд: {_consecutiveVictories}");

                    // Восстановление HP
                    player.HealFull();

                    // Награда: оружие
                    Weapon reward = enemy.GetRewardWeapon();
                    Console.WriteLine($"Из монстра выпало: {reward.Name} ({reward.Damage} урона, {reward.DamageType})");
                    Console.WriteLine($"Ваше текущее оружие: {player.Weapon.Name} ({player.Weapon.Damage} урона)");

                    Console.WriteLine("Хотите заменить оружие? (y/n)");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        player.Weapon = reward;
                        Console.WriteLine("Оружие заменено!");
                    }

                    // Повышение уровня (если можно)
                    if (player.CanLevelUp())
                    {
                        Console.WriteLine("\nВыберите класс для прокачки:");
                        Console.WriteLine("1 - Воин");
                        Console.WriteLine("2 - Варвар");
                        Console.WriteLine("3 - Разбойник");
                        string lvlInput = Console.ReadLine();

                        string lvlClass = lvlInput switch
                        {
                            "1" => "Воин",
                            "2" => "Варвар",
                            "3" => "Разбойник",
                            _ => "Воин"
                        };

                        player.LevelUp(lvlClass);
                        Console.WriteLine($"Вы повысили уровень в классе: {lvlClass}");
                        player.PrintPlayerInfo();
                    }
                }
                else
                {
                    Console.WriteLine("Вы проиграли! Создайте нового персонажа.");
                    break;
                }
            }

            if (_consecutiveVictories >= 5)
            {
                Console.WriteLine("\n🎉 Поздравляем! Вы победили 5 монстров подряд. Игра пройдена!");
                break;
            }
        }
    }

    private Enemy GenerateRandomEnemy()
    {
        return _random.Next(0, 6) switch
        {
            0 => new Goblin(),
            1 => new Skeleton(),
            2 => new Slime(),
            3 => new Ghost(),
            4 => new Golem(),
            _ => new Dragon()
        };
    }
}