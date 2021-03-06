using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    public enum Type
    {
        Meele,
        Range
    }

    public class Spawner
    {      
        public static Enemy SpawnEnemy(int health, string name, int damage, int id, Type type)
        {
            Enemy enemy = null;

            switch (type)
            {
                case Type.Meele:
                    enemy = new Skeleton(health, name, damage, id);
                    break;
                case Type.Range:
                    enemy = new Mage(health, name, damage, id);
                    break;
            }

            return enemy;
        }
    }
}
