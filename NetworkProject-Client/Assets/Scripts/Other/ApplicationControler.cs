using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
static public class ApplicationControler
{
    public static void GoToChoiceCharacterMenu(CharacterChoiceMenuPackage characterChoiceMenuPackage)
    {
        Application.LoadLevel("CharacterChoiceMenu");
        GUIController.SwitchChoiceCharacterMenuGUI(characterChoiceMenuPackage);
    }

    public static void GoToWorld(WorldInfoPackage worldInfoPackage)
    {
        SceneBuilder.CreateScene(worldInfoPackage);
        GUIController.SwitchWorldGUI(worldInfoPackage);
    }
}
