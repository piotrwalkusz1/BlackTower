using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject.Connection.ToServer;

public class TalismanData : ItemData
{
    public int SpellId { get; set; }

    public TalismanData(int spellId)
    {
        IdItem = 6;
        IdPrefabOnScene = 0;
        IdTexture = 13;
        SpellId = spellId;
    }

    public override string GetDescription()
    {
        string description = Languages.GetItemName(6);

        description += " (" + Languages.GetSpellName(SpellId) + ")\n\n";

        description += Languages.GetPhrase("talismanDescription");

        return description;
    }

    public override void UseItem(UseItemInfo info)
    {
        if (info.Player.GetComponent<SpellCaster>().IsSpell(SpellId))
        {
            GUIController.ShowMessage(Languages.GetMessageText(21));
            return;
        }

        var request = new UseTalismanToServer(info.Slot);

        Client.SendRequestAsMessage(request);
    }
}
