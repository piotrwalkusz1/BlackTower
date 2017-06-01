using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Spells;
using NetworkProject.Monsters;
using NetworkProject.Items;
using NetworkProject.Requirements;
using NetworkProject.Benefits;
using NetworkProject.BodyParts;
using NetworkProject.Buffs;
using NetworkProject.Quests;

public static class PackageConverter
{
    #region ToPackage

    public static List<SpellDataPackage> SpellDataToPackage(SpellData[] spells)
    {
        var result = new List<SpellDataPackage>();

        foreach (var spell in spells)
        {
            result.Add(SpellDataToPackage(spell));
        }

        return result;
    }

    public static SpellDataPackage SpellDataToPackage(SpellData spell)
    {
        return new SpellDataPackage(spell.IdSpell, spell.ManaCost, PackageConverter.RequirementToPackage(spell.Requirements.ToArray()).ToArray(),
            spell.Cooldown, spell.RequiredInfo);
    }

    public static List<ItemDataPackage> ItemDataToPackage(ItemData[] items)
    {
        var result = new List<ItemDataPackage>();

        foreach (var item in items)
        {
            result.Add(item.ToPackage());
        }

        return result;
    }

    public static List<ItemPackage> ItemToPackage(Item[] items)
    {
        var result = new List<ItemPackage>();

        foreach (var item in items)
        {
            result.Add(ItemToPackage(item));
        }

        return result;
    }

    public static ItemPackage ItemToPackage(Item item)
    {
        if (item == null)
        {
            return null;
        }
        else if (item is ItemTalisman)
        {
            var itemTalisman = (ItemTalisman)item;

            return new ItemTalismanPackage(itemTalisman.SpellId);
        }
        else
        {
            return new ItemPackage(item.IdItem);
        }
    }

    public static IBenefitPackage[] BenefitToPackage(IBenefit[] benefits)
    {
        List<IBenefitPackage> result = new List<IBenefitPackage>();

        foreach (var benefit in benefits)
        {
            result.Add(benefit.ToPackage());
        }

        return result.ToArray();
    }   

    public static IBenefitPackage[][] BenefitToPackage(IBenefit[][] benefits)
    {
        List<IBenefitPackage[]> result = new List<IBenefitPackage[]>();

        foreach (var benefit in benefits)
        {
            result.Add(BenefitToPackage(benefit));
        }

        return result.ToArray();
    }

    public static BuffDataPackage BuffDataToPackage(BuffData buff)
    {
        return new BuffDataPackage(buff.IdBuff, BenefitToPackage(buff.GetBenefits()));
    }

    public static List<BuffDataPackage> BuffDataToPackage(BuffData[] buffs)
    {
        var result = new List<BuffDataPackage>();

        foreach (var buff in buffs)
        {
            result.Add(BuffDataToPackage(buff));
        }

        return result;
    }

    public static List<IRequirementPackage> RequirementToPackage(IRequirement[] requirements)
    {
        var result = new List<IRequirementPackage>();

        foreach (var requirement in requirements)
        {
            if (requirement is LvlRequirement) result.Add(new LvlRequirementPackage(((LvlRequirement)requirement).Value));
            else throw new NotImplementedException("Nie ma takiego wymagania.");
        }

        return result;
    }

    public static List<IUseActionPackage> UseActionToPackage(IUseAction[] actions)
    {
        var result = new List<IUseActionPackage>();

        foreach (var action in actions)
        {
            result.Add(action.GetPackage());
        }

        return result;
    }

    public static List<HotkeysPackage> HotkeysToPackage(HotkeysObject[] hotkeys)
    {
        var result = new List<HotkeysPackage>();

        foreach (var hotkey in hotkeys)
        {
            result.Add(HotkeysToPackage(hotkey));
        }

        return result;
    }

    public static HotkeysPackage HotkeysToPackage(HotkeysObject hotkey)
    {
        if(hotkey == null)
        {
            return null;
        }
        else if (hotkey is HotkeysSpell)
        {
            return new HotkeysSpellPackage(((HotkeysSpell)hotkey).SpellId);
        }
        else if (hotkey is HotkeysUsableItem)
        {
            return new HotkeysItemPackage(((HotkeysUsableItem)hotkey).SlotInBag);
        }
        else
        {
            throw new NotImplementedException("Nie ma takiego hotkey");
        }
    }

    #endregion

    #region FromPackage

    public static Shop PackageToShop(ShopPackage shop)
    {
        return new Shop(PackageToItemInShop(shop.Items.ToArray()).ToArray());
    }

    public static List<ItemInShop> PackageToItemInShop(ItemInShopPackage[] items)
    {
        var result = new List<ItemInShop>();

        foreach (var item in items)
        {
            result.Add(PackageToItemInShop(item));
        }

        return result;
    }

    public static ItemInShop PackageToItemInShop(ItemInShopPackage item)
    {
        return new ItemInShop(PackageConverter.PackageToItem(item.Item), item.Price);
    }

    public static List<Quest> PackageToQuest(QuestDataPackage[] quests)
    {
        var result = new List<Quest>();

        foreach (var quest in quests)
        {
            result.Add(PackageToQuest(quest));
        }

        return result;
    }

    public static Quest PackageToQuest(QuestDataPackage quest)
    {
        return new Quest(quest.IdQuest, quest.NetIdQuester, PackageToQuestStatus(quest.QuestStatus), PackageToTarget(quest.GetTargets()).ToArray(),
            PackageToReward(quest.GetRewards()).ToArray(), quest.GetRequiredCompletedQuests());
    }

    public static QuestStatus PackageToQuestStatus(QuestStatusPackage status)
    {
        switch (status)
        {
            case QuestStatusPackage.NoTaken:
                return QuestStatus.NoTaken;
            case QuestStatusPackage.InProgress:
                return QuestStatus.InProgress;
            case QuestStatusPackage.Completed:
                return QuestStatus.Completed;
            case QuestStatusPackage.Returned:
                return QuestStatus.Returned;
            default:
                throw new NotImplementedException("Nie ma takiego statusu questa");
        }
    }

    public static List<IReward> PackageToReward(IRewardPackage[] rewards)
    {
        var result = new List<IReward>();

        foreach(var reward in rewards)
        {
            result.Add(PackageToReward(reward));
        }

        return result;
    }

    public static IReward PackageToReward(IRewardPackage reward)
    {
        if (reward is ExpRewardPackage) return new ExpReward(((ExpRewardPackage)reward).Exp);
        else if (reward is GoldRewardPackage) return new GoldReward(((GoldRewardPackage)reward).Gold);
        else if (reward is ItemRewardPackage) return new ItemReward(PackageToItem(((ItemRewardPackage)reward).Item));
        else throw new NotImplementedException("Nie ma takiej nagrody : " + reward.GetType().Name);
    }

    public static List<IQuestTarget> PackageToTarget(IQuestTargetPackage[] targets)
    {
        var result = new List<IQuestTarget>();

        foreach(var target in targets)
        {
            result.Add(PackageToTarget(target));
        }

        return result;
    }

    public static IQuestTarget PackageToTarget(IQuestTargetPackage target)
    {
        if (target is KillQuestTargetPackage)
        {
            KillQuestTargetPackage tar = (KillQuestTargetPackage)target;
            return new KillQuestTarget(tar.TargetKill, tar.MonsterId, tar.AlreadyKill);
        }
        else throw new NotImplementedException("Nie ma takiego targeta : " + target.GetType().Name);
    }

    public static Buff PackageToBuff(BuffPackage buff)
    {
        return new Buff(buff.IdBuff, buff.LvlBuff, buff.EndTime);
    }

    public static Item PackageToItem(ItemPackage item)
    {
        if (item == null)
        {
            return null;
        }
        else if (item is ItemTalismanPackage)
        {
            var itemTalisman = (ItemTalismanPackage)item;

            return new ItemTalisman(itemTalisman.SpellId);
        }
        else
        {
            return new Item(item.IdItem);
        }
    }

    public static Spell PackageToSpell(SpellPackage spell)
    {
        return new Spell(spell.IdSpell, spell.LvlSpell, spell.NextUseTime);
    }

    public static BodyPart PackageToBodyPart(BodyPartPackage bodyPart)
    {
        if (bodyPart is HeadPackage) return new Head(bodyPart.EquipedItem == null ? null : new Item(bodyPart.EquipedItem.IdItem));
        else if (bodyPart is ChestPackage) return new Chest(bodyPart.EquipedItem == null ? null : new Item(bodyPart.EquipedItem.IdItem));
        else if (bodyPart is FeetPackage) return new Feet(bodyPart.EquipedItem == null ? null : new Item(bodyPart.EquipedItem.IdItem));
        else if (bodyPart is RightHandPackage) return new RightHand(bodyPart.EquipedItem == null ? null : new Item(bodyPart.EquipedItem.IdItem));
        else if (bodyPart is LeftHandPackage) return new LeftHand(bodyPart.EquipedItem == null ? null : new Item(bodyPart.EquipedItem.IdItem));
        else if (bodyPart is OtherPackage) return new OtherBodyPart(bodyPart.EquipedItem == null ? null : new Item(bodyPart.EquipedItem.IdItem));
        else throw new NotImplementedException("Nie ma takiej części ciała.");
    }  

    public static List<Spell> PackageToSpell(SpellPackage[] spells)
    {
        var result = new List<Spell>();

        foreach (var spell in spells)
        {
            result.Add(PackageToSpell(spell));
        }

        return result;
    }

    public static List<Item> PackageToItem(ItemPackage[] items)
    {
        var result = new List<Item>();

        foreach (var item in items)
        {
            result.Add(PackageToItem(item));
        }

        return result;
    }

    public static List<IRequirement> PackageToRequirement(IRequirementPackage[] requirements)
    {
        var result = new List<IRequirement>();

        foreach (var requirement in requirements)
        {
            if (requirement is LvlRequirementPackage) result.Add(new LvlRequirement(((LvlRequirementPackage)requirement).RequiredLvl));
            else throw new NotImplementedException("Nie ma takiego wymagania.");
        }

        return result;
    }

    public static List<BodyPart> PackageToBodyPart(BodyPartPackage[] bodyParts)
    {
        var result = new List<BodyPart>();

        foreach (var bodyPart in bodyParts)
        {
            result.Add(PackageToBodyPart(bodyPart));
        }

        return result;
    }

    public static List<HotkeysObject> PackageToHotkeys(HotkeysPackage[] hotkeys)
    {
        var result = new List<HotkeysObject>();

        foreach (var hotkey in hotkeys)
        {
            result.Add(PackageToHotkeys(hotkey));
        }

        return result;
    }

    public static HotkeysObject PackageToHotkeys(HotkeysPackage hotkey)
    {
        if (hotkey == null)
        {
            return null;
        }
        else if (hotkey is HotkeysSpellPackage)
        {
            return new HotkeysSpell(((HotkeysSpellPackage)hotkey).IdSpell);
        }
        else if (hotkey is HotkeysItemPackage)
        {
            return new HotkeysUsableItem(((HotkeysItemPackage)hotkey).SlotInBag);
        }
        else
        {
            throw new NotImplementedException("Nie ma takiego hotkey");
        }
    }

    #endregion
}
