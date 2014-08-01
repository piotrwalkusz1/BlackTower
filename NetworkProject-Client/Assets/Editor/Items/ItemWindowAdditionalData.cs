using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace EditorExtension
{
    public class ItemWindowAdditionData : ItemWindowEquipableData
    {
        public ItemWindowAdditionData(AdditionData item) : base(item)
        {

        }

        public ItemWindowAdditionData(VisualEquipableItemData item) : base(item)
        {

        }

        public override string GetItemTypeName()
        {
            return "Addition";
        }
    }
}
