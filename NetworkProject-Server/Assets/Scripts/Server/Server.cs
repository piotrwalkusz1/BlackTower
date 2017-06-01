using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Connection;
using NetworkProject.Connection.ToServer;
using NetworkProject.Connection.ToClient;
using NetworkProject.Items;
using UnityEngine;

public static class Server
{
    public static ServerStatus Status
    {
        get
        {
            if (_server == null)
            {
                return ServerStatus.Disconnected;
            }
            return _server.Status;
        }
    }

    private static IServer _server;
    private static string _port = "2500";
    private static string _messageText = "";

    static Server()
    {
        _server = Standard.IoC.GetServer();
    }

    public static void Set(IServer server)
    {
        _server = server;
    }

    public static void Start(ServerConfig config)
    {
        _server.Start(config);
    }

    public static void Close()
    {
        _server.Close();
    }

    public static void Listen()
    {
        try
        {
            IncomingMessage m;
            while ((m = _server.ReadMessage()) != null)
            {
                ExecuteMessage(m);
            }
        }
        catch (Exception ex)
        {
            MonoBehaviour.print(ex.Message + '\n' + ex.TargetSite + '\n' + ex.StackTrace);
        }
    }

    public static void OnGUI()
    {
        switch (Status)
        {
            case ServerStatus.Disconnected:

                /*GUILayout.BeginHorizontal();
                GUILayout.Label("Server name :");
                _nameServer = GUILayout.TextField(_nameServer, GUILayout.Width(150));
                GUILayout.EndHorizontal();*/

                GUILayout.BeginHorizontal();
                GUILayout.Label("Port :");
                _port = GUILayout.TextField(_port, GUILayout.Width(150));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Start server"))
                {
                    try
                    {
                        var config = new ServerConfig(int.Parse(_port));

                        Start(config);

                        ApplicationController.LoadAllLevelsAdditive();

                        /*GameObject.FindObjectOfType<ServerController>().StartCoroutine(RegisterServer(_nameServer, _port));*/

                        _messageText = "";
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        _messageText = "Port " + _port + " is already taken. Please choose another port.";
                    }
                }
                GUILayout.Label(_messageText);
                break;
            case ServerStatus.Connecting:
                GUILayout.Label("Connecting");
                break;
            case ServerStatus.Connected:
                AccountRepository.OnGUI();
                if (GUILayout.Button("Disconnect"))
                {
                    Server.Close();
                    ApplicationController.LoadStartLvl();
                }
                break;
            case ServerStatus.Disconnecting:
                GUILayout.Label("Disconnecting");
                break;
        }
    }

    public static void SendRequestAsMessage(INetworkRequestToClient request, IConnectionMember address)
    {
        var message = new OutgoingMessage(request);

        Send(message, address);
    }

    public static void Send(OutgoingMessage message, IConnectionMember[] addresses)
    {
        foreach (IConnectionMember address in addresses)
        {
            Send(message, address);
        }
    }

    public static void Send(OutgoingMessage message, IConnectionMember address)
    {
        _server.Send(message, address);
    }

    public static void SendTextMessage(TextMessage textMessage, IConnectionMember address)
    {
        var request = new TextMessageToClient((int)textMessage);

        SendRequestAsMessage(request, address);
    }

    public static void ExecuteMessage(IncomingMessage message)
    {
        Action<IncomingMessage> method = ChooseMethodReceiveMessage(message.Request);
        IConnectionMember sender = message.Sender;
        method(message);
    }

    private static Action<IncomingMessage> ChooseMethodReceiveMessage(INetworkRequest request)
    {
        if (request is LoginToGame) return ReceiveMessageLogin;
        else if (request is RegisterToServer) return ReveiveMessageRegister;
        else if (request is CreateCharacterToServer) return ReveiveMessageCreateCharacter;
        else if (request is GoIntoWorldToServer) return ReceiveMessageGoIntoWorld;
        else if (request is ChatMessageToServer) return ReceiveMessageChatMessage;
        else if (request is PlayerMoveToServer) return ReceiveMessagePlayerMove;
        else if (request is PlayerJumpToServer) return ReceiveMessagePlayerJump;
        else if (request is PlayerRotationToServer) return ReceiveMessagePlayerRotation;
        else if (request is MoveOtherObjectToServer) return ReceiveMessageMoveOtherObjectToServer;
        else if (request is PickItemToServer) return ReceiveMessagePickItem;
        else if (request is AttackToServer) return ReceiveMessageAttack;
        else if (request is RespawnToServer) return ReceiveMessageRespawn;
        else if (request is ChangeItemsInEquipmentToServer) return ReceiveMessageChangeItemsInEquipment;
        else if (request is ChangeEquipedItemToServer) return ReceiveMessageChangeEquipedItem;
        else if (request is ChangeEquipedItemsToServer) return ReceiveMessageChangeEquipedItems;
        else if (request is UseSpellToServer) return ReceiveMessageUseSpell;
        else if (request is TakeQuestToServer) return ReceiveMessageTakeQuest;
        else if (request is AskQuestProposalToServer) return ReceiveMessageAskQuestProposal;
        else if (request is ReturnQuestToServer) return ReveiveMessageReturnQuest;
        else if (request is ResponseMoveMyCharacterToServer) return ReceiveMessageResponseMoveMyCharacter;
        else if (request is ExecuteActionToServer) return ReceiveMessageExecuteAction;
        else if (request is UseItemToServer) return ReceiveMessageUseItem;
        else if (request is OpenShopToServer) return ReceiveMessageOpenShop;
        else if (request is BuyItemInShopToServer) return ReceiveMessageBuyItemInShop;
        else if (request is UpdateHotkeysToServer) return ReceiveMessageUpdateHotkeys;
        else if (request is UseTalismanToServer) return ReceiveMessageUseTalisman;
        else if (request is LogoutToServer) return ReceiveMessageLogout;
        else throw new Exception("Nie ma takiego typu żądania!");
    }

    #region Receive

    private static void ReveiveMessageRegister(IncomingMessage message)
    {
        var request = (RegisterToServer)message.Request;

        try
        {
            AccountRepository.RegisterAccount(request.Login, request.Password);

            SendTextMessage(TextMessage.RegisterIsSuccess, message.Sender);
        }
        catch(AccountRepositoryException ex)
        {
            switch (ex.ErrorCode)
            {
                case AccountRepositoryExceptionCode.LoginIsTooLong:
                    SendTextMessage(TextMessage.LoginIsTooLong, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.LoginIsTooShort:
                    SendTextMessage(TextMessage.LoginIsTooShort, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.LoginContainsNotAllowedCharacters:
                    SendTextMessage(TextMessage.LoginContainsNotAllowedCharacters, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.PasswordIsTooLong:
                    SendTextMessage(TextMessage.PasswordIsTooLong, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.PasswordIsTooShort:
                    SendTextMessage(TextMessage.PasswordIsTooShort, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.PasswordContainsNotAllowedCharacters:
                    SendTextMessage(TextMessage.PasswordContainsNotAllowedCharacters, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.LoginAlreadyExist:
                    SendTextMessage(TextMessage.LoginAlreadyExist, message.Sender);
                    break;
                default :
                    SendTextMessage(TextMessage.RegisterIsFail, message.Sender);
                    MonoBehaviour.print("Nie ma takiego błędu rejestracji : " + ex.ErrorCode.ToString() + '\n' + ex.StackTrace);
                    break;
            }
        }
    }

    private static void ReveiveMessageCreateCharacter(IncomingMessage message)
    {
        var request = (CreateCharacterToServer)message.Request;

        try
        {
            var account = AccountRepository.GetOnlineAccountByAddress(message.Sender);

            AccountRepository.RegisterCharacter(account.AccountData, request.Name, request.BreedAndGender);

            var requestToClient = new GoToChoiceCharacterMenuToClient();

            foreach (RegisterCharacter character in account.GetCharacters())
            {
                var characterData = new CharacterInChoiceMenu(character.Name, character.BreedAndGender, character.EquipedItems);

                requestToClient.AddCharacter(characterData);
            }

            SendRequestAsMessage(requestToClient, message.Sender);
        }
        catch (AccountRepositoryException ex)
        {
            switch (ex.ErrorCode)
            {
                case AccountRepositoryExceptionCode.CharacterNameIsTooShort:
                    SendTextMessage(TextMessage.CharacterNameIsTooShort, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.CharacterNameIsTooLong:
                    SendTextMessage(TextMessage.CharacterNameIsTooLong, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.CharacterNameContainsNotAllowedCharacters:
                    SendTextMessage(TextMessage.CharacterNameContainsNotAllowedCharacters, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.CharacterNameAlreadyExist:
                    SendTextMessage(TextMessage.CharacterNameAlreadyExist, message.Sender);
                    break;
                default:
                    SendTextMessage(TextMessage.CreateCharacterIsFail, message.Sender);
                    MonoBehaviour.print("Nie ma takiego błędu rejestracji : " + ex.ErrorCode.ToString() + '\n' + ex.StackTrace);
                    break;
            }
        }
    }

    private static void ReceiveMessageLogin(IncomingMessage message) 
    {
        var loginData = (LoginToGame)message.Request;

        try
        {        
            OnlineAccount account = AccountRepository.LoginAccount(loginData.Login, loginData.Password, message.Sender);

            var requestToClient = new GoToChoiceCharacterMenuToClient();

            foreach (RegisterCharacter character in account.GetCharacters())
            {
                var characterData = new CharacterInChoiceMenu(character.Name, character.BreedAndGender, character.EquipedItems);

                requestToClient.AddCharacter(characterData);
            }

            SendRequestAsMessage(requestToClient, message.Sender);
        }
        catch (AccountRepositoryException exception)
        {
            switch (exception.ErrorCode)
            {
                case AccountRepositoryExceptionCode.WrongLoginOrPassword:
                    SendTextMessage(TextMessage.WrongLoginOrPassword, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.AccountAlreadyLogin:
                    AccountRepository.LogoutAccount(loginData.Login);
                    SendTextMessage(TextMessage.AccountAlreadyLogin, message.Sender);
                    break;
                default:
                    SendTextMessage(TextMessage.LoginIsFail, message.Sender);
                    MonoBehaviour.print("Inny błąd : " + exception.ErrorCode.ToString() + '\n' + exception.StackTrace);
                    break;
            }
        }
    }

    private static void ReceiveMessageGoIntoWorld(IncomingMessage message)
    {
        OnlineAccount account = AccountRepository.GetOnlineAccountByAddress(message.Sender);
        var requestData = (GoIntoWorldToServer)message.Request;

        try
        {  
            OnlineCharacter player = AccountRepository.LoginCharacter(account, requestData.CharacterSlot);

            player.CreatePlayerInstantiate();

            int map = Standard.Settings.GetMap(player.Instantiate.transform.position);
            var goIntoWorldRequest = new GoIntoWorldToClient(map);           

            SendRequestAsMessage(goIntoWorldRequest, message.Sender);

            player.Instantiate.GetComponent<NetPlayer>().SendCreateMessageToOwner();

            player.Instantiate.GetComponent<QuestExecutor>().SendQuestsInitialize();

            try
            {
                var updateHotkeys = new UpdateHotkeysToClient(player.CharacterData.Hotkeys.ToArray());

                SendRequestAsMessage(updateHotkeys, message.Sender);
            }
            catch
            {
                MonoBehaviour.print("Nie udało się wysłać hotkeys do gracza");
            }           
        }
        catch (AccountRepositoryException exception)
        {
            if (exception.ErrorCode == AccountRepositoryExceptionCode.CharacterAlreadyLogin)
            {
                MonoBehaviour.print("Postać jest już zalogowana.");
            }
            else
            {
                MonoBehaviour.print("Nieznany błąd z AccountRepository, errorCode : " +
                    exception.ErrorCode.ToString());
            }
        }
    }

    private static void ReceiveMessageChatMessage(IncomingMessage message)
    {
        var request = (ChatMessageToServer)message.Request;

        var sender = AccountRepository.FindNetPlayerByAddress(message.Sender);

        foreach (var netPlayer in GameObject.FindObjectsOfType<NetPlayer>())
        {
            netPlayer.SendChatMessageToOwner(sender.Name, request.Message);
        }
    }

    private static void ReceiveMessagePlayerMove(IncomingMessage message)
    {
        var request = (PlayerMoveToServer)message.Request;
        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerMovement>().SetNewPosition(request.NewPosition);
    }

    private static void ReceiveMessagePlayerJump(IncomingMessage message)
    {
        //narazie niepotrzebne
        //var request = (PlayerJump)message.Request;
        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerMovement>().JumpAndSendMessage();
    }

    private static void ReceiveMessagePlayerRotation(IncomingMessage message)
    {
        var request = (PlayerRotationToServer)message.Request;
        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerMovement>().SetNewRotation(request.NewRotation);
    }

    private static void ReceiveMessageMoveOtherObjectToServer(IncomingMessage message)
    {
        var request = (MoveOtherObjectToServer)message.Request;

        NetObject netObject = GameObjectRepository.FindNetObjectByIdNet(request.NetId);

        NetControlled netControlled = netObject.GetComponent<NetControlled>();

        if (!netControlled.ControllerAddress.Equals(message.Sender))
        {
            MonoBehaviour.print("Adres się nie zgadza.");
            return;
        }

        Movement movement = netObject.GetComponent<Movement>();

        movement.SetNewPosition(request.Position);
    }

    private static void ReceiveMessagePickItem(IncomingMessage message)
    {
        var request = (PickItemToServer)message.Request;

        var netItem = GameObjectRepository.FindNetItemByIdNet(request.IdNetItem);
        var player = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        netItem.TryPickByPlayer(player);
    }

    private static void ReceiveMessageAttack(IncomingMessage message)
    {
        var attackData = (AttackToServer)message.Request;

        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerCombat>().Attack(attackData);
    }

    private static void ReceiveMessageRespawn(IncomingMessage message)
    {
        NetPlayer player = AccountRepository.FindDeadNetPlayerByAddress(message.Sender);

        PlayerRespawn respawn = GameObjectRepository.FindNearestPlayerRespawnOnMap(player.GetMap(), player.transform.position);

        respawn.Respawn(player);
    }

    private static void ReceiveMessageChangeItemsInEquipment(IncomingMessage message)
    {
        var request = (ChangeItemsInEquipmentToServer)message.Request;

        NetPlayer netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);
        var eq = netPlayer.GetComponent<PlayerEquipment>();

        eq.ChangeItemsInBag(request.Slot1, request.Slot2);

        eq.SendUpdateBagSlot(request.Slot1, message.Sender);
        eq.SendUpdateBagSlot(request.Slot2, message.Sender);
    }

    private static void ReceiveMessageChangeEquipedItem(IncomingMessage message)
    {
        var request = (ChangeEquipedItemToServer)message.Request;

        var player = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);
        var eq = player.GetComponent<PlayerEquipment>();

        if (!eq.TryEquipItemFromBag(request.SlotInEquipment, request.EquipedItemSlot))
        {
            MonoBehaviour.print("Nie udało się założyć.");

            return;
        }   

        Item equipedItem = eq.GetEquipedItem(request.EquipedItemSlot);
        Item itemInBag = eq.GetItemFromBag(request.SlotInEquipment);

        var updateEquipedItem = new UpdateEquipedItemToClient(player.IdNet, request.EquipedItemSlot,
            PackageConverter.ItemToPackage(equipedItem));
        var updateItemInEquipment = new UpdateItemInEquipmentToClient(player.IdNet, request.SlotInEquipment,
            PackageConverter.ItemToPackage(itemInBag));

        SendRequestAsMessage(updateEquipedItem, message.Sender);
        SendRequestAsMessage(updateItemInEquipment, message.Sender);

        player.GetComponent<PlayerStats>().CalculateStatsAndSendUpdate();

        player.SendUpdateEquipedItem(request.EquipedItemSlot, equipedItem);
    }

    private static void ReceiveMessageChangeEquipedItems(IncomingMessage message)
    {
        var request = (ChangeEquipedItemsToServer)message.Request;

        var player = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);
        var eq = player.GetComponent<PlayerEquipment>();

        eq.TryChangeEquipedItems(request.Slot1, request.Slot2);

        Item item1 = eq.GetEquipedItem(request.Slot1);
        Item item2 = eq.GetEquipedItem(request.Slot2);

        var updateEquipedItem1 = new UpdateEquipedItemToClient(player.IdNet, request.Slot1,
            PackageConverter.ItemToPackage(item1));
        var updateEquipedItem2 = new UpdateEquipedItemToClient(player.IdNet, request.Slot2,
            PackageConverter.ItemToPackage(item2));

        SendRequestAsMessage(updateEquipedItem1, message.Sender);
        SendRequestAsMessage(updateEquipedItem2, message.Sender);

        player.GetComponent<PlayerStats>().CalculateStatsAndSendUpdate();

        player.SendUpdateEquipedItem(request.Slot1, item1);
        player.SendUpdateEquipedItem(request.Slot2, item2);
    }

    private static void ReceiveMessageUseSpell(IncomingMessage message)
    {
        var request = (UseSpellToServer)message.Request;

        var player = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        var spellCaster = player.GetComponent<SpellCaster>();

        bool success = spellCaster.TryCastSpellFromSpellBook(request.IdSpell, request.Options);

        if (success)
        {
            spellCaster.SendUpdateMana();
        }
        else
        {
            throw new System.Exception("Nie udało się rzucić zaklęcia!");
        }
    }

    private static void ReceiveMessageTakeQuest(IncomingMessage message)
    {
        var request = (TakeQuestToServer)message.Request;

        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);
        var questExecutor = netPlayer.GetComponent<QuestExecutor>();

        var quester = GameObjectRepository.FindNetObjectByIdNet(request.IdQuester).GetComponent<Quester>();

        var offset = netPlayer.transform.position - quester.transform.position;

        if(!quester.CanGiveQuest(questExecutor, request.IdQuest))
        {
            throw new InvalidOperationException("Nie można dać questa.");
        }

        quester.GiveQuestAndSendMessage(request.IdQuest, questExecutor, message.Sender);
    }

    private static void ReceiveMessageAskQuestProposal(IncomingMessage message)
    {
        var request = (AskQuestProposalToServer)message.Request;

        var quest = QuestRepository.GetQuest(request.IdQuest);

        var response = new GiveQuestProposalToClient(PackageConverter.QuestDataToPackage(quest), request.NetIdQuster);

        SendRequestAsMessage(response, message.Sender);
    }

    private static void ReveiveMessageReturnQuest(IncomingMessage message)
    {
        var request = (ReturnQuestToServer)message.Request;

        var questExecutor = AccountRepository.FindAliveNetPlayerByAddress(message.Sender).GetComponent<QuestExecutor>();

        var quester = GameObjectRepository.FindNetObjectByIdNet(request.IdNetQuester).GetComponent<Quester>();

        quester.TryGiveRewardForQuest(questExecutor, request.IdQuest);
    }

    private static void ReceiveMessageResponseMoveMyCharacter(IncomingMessage message)
    {
        var netPlayer = AccountRepository.FindNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerMovement>().ResponseUpdatePosition();
    }

    private static void ReceiveMessageExecuteAction(IncomingMessage message)
    {
        var request = (ExecuteActionToServer)message.Request;

        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        var netObject = GameObjectRepository.FindNetObjectByIdNet(request.NetId);
        var actionExecutor = netObject.GetComponent<ActionExecutor>();

        actionExecutor.ExecuteAction(netPlayer);
    }

    private static void ReceiveMessageUseItem(IncomingMessage message)
    {
        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        var request = (UseItemToServer)message.Request;

        netPlayer.GetComponent<PlayerEquipment>().UseItem(request.Slot);
    }

    private static void ReceiveMessageOpenShop(IncomingMessage message)
    {
        var request = (OpenShopToServer)message.Request;

        var merchant = GameObject.FindObjectsOfType<Merchant>().First(x => x.GetComponent<NetNPC>().IdNet == request.NetIdMerchant);

        merchant.OpenShopInClient(message.Sender);
    }

    private static void ReceiveMessageBuyItemInShop(IncomingMessage message)
    {
        var netPlayer = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        var request = (BuyItemInShopToServer)message.Request;

        var merchant = GameObject.FindObjectsOfType<Merchant>().First(x => x.GetComponent<NetNPC>().IdNet == request.IdNetNPC);

        merchant.SellItem(request.Slot, netPlayer);
    }

    private static void ReceiveMessageUpdateHotkeys(IncomingMessage message)
    {
        var request = (UpdateHotkeysToServer)message.Request;

        var character = AccountRepository.GetOnlineCharacterByAddress(message.Sender);

        character.CharacterData.Hotkeys = request.Hotkeys;
    }

    private static void ReceiveMessageUseTalisman(IncomingMessage message)
    {
        var request = (UseTalismanToServer)message.Request;

        var player = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);

        var spellCaster = player.GetComponent<SpellCaster>();

        var item = player.GetComponent<PlayerEquipment>().GetItemFromBag(request.Slot);

        if (item == null)
        {
            MonoBehaviour.print("Nie ma żadnego itemu w tym slocie");
            return;
        }

        if (!(item is ItemTalisman))
        {
            MonoBehaviour.print("Item w tym slocie nie jest talizmanem");
            return;     
        }

        var talisman = (ItemTalisman)item;

        if (spellCaster.IsSpell(talisman.SkillId))
        {
            MonoBehaviour.print("Gracz posiada już to zaklęcie");
            return;    
        }

        var newSpell = new Spell(talisman.SkillId, 1);

        spellCaster.AddSpell(newSpell);

        var updateSpell = new UpdateSpellToClient(player.IdNet, PackageConverter.SpellToPackage(newSpell));

        Server.SendRequestAsMessage(updateSpell, message.Sender);

        player.GetComponent<PlayerEquipment>().SetItemInBag(null, request.Slot);

        var updateSlot = new UpdateItemInEquipmentToClient(player.IdNet, request.Slot, null);

        Server.SendRequestAsMessage(updateSlot, message.Sender);
    }

    private static void ReceiveMessageLogout(IncomingMessage message)
    {
        var account = AccountRepository.GetOnlineAccountByAddress(message.Sender);

        AccountRepository.LogoutAccount(account);
    }
        
    #endregion

    private static System.Collections.IEnumerator RegisterServer(string nameServer, string port)
    {
        Network.InitializeServer(0, 0, false);

        WWW www = new WWW("http://checkip.dyndns.org/");

        yield return www;

        string[] a = www.text.Split(':');
        string a2 = a[1].Substring(1);
        string[] a3 = a2.Split('<');
        string externalIP = a3[0];

        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress curAdd = heserver.AddressList[0];
        string internalIP = curAdd.ToString();

        MasterServer.RegisterHost(Settings.gameName, nameServer, externalIP + ':' + internalIP + ':' + port);
    }
}
