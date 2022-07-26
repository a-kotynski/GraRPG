using GameRPG;
using System;
Console.WriteLine("Hello, World!");
Fellowship party = new Fellowship("Drużyna Pierścienia");
party.AddHero(new Warrior());
party.AddHero(new Warrior("Andrzej Dupa", 0.5f, 3));
party.AddHero(new Wizard());
party.AddHero(new Wizard("Marcin Gortat", 1.0f, 6, 12));
party.Heroes[1].HealthChange(0.47f);
Console.WriteLine(party.ToString());
namespace GameRPG
{
    public abstract class Hero
    {
        protected string Name { get; set; }
        protected float HP { get; set; }
        protected int Strength { get; set; }
        internal float HealthChange(float hpChange)
        {
            if (HP + hpChange < 0)
            {
                return 0;
            }
            else if (HP + hpChange > 1.0f)
            {
                return 1.0f;
            }
            else
            {
                return HP + hpChange;
            }
        }
        internal abstract float AttackPower();
    }
    public class Wizard : Hero
    {
        private int MP { get; set; }
        public Wizard()
        {
            Name = "Xardas";
            HP = 1.0f;
            Strength = Dice.MultipleRoll(1, 6);
            MP = Dice.MultipleRoll(2, 6);
        }
        public Wizard(string name, float hp, int strength, int mp)
        {
            Name = name;
            HP = hp;
            Strength = strength;
            MP = mp;
        }
        internal override float AttackPower()
        {
            return (MP + Strength) * HP;
        }
        public override string ToString()
        {
            return $"{Name} {HP} {AttackPower()}";
        }
    }
    public class Warrior : Hero
    {
        public Warrior()
        {
            Name = "Geralt";
            HP = 1.0f;
            Strength = Dice.MultipleRoll(3, 6);
        }
        public Warrior(string name, float hp, int strength)
        {
            Name = name;
            HP = hp;
            Strength = strength;
        }
        internal override float AttackPower()
        {
            if (HP < 0.05f)
            {
                return Strength;
            }
            else
            {
                return HP * Strength;
            }
        }
        public override string ToString()
        {
            return $"{Name} {HP} {AttackPower()}";
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
        public float AttackSum()
        {
            float sum = 0;
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
                $"\nCharacter list: {Heroes.ToString()}";
        }
    }
}