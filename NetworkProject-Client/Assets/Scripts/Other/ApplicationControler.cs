using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection.ToClient;
using NetworkProject.Items;
using NetworkProject.Buffs;
using NetworkProject.Spells;
using Standard;

public class ApplicationControler : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Standard.Settings.SetSettingsFromFileOrResources();

        var spellRepository = Standard.IoC.GetSpellRepository();
        spellRepository.SetSpellsFromResources();
        SpellRepository.Set(spellRepository);

        InputsSystem.Inputs.LoadInputsFromFileOrResources(Standard.Settings.pathToInputsInApplication);

        ItemRepository.SetItemsFromResource(Standard.Settings.pathToItemsInResources);

        Languages.SetLanguage(Standard.Settings.pathToLanguagesInApplication, Standard.Settings.UserConfiguration.DefaultLanguageName);

        BuffRepository.LoadAndSetFromResources(Standard.Settings.pathToBuffsInResources);
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
}
