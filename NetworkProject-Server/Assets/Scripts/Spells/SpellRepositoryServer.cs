using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using NetworkProject.Spells;
using UnityEngine;

public class SpellRepositoryServer : ISpellRepository
{
    public List<SpellActionData> _spells;

    public SpellData GetSpell(int idSpell)
    {
        return _spells[idSpell];
    }

    public void SetSpellsFromResources()
    {
        _spells = new List<SpellActionData>();

        var spells = LoadSpellsFromResources();

        foreach (var spell in spells)
        {
            _spells.Add(new SpellActionData(spell, SpellActionsRepository.GetSpellAction(spell.IdSpell)));
        }
    }

    private List<SpellData> LoadSpellsFromResources()
    {
        var seriallizer = new BinaryFormatter();

        var text = (TextAsset)Resources.Load(Standard.Settings.PATH_TO_SPELLS_IN_RESOURCES, typeof(List<SpellData>));
        var stream = new MemoryStream(text.bytes);

        return new List<SpellData>((SpellData[])seriallizer.Deserialize(stream));
    }
}
