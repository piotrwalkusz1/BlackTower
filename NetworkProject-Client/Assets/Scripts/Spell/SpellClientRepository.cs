using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using NetworkProject.Spells;
using UnityEngine;

public class SpellClientRepository : ISpellRepository
{
    private List<VisualSpellData> _spells;

    public SpellClientRepository()
    {
        _spells = new List<VisualSpellData>();
    }

    public SpellClientRepository(List<VisualSpellData> spells)
    {
        _spells = new List<VisualSpellData>(spells);
    }

    public SpellData GetSpell(int idSpell)
    {
        return _spells.FirstOrDefault(x => x.IdSpell == idSpell);
    }

    public void SetSpellsFromResources()
    {
        _spells = LoadSpellsFromResources();
    }

    public List<VisualSpellData> LoadSpellsFromResources()
    {       
        var textAsset = (TextAsset)Resources.Load(Standard.Settings.pathToSpellsInResources);
        var stream = new MemoryStream(textAsset.bytes);
        var serializer = new BinaryFormatter();
        return new List<VisualSpellData>((VisualSpellData[])serializer.Deserialize(stream));      
    }
}
