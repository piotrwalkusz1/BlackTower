using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;

[System.CLSCompliant(false)]
static public class ApplicationControler
{
    public static void GoToChoiceCharacterMenu(GoToChoiceCharacterMenuToClient characterChoiceMenuPackage)
    {
        Application.LoadLevel("CharacterChoiceMenu");
        GUIController.SwitchChoiceCharacterMenuGUI(characterChoiceMenuPackage);
    }

    public static void GoToWorld(GoIntoWorldToClient worldInfoPackage)
    {
        SceneBuilder.CreateScene(worldInfoPackage);
        GUIController.SwitchWorldGUI(worldInfoPackage);
    }
}
