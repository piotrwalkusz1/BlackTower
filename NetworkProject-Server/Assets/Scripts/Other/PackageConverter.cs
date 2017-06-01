using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Spells;
using NetworkProject.Requirements;
using NetworkProject.Items;
using NetworkProject.BodyParts;
using NetworkProject.Buffs;
using NetworkProject.Benefits;
using NetworkProject.Quests;

public static class PackageConverter
{
    #region ToPackage

    public static List<QuestDataPackage> QuestDataToPackage(QuestData[] quests)
    {
        var result = new List<QuestDataPackage>();

        foreach (var quest in quests)
        {
            result.Add(QuestDataToPackage(quest));
        }

        return result;
    }

    public static QuestDataPackage QuestDataToPackage(QuestData quest)
    {
        return new QuestDataPackage(quest.IdQuest, -1, QuestStatusPackage.NoTaken, TargetDataToPackage(quest.Targets.ToArray()).ToArray(),
            RewardToPackage(quest.Rewards.ToArray()).ToArray(), quest.RequiredCompletedQuests.ToArray()); 
    }

    public static List<IRewardPackage> RewardToPackage(IReward[] rewards)
    {
        var result = new List<IRewardPackage>();

        foreach (var reward in rewards)
        {
            result.Add(reward.ToPackage());
        }

        return result;
    }

    public static List<IQuestTargetPackage> TargetToPackage(IQuestTarget[] targets)
    {
        var result = new List<IQuestTargetPackage>();

        foreach (var target in targets)
        {
            result.Add(target.ToPackage());
        }

        return result;
    }

    public static List<IQuestTargetPackage> TargetDataToPackage(IQuestTargetData[] targets)
    {
        var result = new List<IQuestTargetPackage>();

        foreach (var target in targets)
        {
            result.Add(target.ToPackage());
        }

        return result;
    }

    public static PlayerEquipedItemsPackage PlayerEquipedItemsToPackage(PlayerEquipment eq)
    {
        return new PlayerEquipedItemsPackage(BodyPartToPackage(eq.GetBodyParts()));
    }

    public static EquipmentPackage EquipmentToPackage(PlayerEquipment eq)
    {
        return new EquipmentPackage(ItemToPackage(eq.GetItemsFromBag()).ToArray(), eq.Gold);
    }

    public static List<SpellPackage> SpellToPackage(Spell[] spells)
    {
        var result = new List<SpellPackage>();

        foreach (var spell in spells)
        {
            result.Add(SpellToPackage(spell));
        }

        return result;
    }

    public static SpellPackage SpellToPackage(Spell spell)
    {
        return new SpellPackage(spell.IdSpell, spell.LvlSpell, spell.NextUseTime);
    }

    public static List<BodyPartPackage> BodyPartToPackage(BodyPart[] bodyParts)
    {
        var result = new List<BodyPartPackage>();

        foreach (var bodyPart in bodyParts)
        {
            result.Add(BodyPartToPackage(bodyPart));
        }

        return result;
    }

    public static BodyPartPackage BodyPartToPackage(BodyPart bodyPart)
    {
        if (bodyPart is Head) return new HeadPackage(ItemToPackage(bodyPart.EquipedItem));
        else if (bodyPart is Chest) return new ChestPackage(ItemToPackage(bodyPart.EquipedItem));
        else if (bodyPart is Feet) return new FeetPackage(ItemToPackage(bodyPart.EquipedItem));
        else if (bodyPart is RightHand) return new RightHandPackage(ItemToPackage(bodyPart.EquipedItem));
        else if (bodyPart is LeftHand) return new LeftHandPackage(ItemToPackage(bodyPart.EquipedItem));
        else if (bodyPart is OtherBodyPart) return new OtherPackage(ItemToPackage(bodyPart.EquipedItem));
        else throw new NotImplementedException("Nie ma takiej części ciała.");
    }

    public static List<ItemInShopPackage> ItemInShopToPackage(ItemInShop[] items)
    {
        var result = new List<ItemInShopPackage>();

        foreach (var item in items)
        {
            result.Add(ItemInShopToPackage(item));
        }

        return result;
    }

    public static ItemInShopPackage ItemInShopToPackage(ItemInShop item)
    {
        return new ItemInShopPackage(ItemToPackage(item.Item), item.Price);
    }

    public static List<ItemPackage> ItemToPackage(Item[] items)
    {
        var result = new List<ItemPackage>();

        foreach (var item in items)
        {
            result.Add(PackageConverter.ItemToPackage(item));
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

            return new ItemTalismanPackage(itemTalisman.SkillId);
        }
        else
        {
            return new ItemPackage(item.IdItem);
        }
    }

    #endregion

    #region FromPackage

    public static List<BuffData> PackageToBuffData(BuffDataPackage[] buffs)
    {
        var result = new List<BuffData>();

        foreach (var buff in buffs)
        {
            result.Add(PackageToBuffData(buff));
        }

        return result;
    }

    public static BuffData PackageToBuffData(BuffDataPackage buff)
    {
        return new BuffData(buff.IdBuff, PackageToBenefit(buff.GetBenefits()));
    }

    public static IBenefit[][] PackageToBenefit(IBenefitPackage[][] benefits)
    {
        var result = new List<IBenefit[]>();

        foreach (var benefit in benefits)
        {
            result.Add(PackageToBenefit(benefit));
        }

        return result.ToArray();
    }

    public static IBenefit[] PackageToBenefit(IBenefitPackage[] benefits)
    {
        var result = new List<IBenefit>();

        foreach (var benefit in benefits)
        {
            result.Add(PackageToBenefit(benefit));
        }

        return result.ToArray();
    }

    public static IBenefit PackageToBenefit(IBenefitPackage benefit)
    {
        if (benefit is AdditionalMaxHpPackage) return new AdditionalMaxHP(((AdditionalMaxHpPackage)benefit).Value);
        else throw new NotImplementedException("Nie ma takiego benefitu : " + benefit.GetType().Name);
    }

    public static List<SpellData> PackageToSpellData(SpellDataPackage[] spells)
    {
        var result = new List<SpellData>();

        foreach (var spell in spells)
        {
            result.Add(PackageToSpellData(spell));
        }

        return result;
    }

    public static SpellData PackageToSpellData(SpellDataPackage spell)
    {
        return new SpellData(spell.IdSpell, spell.ManaCost, PackageToRequirement(spell.GetRequirements()).ToArray(), spell.Cooldown,
            SpellActionsRepository.GetSpellAction(spell.IdSpell));
    }

    public static List<IRequirement> PackageToRequirement(IRequirementPackage[] requirements)
    {
        var result = new List<IRequirement>();

        foreach (var requirement in requirements)
        {
            result.Add(PackageToRequirement(requirement));
        }

        return result;
    }

    public static IRequirement PackageToRequirement(IRequirementPackage requirement)
    {
        if (requirement is LvlRequirementPackage) return new LvlRequirement(((LvlRequirementPackage)requirement).RequiredLvl);
        else throw new NotImplementedException("Nie ma takiego wymagania " + requirement.GetType().Name);
    }

    public static List<ItemData> PackageToItemData(ItemDataPackage[] items)
    {
        var result = new List<ItemData>();

        foreach (var item in items)
        {
            result.Add(PackageToItemData(item));
        }

        return result;
    }

    public static ItemData PackageToItemData(ItemDataPackage item)
    {
        if (item is EquipableItemPackage)
        {
            var equipedItem = (EquipableItemPackage)item;
            var benefits = PackageToBenefit(equipedItem.GetBenefits());
            var requirements = PackageToRequirement(equipedItem.GetRequirements()).ToArray();

            if (item is HelmetPackage)
            {
                var i = (HelmetPackage)item;
                return new HelmetData(i.IdItem, benefits, requirements, i._defense);
            }
            else if (item is ArmorPackage)
            {
                var i = (ArmorPackage)item;
                return new ArmorData(i.IdItem, benefits, requirements, i._defense);
            }
            else if (item is ShoesPackage)
            {
                var i = (ShoesPackage)item;
                return new ShoesData(i.IdItem, benefits, requirements, i._movementSpeed);
            }
            else if (item is WeaponPackage)
            {
                var i = (WeaponPackage)item;
                return new WeaponData(i.IdItem, benefits, requirements, i._minDmg, i._maxDmg, i._attackSpeed);
            }
            else if (item is ShieldPackage)
            {
                var i = (ShieldPackage)item;
                return new ShieldData(i.IdItem, benefits, requirements, i._defense);
            }
            else if (item is AdditionPackage)
            {
                var i = (AdditionPackage)item;
                return new AdditionData(i.IdItem, benefits, requirements);
            }
            else throw new NotImplementedException("Nie ma takiego ekwipunku : " + item.GetType().Name);
        }
        else if (item is UsableItemPackage)
        {
            var i = (UsableItemPackage)item;
            return new ItemUsableData(i.IdItem, PackageConverter.PackageToUseAction(i.Actions.ToArray()).ToArray());
        }
        else
        {
            return new ItemData(item.IdItem);
        }
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

    public static List<Item> PackageToItem(ItemPackage[] items)
    {
        var result = new List<Item>();

        foreach (var item in items)
        {
            result.Add(PackageConverter.PackageToItem(item));
        }

        return result;
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

    public static List<Spell> PackageToSpell(SpellPackage[] spells)
    {
        var result = new List<Spell>();

        foreach (var spell in spells)
        {
            result.Add(PackageToSpell(spell));
        }

        return result;
    }

    public static Spell PackageToSpell(SpellPackage spell)
    {
        return new Spell(spell.IdSpell, spell.LvlSpell, spell.NextUseTime);
    }

    public static List<IUseAction> PackageToUseAction(IUseActionPackage[] actions)
    {
        var result = new List<IUseAction>();

        foreach (var action in actions)
        {
            result.Add(PackageToUseAction(action));
        }

        return result;
    }

    public static IUseAction PackageToUseAction(IUseActionPackage action)
    {
        if (action is UseActionAddHPPackage) return new UseActionAddHP(((UseActionAddHPPackage)action).Value);
        else { throw new NotImplementedException("Nie ma takiego UseAction : " + action.GetType().Name); }
    }

    #endregion
}
