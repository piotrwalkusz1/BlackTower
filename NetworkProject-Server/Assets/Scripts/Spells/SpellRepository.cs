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

    public static SpellData GetSpell(int idSpell)
    {
        return _spells[idSpell];
    }

    public static void SetSpellsFromResources()
    {
        _spells = LoadSpellsFromResources();
    }

    public static int GetSpellsCount()
    {
        return _spells.Count;
    }

    private static List<SpellData> LoadSpellsFromResources()
    {
        var seriallizer = new BinaryFormatter();

        var text = (TextAsset)Resources.Load(Standard.Settings.PATH_TO_SPELLS_IN_RESOURCES, typeof(List<SpellData>));
        var stream = new MemoryStream(text.bytes);

        return PackageConverter.PackageToSpellData((SpellDataPackage[])seriallizer.Deserialize(stream));
    }
}
