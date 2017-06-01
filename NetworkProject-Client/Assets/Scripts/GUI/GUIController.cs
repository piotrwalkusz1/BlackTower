using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Standard;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Connection.ToServer;

enum Place
{
    ConnectionMenu,
    LoginMenu,
    RegisterMenu,
    ChoiceCharacterMenu,
    World, 
}

public class GUIController : MonoBehaviour
{
    public GUISkin _skin;
    public Texture2D _helpImage;
    public static Texture2D HelpImage { get; set; }
    public static Interlocutor CurrentInterlocutor
    {
        get { return _dialogWindow.Interlocutor; }
    }
    public static int SizeItemImage
    {
        get { return _equipmentWindow._sizeItemImage; }
    }  

    static private Place _place;
    static private object _infoAboutCurrentPlace;

    static private bool _isFreezedScreen;
    static private Texture2D _freezedScreenTexture;
    static private WindowGUI _freezingWindow;

    static private List<WindowGUI> _windows;
    static private List<WindowGUI> _windowsToClose;
    static private int _nextWindowId = 1;

    static private int _stackPosition = 1;

    static private EquipmentWindow _equipmentWindow;
    static private CharacterWindow _characterWindow;
    static private QuestProposalWindow _questProposalWindow;
    static private DialogWindow _dialogWindow;
    static private QuestManagerWindow _questManagerWindow;
    static private GUIMagicBook _magicBook;
    static private HPBar _hpBar;
    static private MPBar _mpBar;
    static private GUIDescription _description;
    static private GUIShop _shop;
    static private GameObject _hotkeys;

    void Awake()
    {
        _windows = new List<WindowGUI>();
        _windowsToClose = new List<WindowGUI>();
        DontDestroyOnLoad(gameObject);

        var connectionMenu = new ConnectionMenuInfo();
        //StartCoroutine(Client.RefreshHostList(connectionMenu));
        SwitchConnectionMenu(connectionMenu);

        HelpImage = _helpImage;
    }

    void Start()
    {
        GameObject window = Instantiate(Prefabs.GUIEquipment, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _equipmentWindow = window.GetComponent<EquipmentWindow>();
        _equipmentWindow._closeButtonRect = new Rect(245, 5, 22, 22);
        _equipmentWindow._externalBorder = new Vector2(6, 32);
        _equipmentWindow._internalBorder = 4;
        _equipmentWindow._sizeItemImage = 40;
        _equipmentWindow.gameObject.SetActive(false);

        _equipmentWindow.Initialize();

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUICharacter, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _characterWindow = window.GetComponent<CharacterWindow>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUIQuestProposal, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _questProposalWindow = window.GetComponent<QuestProposalWindow>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUIDialog, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _dialogWindow = window.GetComponent<DialogWindow>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUIQuestManager, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _questManagerWindow = window.GetComponent<QuestManagerWindow>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.HPBar, new Vector3(0, 1, 10000), Quaternion.identity) as GameObject;
        _hpBar = window.GetComponent<HPBar>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.MPBar, new Vector3(0, 1, 10000), Quaternion.identity) as GameObject;
        _mpBar = window.GetComponent<MPBar>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUIDescription, new Vector3(0, 0, 10000), Quaternion.identity) as GameObject;
        _description = window.GetComponent<GUIDescription>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUIMagicBook, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _magicBook = window.GetComponent<GUIMagicBook>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUIShop, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _shop = window.GetComponent<GUIShop>();
        window.SetActive(false);

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUIHotkeys, new Vector3(0.5f, 0, 0), Quaternion.identity) as GameObject;
        _hotkeys = window;
        window.SetActive(false);

        DontDestroyOnLoad(window);
    }

    void OnGUI()
    {
        GUI.skin = _skin;

        RemoveWindows();

        if (_isFreezedScreen)
        {
            DrawFreezedScreen();
        }
        else
        {
            switch (_place)
            {
                case Place.ConnectionMenu:
                    DrawConnectionMenu();
                    break;
                case Place.LoginMenu:
                    DrawLoginMenuGUI();
                    break;
                case Place.RegisterMenu:
                    DrawRegisterMenuGUI();
                    break;
                case Place.ChoiceCharacterMenu:
                    DrawChoiceCharacterMenuGUI();
                    break;
                case Place.World:
                    DrawWorldGUI();
                    break;  
                default:
                    MonoBehaviour.print("Nie ma takiego miejsca GUI");
                    break;
            }

            DrawWindows();
        }       
    }

    public static void ShowShop(Shop shop, int netIdNPC)
    {
        _shop.gameObject.SetActive(true);

        _shop.Refresh(shop, netIdNPC);

        RefreshPlayerControl();
    }

    public static void HideShop()
    {
        _shop.gameObject.SetActive(false);

        RefreshPlayerControl();
    }

    public static bool IsShopActive()
    {
        return _shop.gameObject.activeInHierarchy;
    }

    public static void ShowMagicBook()
    {
        _magicBook.gameObject.SetActive(true);

        _magicBook.Refresh();

        RefreshPlayerControl();
    }

    public static void HideMagicBook()
    {
        _magicBook.gameObject.SetActive(false);

        RefreshPlayerControl();
    }

    public static bool IsMagicBookActive()
    {
        return _magicBook.gameObject.activeInHierarchy;
    }

    public static void IfActiveMagicBookRefresh()
    {
        if (IsMagicBookActive())
        {
            _magicBook.Refresh();
        }
    }

    public static void ShowDescription(Vector2 positionInPixel, string text, int widthIcon, int heightIcon, GUIObject boundObject)
    {
        _description.BoundObject = boundObject;

        _description.gameObject.SetActive(true);

        _description.Show(positionInPixel, text, widthIcon, heightIcon);
    }

    public static void HideDescription()
    {
        _description.BoundObject = null;

        _description.gameObject.SetActive(false);
    }

    public static void ShowPlayerInterface()
    {
        _hpBar.gameObject.SetActive(true);
        _mpBar.gameObject.SetActive(true);
        _hotkeys.SetActive(true);
    }

    public static void HidePlayerInterface()
    {
        _hpBar.gameObject.SetActive(false);
        _mpBar.gameObject.SetActive(false);
        _hotkeys.SetActive(false);
    }

    public static void ShowQuestManager()
    {
        _questManagerWindow.gameObject.SetActive(true);

        _questManagerWindow.Refresh();

        RefreshPlayerControl();
    }

    public static void HideQuestManager()
    {
        _questManagerWindow.gameObject.SetActive(false);

        RefreshPlayerControl();
    }

    public static bool IsQuestManagerActive()
    {
        return _questManagerWindow.gameObject.activeInHierarchy;
    }

    public static void IfActiveQuestManagerRefresh()
    {
        if (IsQuestManagerActive())
        {
            _questManagerWindow.Refresh();
        }
    }

    public static void ShowDialogWindow(Conversation conversation, Interlocutor interlocutor)
    {
        _dialogWindow.Interlocutor = interlocutor;
        _dialogWindow.Conversation = conversation;

        _dialogWindow.gameObject.SetActive(true);

        RefreshPlayerControl();
    }

    public static void HideDialogWindow()
    {
        _dialogWindow.gameObject.SetActive(false);

        RefreshPlayerControl();
    }

    public static bool IsDialogActive()
    {
        return _dialogWindow.gameObject.activeInHierarchy;
    }

    public static void IfActiveDialogRefresh()
    {
        if (IsDialogActive())
        {
            _dialogWindow.Refresh();
        }
    }

    public static void ShowQuestProposalWindow(Quest quest, int netIdQuester)
    {
        _questProposalWindow.Quest = quest;
        _questProposalWindow.NetIdQuester = netIdQuester;

        _questProposalWindow.gameObject.SetActive(true);

        RefreshPlayerControl();
    }

    public static void HideQuestProposalWindow()
    {
        _questProposalWindow.gameObject.SetActive(false);

        RefreshPlayerControl();
    }

    public static bool IsQuestProposalActive()
    {
        return _questProposalWindow.gameObject.activeInHierarchy;
    }

    static public int GetStackPosition()
    {
        _stackPosition++;

        return _stackPosition;
    }

    static public bool IsEquipmentActive()
    {
        return _equipmentWindow.gameObject.activeInHierarchy;
    }

    static public void ShowEquipment()
    {
        _equipmentWindow.Focus();

        _equipmentWindow.gameObject.SetActive(true);

        _equipmentWindow.RefreshEquipment();

        RefreshPlayerControl();
    }

    static public void HideEquipment()
    {
        _equipmentWindow.gameObject.SetActive(false);

        RefreshPlayerControl();

        if (_description.BoundObject == _equipmentWindow)
        {
            HideDescription();
        }
    }

    static public void IfActiveEquipmentRefresh()
    {
        if (IsEquipmentActive())
        {
            _equipmentWindow.RefreshEquipment();
        }
    }

    static public bool IsCharacterGUIActive()
    {
        return _characterWindow.gameObject.activeInHierarchy;
    }

    static public void ShowCharacterGUI()
    {
        _characterWindow.Focus();

        _characterWindow.gameObject.SetActive(true);

        _characterWindow.Refresh();

        RefreshPlayerControl();
    }

    static public void HideCharacterGUI()
    {
        _characterWindow.gameObject.SetActive(false);

        RefreshPlayerControl();

        if (_description.BoundObject == _characterWindow)
        {
            HideDescription();
        }
    }

    static public void IfActiveCharacterGUIRefresh()
    {
        if (IsCharacterGUIActive())
        {
            _characterWindow.Refresh();
        }
    }

    static public void ShowWindow(string title, string text)
    {
        MessageGUI message = new MessageGUI(title, text);
        ShowWindow(message);
    }

    private static void ShowWindow(string title, string text, Rect position)
    {
        MessageGUI message = new MessageGUI(title, text);
        ShowWindow(message, position);
    }

    static public void ShowWindow(MessageGUI message)
    {
        ShowWindow(message, new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 200));
    }

    private static void ShowWindow(MessageGUI message, Rect position)
    {
        WindowGUI window = new WindowGUI();
        window.IdWindow = _nextWindowId;
        _nextWindowId ++;
        window.Content = message.Content;
        window.Title = message.Title;
        window.WindowRect = position;        
        _windows.Add(window);
    }

    static public void ShowFreezingWindow(string title, string text)
    {
        MessageGUI message = new MessageGUI(title, text);
        ShowFreezingWindow(message);
    }

    static public void ShowFreezingWindow(string title, string text, Rect position)
    {
        MessageGUI message = new MessageGUI(title, text);
        ShowFreezingWindow(message, position);
    }

    static public void ShowFreezingWindow(MessageGUI message)
    {
        ShowFreezingWindow(message, new Rect(Screen.width/2 - 100, Screen.height/2 - 60, 200, 120));
    }

    static public void ShowFreezingWindow(MessageGUI message, Rect position)
    {
        if (_isFreezedScreen)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            FreezeScreen();
            WindowGUI window = new WindowGUI();
            window.IdWindow = 0;
            window.Content = message.Content;
            window.Title = message.Title;
            window.WindowRect = position;
            _freezingWindow = window;
        }
    }

    public static void SwitchConnectionMenu(ConnectionMenuInfo info)
    {
        _place = Place.ConnectionMenu;
        _infoAboutCurrentPlace = info;
    }

    static public void SwitchLoginMenuGUI(LoginMenuInfo info)
    {
        _place = Place.LoginMenu;
        _infoAboutCurrentPlace = info;
    }

    static public void SwitchRegisterMenuGUI(RegisterMenuInfo info)
    {
        _place = Place.RegisterMenu;
        _infoAboutCurrentPlace = info;
    }

    static public void SwitchChoiceCharacterMenuGUI(GoToChoiceCharacterMenuToClient request)
    {
        var info = new CharactersMenuInfo(request.Characters.ToArray());

        _place = Place.ChoiceCharacterMenu;
        _infoAboutCurrentPlace = info;
    }

    static public void SwitchWorldGUI(GoIntoWorldToClient info)
    {
        _place = Place.World;
        _infoAboutCurrentPlace = new WordGUIInfo();
    }

    public static void ShowMessage(string text)
    {
        DoWindowGUIContent content = delegate(Rect rect)
        {
            GUILayout.Box(text);

            if (GUILayout.Button("Ok"))
            {
                return true;
            }

            return false;
        };

        MessageGUI message = new MessageGUI("", content);

        ShowWindow(message);
    }

    public static void ShowDeadMessage()
    {
        DoWindowGUIContent content = delegate(Rect rect)
        {
            GUILayout.Box(Languages.GetMessageText((int)TextMessage.YouAreDead));

            if (GUILayout.Button(Languages.GetPhrase("resurrect")))
            {
                var request = new RespawnToServer();

                Client.SendRequestAsMessage(request);

                return true;
            }

            return false;
        };

        MessageGUI message = new MessageGUI("", content);

        ShowWindow(message);
    }

    public static void ShowChatMessage(string message)
    {
        var info = (WordGUIInfo)_infoAboutCurrentPlace;

        info.Chat.AddMessage(message);
    }

    static private void DrawChoiceCharacterMenuGUI()
    {
        var info = (CharactersMenuInfo)_infoAboutCurrentPlace;

        if (info.IsCreateNewCharacter)
        {
            GUILayout.BeginArea(new Rect(10, 10, 400, 200), GUI.skin.FindStyle("area"));

            var character = info.NewCharacter;

            GUILayout.BeginHorizontal();
            GUILayout.Label(Languages.GetPhrase("nameCharacter"), GUILayout.Width(80));
            character.Name = GUILayout.TextField(character.Name, 20);
            GUILayout.EndHorizontal();

            int breed = character.BreadAndGender.Breed;
            bool isMale = character.BreadAndGender.IsMale;

            /*
            GUILayout.BeginHorizontal();
            GUILayout.Label(Languages.GetPhrase("breed"), GUI.skin.FindStyle("selectionGridName"), GUILayout.Width(80));
            breed = GUILayout.SelectionGrid(breed, new string[] { Languages.GetPhrase("breedTroll") }, 1);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(Languages.GetPhrase("gender"), GUI.skin.FindStyle("selectionGridName"), GUILayout.Width(80));
            isMale = GUILayout.SelectionGrid(isMale ? 0 : 1, new string[] { Languages.GetPhrase("male"), Languages.GetPhrase("female") }, 2) == 0 ? true : false;
            GUILayout.EndHorizontal();

            character.BreadAndGender = new BreedAndGender(breed, isMale); 
            */

            if (GUILayout.Button(Languages.GetPhrase("createNewCharacter")))
            {
                var request = new CreateCharacterToServer(character.Name, character.BreadAndGender);

                Client.SendRequestAsMessage(request);
            }

            if (GUILayout.Button(Languages.GetPhrase("back")))
            {
                info.IsCreateNewCharacter = false;

                DrawChoiceCharacterMenuGUI_UpdatePlayerModel(info.GetSelectedCharacter());
            }

            GUILayout.EndArea();
        }
        else
        {
            GUILayout.BeginArea(new Rect(10, 10, 250, 200), GUI.skin.FindStyle("area"));

            info.ScrollPosition = GUILayout.BeginScrollView(info.ScrollPosition);

            for (int i = 0; i < info.Characters.Length; i++)
            {
                if (GUILayout.Button(info.Characters[i].Name))
                {
                    info.SelectedCharacter = i;

                    DrawChoiceCharacterMenuGUI_UpdatePlayerModel(info.GetSelectedCharacter());
                }
            }

            GUILayout.EndScrollView();

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(10, 220, 250, 120), GUI.skin.FindStyle("area"));

            if (GUILayout.Button(Languages.GetPhrase("chooseCharacter")))
            {
                var request = new GoIntoWorldToServer(info.SelectedCharacter);

                Client.SendRequestAsMessage(request);
            }

            if (GUILayout.Button(Languages.GetPhrase("createNewCharacter")))
            {
                info.IsCreateNewCharacter = true;

                DrawChoiceCharacterMenuGUI_UpdatePlayerModel(info.NewCharacter);
            }

            GUILayout.EndArea();
        }   
    }

    private static void DrawChoiceCharacterMenuGUI_UpdatePlayerModel(CharacterInChoiceMenu character)
    {
        var generatorModel = GameObject.FindObjectOfType<PlayerGeneratorModelInMenu>();

        generatorModel.SetBreedAndGender(character.BreadAndGender);
        generatorModel.DeleteAndCreateModel();
        generatorModel.UpdateEquipedItems(character.EquipedItems);
    }

    private static void DrawConnectionMenu()
    {
        /*var info = (ConnectionMenuInfo)_infoAboutCurrentPlace;

        if (Client.Status == ClientStatus.Disconnected)
        {
            if (info.ManualWritteIP)
            {
                DrawConnectionMenu_Manual(info);
            }
            else
            {
                DrawConnectionMenu_ServerList(info);
            }
        }*/

        GUILayout.BeginArea(new Rect(10, 10, 200, 60), GUI.skin.FindStyle("area"));

        switch (Client.Status)
        {
            case ClientStatus.Disconnected:
                if (ClientController.IsError())
                {
                    GUILayout.Label(Languages.GetMessageText((int)TextMessage.ConnectingIsFail));
                }
                else
                {
                    GUILayout.Label(Languages.GetPhrase("connecting"));
                }
                break;
            case ClientStatus.Connecting:
                GUILayout.Label(Languages.GetPhrase("connecting"));
                break;
            case ClientStatus.Connected:
                SwitchLoginMenuGUI(new LoginMenuInfo());
                break;
        }

        GUILayout.EndArea();
    }

    private static void DrawConnectionMenu_Manual(ConnectionMenuInfo info)
    {
        GUILayout.BeginArea(new Rect(10, 10, 250, 230), GUI.skin.FindStyle("area"));

        if (GUILayout.Button(Languages.GetPhrase("serverList")))
        {
            info.ManualWritteIP = false;
            info.Status = ConnectionStage.Disconnection;
        }

        GUILayout.BeginHorizontal();
        GUILayout.Label("IP :", GUILayout.Width(40));
        info.IP = GUILayout.TextField(info.IP);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Port :", GUILayout.Width(40));
        info.Port = GUILayout.TextField(info.Port);
        GUILayout.EndHorizontal();

        if (GUILayout.Button(Languages.GetPhrase("connect")))
        {
            Client.Start();
            Client.Connect(info.IP, int.Parse(info.Port));
            info.Status = ConnectionStage.ManualConnecting;
            info.EndConnectionTime = DateTime.UtcNow.AddSeconds(5);
        }

        if (info.Status == ConnectionStage.ManualConnecting)
        {
            if (DateTime.UtcNow > info.EndConnectionTime)
            {
                info.Status = ConnectionStage.Disconnection;
                Client.Close();

                GUIController.ShowMessage(Languages.GetMessageText((int)TextMessage.ConnectingIsFail));
            }

            GUILayout.Label(Languages.GetPhrase("connecting"));
        }

        GUILayout.EndArea();
    }

    private static void DrawConnectionMenu_ServerList(ConnectionMenuInfo info)
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 160), GUI.skin.FindStyle("area"));

        if (GUILayout.Button(Languages.GetPhrase("manualWritteIP")))
        {
            info.ManualWritteIP = true;
            info.Status = ConnectionStage.Disconnection;
        }
        if (GUILayout.Button(Languages.GetPhrase("refresh")))
        {
            GameObject.FindObjectOfType<ClientController>().StartCoroutine(Client.RefreshHostList(info));
        }
        if (info.Status == ConnectionStage.ExternalConnecting || info.Status == ConnectionStage.InternalConnecting)
        {
            GUILayout.Label(Languages.GetPhrase("connecting"));
        }

        GUILayout.EndArea();

        if (info.Status == ConnectionStage.Disconnection)
        {
            GUILayout.BeginArea(new Rect(220, 10, 300, 500), GUI.skin.FindStyle("area"));

            info.ScrollPosition = GUILayout.BeginScrollView(info.ScrollPosition);

            foreach (var host in info.HostList)
            {
                if (GUILayout.Button(host.Name))
                {
                    Client.Start();

                    Client.Connect(host.ExternalIP, host.Port);

                    info.EndConnectionTime = DateTime.UtcNow.AddSeconds(3);

                    info.Status = ConnectionStage.ExternalConnecting;

                    info.ChosenHost = host;
                }
            }

            GUILayout.EndScrollView();

            GUILayout.EndArea();
        } 

        if (info.Status == ConnectionStage.ExternalConnecting && DateTime.UtcNow > info.EndConnectionTime)
        {
            Client.Close();
            Client.Start();

            Client.Connect(info.ChosenHost.InternalIP, info.ChosenHost.Port);

            info.EndConnectionTime = DateTime.UtcNow.AddSeconds(3);

            info.Status = ConnectionStage.InternalConnecting;
        }

        if (info.Status == ConnectionStage.InternalConnecting && DateTime.UtcNow > info.EndConnectionTime)
        {
            info.Status = ConnectionStage.Disconnection;
            info.ChosenHost = null;
            Client.Close();

            GUIController.ShowMessage(Languages.GetMessageText((int)TextMessage.ConnectingIsFail));
        }
    }

    static private void DrawLoginMenuGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 190), GUI.skin.FindStyle("area"));

        LoginMenuInfo info = (LoginMenuInfo)_infoAboutCurrentPlace;

        GUILayout.BeginHorizontal();
        GUILayout.Label(Languages.GetPhrase("login"), GUILayout.Width(80));
        info.Login = GUILayout.TextField(info.Login);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(Languages.GetPhrase("password"), GUILayout.Width(80));
        info.Password = GUILayout.PasswordField(info.Password, '*');
        GUILayout.EndHorizontal();

            

        if(GUILayout.Button(Languages.GetPhrase("loginToGame")))
        {
            var request = new LoginToGame(info.Login, info.Password);

            Client.SendRequestAsMessage(request);
        }

        if (GUILayout.Button(Languages.GetPhrase("createAccount")))
        {
            SwitchRegisterMenuGUI(new RegisterMenuInfo());
        }

        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(6, 206, HelpImage.width, HelpImage.height));
        GUILayout.Label(HelpImage);
        GUILayout.EndArea();
    }

    private static void DrawRegisterMenuGUI()
    {
        var info = (RegisterMenuInfo)_infoAboutCurrentPlace;

        GUILayout.BeginArea(new Rect(10, 10, 300, 250), GUI.skin.FindStyle("area"));

        GUILayout.BeginHorizontal();
        GUILayout.Label(Languages.GetPhrase("login"), GUILayout.Width(80));
        info.Login = GUILayout.TextField(info.Login, 20);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(Languages.GetPhrase("password"), GUILayout.Width(80));
        info.Password = GUILayout.PasswordField(info.Password, '*', 20);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(Languages.GetPhrase("repeatPassword"), GUILayout.Width(80));
        info.RepeatPassword = GUILayout.PasswordField(info.RepeatPassword, '*', 20);
        GUILayout.EndHorizontal();

        if (GUILayout.Button(Languages.GetPhrase("register")))
        {
            if (info.Password == info.RepeatPassword)
            {
                var request = new RegisterToServer(info.Login, info.Password);

                Client.SendRequestAsMessage(request);
            }
            else
            {
                ShowMessage(Languages.GetMessageText((int)TextMessage.PasswordAndRepeatPasswordAreDifferent));
            }
        }

        if (GUILayout.Button(Languages.GetPhrase("back")))
        {
            SwitchLoginMenuGUI(new LoginMenuInfo());
        }

        GUILayout.EndArea();
    }

    static private void DrawFreezedScreen()
    {
        if (_freezedScreenTexture == null)
        {
            _freezedScreenTexture = Screenshot();
            _freezedScreenTexture.Apply();
        }
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _freezedScreenTexture);
        DrawFreezingWindow();
    }

    static private void DrawFreezingWindow()
    {
        _freezingWindow.WindowRect = GUI.Window(0, _freezingWindow.WindowRect, DoFreezingMessageWindow, _freezingWindow.Title);
    }

    static private void DoFreezingMessageWindow(int idWindow)
    {
        _freezingWindow.DrawContent();
        GUI.DragWindow();
    }

    static private void DefrostScreen()
    {
        _freezedScreenTexture = null;
        _freezingWindow = null;
    }

    static private void FreezeScreen()
    {
        _isFreezedScreen = true;
    }   

    static private Texture2D Screenshot()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        return tex;
    }

    static private void DrawWindows()
    {
        for(int i = 0; i < _windows.Count; i ++)
        {
            _windows[i].WindowRect = GUI.Window(i, _windows[i].WindowRect, DoMessageWindow, _windows[i].Title);
        }
    }

    static private void DoMessageWindow(int idWindow)
    {
        bool isCloseWindow = _windows[idWindow].DrawContent();
        GUI.DragWindow();

        if (isCloseWindow)
        {
            _windowsToClose.Add(_windows[idWindow]);
        }
    }

    static private void RemoveWindows()
    {
        foreach (WindowGUI windowToClose in _windowsToClose)
        {
            for (int i = 0; i < _windows.Count; i++)
            {
                if (_windows[i] == windowToClose)
                {
                    _windows.RemoveAt(i);

                    break;
                }
            }
        }
    }

    static private void DrawWorldGUI()
    {
        var info = (WordGUIInfo)_infoAboutCurrentPlace;

        DrawChat(info.Chat);
    }

    static private void DrawChat(Chat chat)
    {
        int leftBorder = 5;
        int botBorder = 5;
        int chatAndMessagesSpace = 5;
        int messagesSpace = 0;
        int width = 600;
        int height = 20;

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == InputsK.chat)
        {
            if (chat.IsWritte)
            {
                if (chat.NewMessage != "")
                {
                    var request = new ChatMessageToServer(chat.NewMessage);

                    Client.SendRequestAsMessage(request);

                    chat.NewMessage = "";
                }

                chat.IsWritte = false;
            }
            else
            {
                chat.IsWritte = true;
            }
        }

        if(chat.IsWritte)
        {
            GUI.SetNextControlName("Chat");
            chat.NewMessage = GUI.TextField(new Rect(leftBorder, Screen.height - height - botBorder, 200, 20), chat.NewMessage, Chat.MAX_CHAR_IN_MESSAGE);
            GUI.FocusControl("Chat");
        }

        string[] messages = chat.GetMessages();

        for (int i = 0; i < messages.Length; i++)
        {
            GUI.Label(new Rect(leftBorder, Screen.height - botBorder - chatAndMessagesSpace - (i + 2) * height - i * messagesSpace, width, height), messages[i]);
        }
    }

    private static void RefreshPlayerControl()
    {
        bool isAnyWindowOpened = IsCharacterGUIActive() || IsDialogActive() || IsEquipmentActive() ||
            IsQuestProposalActive() || IsQuestManagerActive() || IsMagicBookActive() || IsShopActive();

        var player = Client.GetNetOwnPlayer();

        player.GetComponent<InputPlayer>().SetLockRotationByGUI(isAnyWindowOpened);

        if (isAnyWindowOpened)
        {
            player.GetComponent<OwnPlayerCombat>().IsGUIAssent = false;
        }
        else
        {
            player.StartCoroutine(GiveCombatAssent(player.GetComponent<OwnPlayerCombat>()));
        }
    }

    private static IEnumerator GiveCombatAssent(OwnPlayerCombat player)
    {
        yield return new WaitForSeconds(0.1f);

        player.GetComponent<OwnPlayerCombat>().IsGUIAssent = true;
    }
}
