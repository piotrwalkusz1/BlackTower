using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Monsters
{
    public class Monster
    {
        public Monster()
        {
            _damages = new List<int>();
            _drop = new List<ItemDrop>();
        }

        public int _id;
        public int _maxHp;
        public float _movingSpeed;
        public List<int> _damages;
        public List<ItemDrop> _drop;

        public void AddNewDamage(int dmg)
        {
            _damages.Add(dmg);
        }

        public void AddNewDrop(ItemDrop drop)
        {
            _drop.Add(drop);
        }
    }
}
