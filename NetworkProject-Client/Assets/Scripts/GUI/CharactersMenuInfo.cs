using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Items;

public class CharactersMenuInfo
{
    public CharacterInChoiceMenu[] Characters { get; set; }
    public int SelectedCharacter { get; set; }
    public bool IsCreateNewCharacter { get; set; }
    public CharacterInChoiceMenu NewCharacter { get; set; }
    public Vector2 ScrollPosition { get; set; }

    public CharactersMenuInfo(CharacterInChoiceMenu[] characters)
    {
        Characters = characters;
        IsCreateNewCharacter = false;
        SelectedCharacter = 0;

        var breedAndGender = new BreedAndGender(0, true);
        var equipedItems = new PlayerEquipedItemsPackage(IoC.GetBodyParts().ToList());
        NewCharacter = new CharacterInChoiceMenu("", breedAndGender, equipedItems);
    }

    public CharacterInChoiceMenu GetSelectedCharacter()
    {
        return Characters[SelectedCharacter];
    }
}
