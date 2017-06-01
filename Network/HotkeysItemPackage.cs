using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    [Serializable]
    public class HotkeysItemPackage : HotkeysPackage
    {
        public int SlotInBag { get; set; }

        public HotkeysItemPackage(int slotInBag)
        {
            SlotInBag = slotInBag;
        }
    }
}
