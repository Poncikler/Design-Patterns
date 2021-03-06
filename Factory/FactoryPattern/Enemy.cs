using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    public abstract class Enemy
    {
        public Enemy(int health, string name, int damage, int id)
        {
            Health = health;
            Name = name;
            Damage = damage;
            ID = id;
        }

        public int Health { get; private set; }
        public string Name { get; private set; }
        public int Damage { get; private set; }
        public int ID { get; private set; }
        public void TakeDamage(Enemy enemy)
        {
            Health -= enemy.Damage;
            Console.WriteLine(Name + " hasar alıyor.Aaaaaaah bu acıttı! Kalan canım : " + Health);
        }
        public abstract void Shout();
    }
}
