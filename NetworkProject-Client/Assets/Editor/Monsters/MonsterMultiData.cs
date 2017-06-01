using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Monsters;
using NetworkProject.Items;

namespace EditorExtension
{
    [Serializable]
    public class MonsterMultiData
    {
        public int IdMonster
        {
            get { return _monsterClientVersion.IdMonster; }
            set
            {   
                _monsterClientVersion.IdMonster = value;
                _monsterServerVersion.IdMonster = value;
            }
        }

        public int IdPrefabOnScene
        {
            get { return _monsterClientVersion.IdPrefabOnScene; }
            set { _monsterClientVersion.IdPrefabOnScene = value; }
        }

        public int MaxHP
        {
            get { return _monsterServerVersion.Stats.MaxHP; }
            set { _monsterServerVersion.Stats.MaxHP = value; }
        }

        public int MinDmg
        {
            get { return MonsterServerVersion.MinDmg; }
            set { MonsterServerVersion.MinDmg = value; }
        }

        public int MaxDmg
        {
            get { return MonsterServerVersion.MaxDmg; }
            set { MonsterServerVersion.MaxDmg = value; }
        }

        public int AttackSpeed
        {
            get { return MonsterServerVersion.AttackSpeed; }
            set { MonsterServerVersion.AttackSpeed = value; }
        }

        public float[] MovementSpeed
        {
            get { return MonsterServerVersion.MovementSpeed; }
            set { MonsterServerVersion.MovementSpeed = value; }
        }

        public ItemDrop[] Drop
        {
            get { return MonsterServerVersion.Drop.ToArray(); }
            set { MonsterServerVersion.Drop = new List<ItemDrop>(value); }
        }

        public VisualMonsterData MonsterClientVersion
        {
            get { return _monsterClientVersion; }
            set { _monsterClientVersion = value; }
        }

        public MonsterFullData MonsterServerVersion
        {
            get { return _monsterServerVersion; }
            set { _monsterServerVersion = value; }
        }

        private VisualMonsterData _monsterClientVersion;
        private MonsterFullData _monsterServerVersion;

        public MonsterMultiData()
        {
            _monsterClientVersion = new VisualMonsterData(0, 0);
            _monsterServerVersion = new MonsterFullData(0, new MonsterStatsInRepository());
        }

        public MonsterMultiData(VisualMonsterData monsterVersionClient, MonsterFullData monsterServerVersion)
        {
            _monsterClientVersion = monsterVersionClient;
            _monsterServerVersion = monsterServerVersion;
        }
    }
}
