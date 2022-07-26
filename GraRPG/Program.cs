// See https://aka.ms/new-console-template for more information
using GameRPG;
using System;

Console.WriteLine("Hello, World!");

Fellowship party = new Fellowship("Drużyna Pierścienia");
party.AddHero(new Warrior());
party.AddHero(new Warrior("Andrzej Dupa", 0.5m, 3));
party.AddHero(new Wizard());
party.AddHero(new Wizard("Marcin Gortat", 1.0m, 6, 12));

Console.WriteLine(party.Heroes[1].HealthChange(-0.47m));
Console.WriteLine(party.ToString());

namespace GameRPG
{
    public abstract class Hero
    {
        protected string Name { get; set; }
        protected decimal HP { get; set; }
        protected int Strength { get; set; }

        internal abstract decimal AttackPower();

        internal decimal HealthChange(decimal hpChange)
        {
            if (HP + hpChange < 0)
            {
                HP = 0;
            }
            else if (HP + hpChange > 1.0m)
            {
                HP = 1.0m;
            }
            else
            {
                HP += hpChange;
            }
            return HP;
        }

        public override string ToString()
        {
            return $"{Name} {HP} {AttackPower()}";
        }
    }

    public class Wizard : Hero
    {
        private int MP { get; set; }

        public Wizard()
        {
            Name = "Xardas";
            HP = 1.0m;
            Strength = Dice.MultipleRoll(1, 6);
            MP = Dice.MultipleRoll(2, 6);
        }
        public Wizard(string name, decimal hp, int strength, int mp)
        {
            Name = name;
            HP = hp;
            Strength = strength;
            MP = mp;
        }

        internal override decimal AttackPower()
        {
            return (MP + Strength) * HP;
        }
    }

    public class Warrior : Hero
    {
        public Warrior()
        {
            Name = "Geralt";
            HP = 1.0m;
            Strength = Dice.MultipleRoll(3, 6);
        }
        public Warrior(string name, decimal hp, int strength)
        {
            Name = name;
            HP = hp;
            Strength = strength;
        }

        internal override decimal AttackPower()
        {
            if (HP < 0.05m)
            {
                return Strength;
            }
            else
            {
                return HP * Strength;
            }
        }
    }

    abstract class Dice
    {
        public static int MultipleRoll(int rolls, int maxDiceValue)
        {
            return new Random().Next(rolls, rolls * maxDiceValue + 1);
        }
    }

    internal class Fellowship : ICloneable
    {
        public string Name { get; set; }
        public List<Hero> Heroes { get; set; } = new List<Hero>();
        public Fellowship(string name)
        {
            Name = name;
        }

        public void AddHero(Hero hero)
        {
            Heroes.Add(hero);
        }

        public object Clone()
        {
            return new Fellowship(Name);
        }
        public decimal AttackSum()
        {
            decimal sum = 0;
            foreach (Hero hero in Heroes)
            {
                sum += hero.AttackPower();
            }
            return sum;
        }
        public override string ToString()
        {
            return $"Group name {Name}" +
                $"\nAttack sum: {AttackSum()}" +
                $"\nCharacter list: {string.Join('\n', Heroes)}";
        }
    }
}
