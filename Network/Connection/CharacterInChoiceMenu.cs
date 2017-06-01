using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

namespace NetworkProject.Connection
{
    [Serializable]
    public class CharacterInChoiceMenu
    {
        public string Name { get; set; }
        public BreedAndGender BreadAndGender { get; set; }
        public PlayerEquipedItemsPackage EquipedItems { get; set; }

        public CharacterInChoiceMenu(string name, BreedAndGender breadAndGender, PlayerEquipedItemsPackage equipedItems)
        {
            Name = name;
            BreadAndGender = breadAndGender;
            EquipedItems = equipedItems;
        }
    }
}
