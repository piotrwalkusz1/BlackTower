using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Quests
{
    [Serializable]
    public class ItemRewardPackage : IRewardPackage
    {
        public ItemPackage Item { get; set; }

        public ItemRewardPackage(ItemPackage item)
        {
            Item = item;
        }
    }
}
