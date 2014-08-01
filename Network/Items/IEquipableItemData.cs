using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Benefits;
using NetworkProject.Requirements;

namespace NetworkProject.Items
{
    public interface IEquipableItemManager
    {
        EquipableItemData GetEquipableItemData();
    }
}
