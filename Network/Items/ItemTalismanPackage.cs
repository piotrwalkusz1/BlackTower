using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class ItemTalismanPackage : ItemPackage
    {
        public int SpellId { get; set; }

        public ItemTalismanPackage(int spellId)
            : base(6)
        {
            SpellId = spellId;
        }
    }
}
