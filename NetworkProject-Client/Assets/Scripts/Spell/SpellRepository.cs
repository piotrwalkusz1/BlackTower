using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using NetworkProject.Spells;
using UnityEngine;

public static class SpellRepository
{
    private static List<SpellData> _spells;

    static SpellRepository()
    {
        _spells = new List<SpellData>();
    }

    public static SpellData GetSpell(int idSpell)
    {
        return _spells.FirstOrDefault(x => x.IdSpell == idSpell);
    }

    public static void SetSpellsFromResources()
    {
        _spells = LoadSpellsFromResources();
    }

    public static List<SpellData> LoadSpellsFromResources()
    {       
        var textAsset = (TextAsset)Resources.Load(Standard.Settings.pathToSpellsInResources);
        var stream = new MemoryStream(textAsset.bytes);
        var serializer = new BinaryFormatter();
        return new List<SpellData>((SpellData[])serializer.Deserialize(stream));      
    }
}
