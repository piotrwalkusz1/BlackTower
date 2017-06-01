using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ItemTalisman : Item
{
    public int SkillId { get; set; }
    public override ItemData ItemData
    {
        get { return new ItemTalismanData(); }
    }

    public ItemTalisman(int skillId)
        : base(6)
    {
        SkillId = skillId;
    }
}
