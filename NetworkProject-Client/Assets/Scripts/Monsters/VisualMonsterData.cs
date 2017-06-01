using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Monsters;

[Serializable]
public class VisualMonsterData : MonsterData
{
    public int IdPrefabOnScene { get; set; }

    public VisualMonsterData(int idMonster, int idPrefabOnScene)
        : base(idMonster)
    {
        IdPrefabOnScene = idPrefabOnScene;
    }
}
