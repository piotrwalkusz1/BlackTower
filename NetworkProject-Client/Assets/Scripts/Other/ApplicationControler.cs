using UnityEngine;
using System;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;
using NetworkProject.Items;
using NetworkProject.Buffs;
using NetworkProject.Spells;
using Standard;

public class ApplicationControler : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Standard.Settings.SetSettingsFromFileOrResources();

        SpellRepository.SetSpellsFromResources();

        InputsSystem.Inputs.LoadInputsFromFileOrResources(Standard.Settings.pathToInputsInApplication);

        ItemRepository.SetItemsFromResource(Standard.Settings.pathToItemsInResources);

        Languages.SetLanguage(Standard.Settings.pathToLanguagesInApplication, Standard.Settings.UserConfiguration.DefaultLanguageName);

        BuffRepository.LoadAndSetFromResources(Standard.Settings.pathToBuffsInResources);

        ConversationRepository.LoadAndSetConversations();
    }

    public static void GoToLoginMenu()
    {
        Application.LoadLevel("Restart");
        GUIController.SwitchLoginMenuGUI(new LoginMenuInfo());
        GUIController.HidePlayerInterface();
    }

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

    public static void Quit()
    {
        Application.Quit();
    }
}
