using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;
using NetworkProject.Connection.ToClient;
using NetworkProject.Connection.ToServer;

enum Place
{
    LoginMenu,
    ChoiceCharacterMenu,
    World
}

enum DragType
{
    Equipment
}

public class GUIController : MonoBehaviour
{
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

    void Awake()
    {
        _windows = new List<WindowGUI>();
        _windowsToClose = new List<WindowGUI>();
        DontDestroyOnLoad(gameObject);
        LoginMenuInfo info = new LoginMenuInfo();
        info.Login = "Login";
        info.Password = "Password";
        SwitchLoginMenuGUI(info);
    }

    void Start()
    {
        GameObject window = Instantiate(Prefabs.GUIEquipment, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _equipmentWindow = window.GetComponent<EquipmentWindow>();
        _equipmentWindow._closeButtonRect = new Rect(245, 5, 22, 22);
        _equipmentWindow._externalBorder = new Vector2(6, 32);
        _equipmentWindow._internalBorder = 4;
        _equipmentWindow._sizeItemImage = 40;
        _equipmentWindow._texture = ImageRepository.EquipmentWindow;
        _equipmentWindow.gameObject.SetActive(false);

        _equipmentWindow.Initialize();

        DontDestroyOnLoad(window);

        window = Instantiate(Prefabs.GUICharacter, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _characterWindow = window.GetComponent<CharacterWindow>();
        _characterWindow.Texture = ImageRepository.CharacterWindow;
        _characterWindow.gameObject.SetActive(false);

        DontDestroyOnLoad(window);
    }

    void OnGUI()
    {
        RemoveWindows();

        if (_isFreezedScreen)
        {
            DrawFreezedScreen();
        }
        else
        {
            switch (_place)
            {
                case Place.LoginMenu:
                    DrawLoginMenuGUI();
                    break;
                case Place.ChoiceCharacterMenu:
                    DrawChoiceCharacterMenuGUI();
                    break;
                case Place.World:
                    DrawWorldGUI();
                    break;    
            }

            DrawWindows();
        }       
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
        NetOwnPlayer player = FindObjectOfType<NetOwnPlayer>();
        _equipmentWindow.SetEquipment(player.GetComponent<OwnPlayerEquipment>());

        _equipmentWindow.Focus();

        _equipmentWindow.gameObject.SetActive(true);

        _equipmentWindow.RefreshEquipment();
    }

    static public void HideEquipment()
    {
        _equipmentWindow.gameObject.SetActive(false);
    }

    static public void IsActiveEquipmentRefresh()
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
        NetOwnPlayer player = FindObjectOfType<NetOwnPlayer>();
        _characterWindow.SetStats(player.GetComponent<OwnPlayerStats>());

        _characterWindow.Focus();

        _characterWindow.gameObject.SetActive(true);

        _characterWindow.Refresh();
    }

    static public void HideCharacterGUI()
    {
        _characterWindow.gameObject.SetActive(false);
    }

    static public void IsActiveCharacterGUIRefresh()
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
        ShowWindow(message, new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 120));
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

    static public void SwitchLoginMenuGUI(LoginMenuInfo info)
    {
        _place = Place.LoginMenu;
        _infoAboutCurrentPlace = info;
    }

    static public void SwitchChoiceCharacterMenuGUI(GoToChoiceCharacterMenuToClient info)
    {
        _place = Place.ChoiceCharacterMenu;
        _infoAboutCurrentPlace = info;
    }

    static public void SwitchWorldGUI(GoIntoWorldToClient info)
    {
        _place = Place.World;
        _infoAboutCurrentPlace = info;
    }

    public static void ShowDeadMessage()
    {
        DoWindowGUIContent content = delegate(Rect rect)
        {
            GUI.TextArea(new Rect(5, 10, rect.width - 10, rect.height - 45), "Nie żyjesz!");

            if (GUI.Button(new Rect(5, rect.height - 30, rect.width - 10, 25), "Wskrześ"))
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

    static private void DrawChoiceCharacterMenuGUI()
    {
        var info = (GoToChoiceCharacterMenuToClient)_infoAboutCurrentPlace;

        if (info != null && info.Characters[0] != null)
        {
            if (GUI.Button(new Rect(10, 10, 100, 20), info.Characters[0].Name))
            {
                var request = new GoIntoWorldToServer(0);

                Client.SendRequestAsMessage(request);
            }
        }
    }

    static private void DrawLoginMenuGUI()
    {
        LoginMenuInfo info = (LoginMenuInfo)_infoAboutCurrentPlace;

        info.Login = GUI.TextField(new Rect(10, 10, 150, 20), info.Login);
        info.Password = GUI.TextField(new Rect(10, 40, 150, 20), info.Password);

        if(GUI.Button(new Rect(10, 70, 150, 20), "Login"))
        {
            var request = new LoginToGame(info.Login, info.Password);

            Client.SendRequestAsMessage(request);
        }
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

    }
}
