using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class MonsterInfo
    {
        public MonsterInfo()
        {
            _damages = new ListPackage<IntPackage>();
            _itemsToDrop = new ListPackage<IntPackage>();
            _chancesToDrop = new ListPackage<FloatPackage>();
        }

        public int _id;
        public int _maxHp;
        public float _movingSpeed;
        public ListPackage<IntPackage> _damages;
        public ListPackage<IntPackage> _itemsToDrop;
        public ListPackage<FloatPackage> _chancesToDrop;

        public void AddNewDamage(int dmg)
        {
            _damages.List.Add(dmg);
        }

        public void AddNewDrop(int idItem, float chance)
        {
            _itemsToDrop.Add(idItem);
            _chancesToDrop.Add(chance);
        }
    }
}
