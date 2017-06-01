using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemTalisman : Item
{
    public int SpellId { get; set; }
    public override ItemData ItemData
    {
        get { return new TalismanData(SpellId); }
    }

    public ItemTalisman(int spellId)
        : base(6)
    {
        SpellId = spellId;
    }
}
