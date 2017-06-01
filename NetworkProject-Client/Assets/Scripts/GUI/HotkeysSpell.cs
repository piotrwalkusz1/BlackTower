using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HotkeysSpell : HotkeysObject
{
    public int SpellId { get; set; }

    public HotkeysSpell(int spellId)
    {
        SpellId = spellId;
    }

    public override Texture2D GetImage()
    {
        try
        {
            int idTexture = Client.GetNetOwnPlayer().GetComponent<SpellCaster>().GetSpellById(SpellId).SpellData.IdImage;

            return ImageRepository.GetImageByIdImage(idTexture);
        }
        catch
        {
            return null;
        }       
    }

    public override void Use()
    {
        try
        {
            var spellCaster = Client.GetNetOwnPlayer().GetComponent<SpellCaster>();

            spellCaster.GetSpellById(SpellId).TryUseSpell(spellCaster);
        }
        catch
        {
        } 
    }

    public override string GetDescription()
    {
        var spellCaster = Client.GetNetOwnPlayer().GetComponent<SpellCaster>();

        return spellCaster.GetSpellById(SpellId).SpellData.GetDescription(spellCaster);
    }

    public override bool IsEmpty()
    {
        return false;
    }
}
