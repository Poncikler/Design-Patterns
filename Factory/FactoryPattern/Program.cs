using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{  
    class Skeleton : Enemy
    {
        public Skeleton(int health,string name,int damage,int id) : base(health,name,damage,id)
        {
            Console.WriteLine("Skeleton is ready!");
        }

        public override void Shout()
        {
            Console.WriteLine("TASTE MY BONE!");
        }
    }
    class Mage : Enemy
    {
        public Mage(int health, string name, int damage, int id) : base(health, name, damage, id)
        {
            Console.WriteLine("Mage is ready!");
        }

        public override void Shout()
        {
            Console.WriteLine("Kul e'ûzü birabbinnâs".ToUpper());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Enemy pitircik = Spawner.SpawnEnemy(70, "Iskelet Abi", 30, 1, Type.Meele);
            pitircik.Shout();

            Enemy imam = Spawner.SpawnEnemy(100, "Imam Abi", 50, 2, Type.Range);
            imam.Shout();

            pitircik.TakeDamage(imam);

            imam.TakeDamage(pitircik);         

            Console.ReadLine();
        }
    }
}
