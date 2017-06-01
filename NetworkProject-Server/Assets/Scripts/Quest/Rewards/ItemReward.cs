using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Quests;

public class ItemReward : IReward
{
    public Item Item { get; set; }
    private int _changedSlot;

    public ItemReward(Item item)
    {
        Item = item;
    }

    public void GiveReward(IRewardable recipient)
    {
        _changedSlot = recipient.AddItem(Item);
    }

    public void SelectChange(RewardSenderUpdate sender)
    {
        sender.ItemBagChangedSlots.Add(_changedSlot);
    }

    public IRewardPackage ToPackage()
    {
        return new ItemRewardPackage(PackageConverter.ItemToPackage(Item));
    }
}
