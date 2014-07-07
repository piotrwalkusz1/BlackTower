using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Connection;
using NetworkProject.Connection.ToServer;
using NetworkProject.Items;
using UnityEngine;

[System.CLSCompliant(false)]
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

    public static void SendRequestAsMessage(INetworkRequest request, IConnectionMember address)
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

    public static void SendErrorMessage(ErrorCode errorCode, IConnectionMember address)
    {
        var request = new NetworkProject.Connection.ToClient.ErrorMessage(errorCode);
        var message = new OutgoingMessage(request);

        Send(message, address);
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
        else if (request is GoIntoWorld) return ReceiveMessageGoIntoWorld;
        else if (request is PlayerMove) return ReceiveMessagePlayerMove;
        else if (request is PlayerJump) return ReceiveMessagePlayerJump;
        else if (request is PlayerRotation) return ReceiveMessagePlayerRotation;
        else if (request is PickItem) return ReceiveMessagePickItem;
        else if (request is Attack) return ReceiveMessageAttack;
        else if (request is Respawn) return ReceiveMessageRespawn;
        else if (request is ChangeItemsInEquipment) return ReceiveMessageChangeItemsInEquipment;
        else if (request is ChangeEquipedItem) return ReceiveMessageChangeEquipedItem;
        else if (request is ChangeEquipedItems) return ReceiveMessageChangeEquipedItems;
        else if (request is UseSpell) return ReceiveMessageUseSpell;
        else throw new Exception("Nie ma takiego typu żądania!");
    }

    #region Receive

    private static void ReceiveMessageLogin(IncomingMessage message) 
    {
        try
        {
            var loginData = (LoginToGame)message.Request;

            OnlineAccount account = AccountRepository.LoginAccount(loginData.Login, loginData.Password, message.Sender);

            var requestToClient = new NetworkProject.Connection.ToClient.GoToChoiceCharacterMenu();

            foreach (RegisterCharacter character in account.GetCharacters())
            {
                var characterData = new CharacterInChoiceMenu(character.Name);

                requestToClient.AddCharacter(characterData);
            }

            var messageToClient = new OutgoingMessage(requestToClient);

            Send(messageToClient, message.Sender);
        }
        catch (AccountRepositoryException exception)
        {
            switch (exception.ErrorCode)
            {
                case AccountRepositoryExceptionCode.WrongLoginOrPassword:
                    SendErrorMessage(ErrorCode.WrongLoginOrPassword, message.Sender);
                    break;
                case AccountRepositoryExceptionCode.CharacterAlreadyLogin:
                    SendErrorMessage(ErrorCode.AccountAlreadyLogin, message.Sender);
                    break;
                default:
                    MonoBehaviour.print("Inny błąd : " + exception.ErrorCode.ToString() + '\n' + exception.StackTrace);
                    break;
            }
        }
    }

    private static void ReceiveMessageGoIntoWorld(IncomingMessage message)
    {
        OnlineAccount account = AccountRepository.GetOnlineAccountByAddress(message.Sender);
        GoIntoWorld requestData = (GoIntoWorld)message.Request;

        if (account.IsLoggedCharacter())
        {
            Server.SendErrorMessage(ErrorCode.CharacterAlreadyLogin, message.Sender);
        }
        else
        {  
            OnlineCharacter player = AccountRepository.LoginCharacter(account, requestData.CharacterSlot);

            player.CreatePlayerInstantiate();

            int map = Standard.Settings.GetMap(player.Instantiate.transform.position);
            var goIntoWorldRequest = new NetworkProject.Connection.ToClient.GoIntoWorld(map);
            var goInfoWorldMessage = new OutgoingMessage(goIntoWorldRequest);

            var netPlayer = player.Instantiate.GetComponent<NetPlayer>();
            var tran = player.Instantiate.transform;
            var stats = player.Instantiate.GetComponent<PlayerStats>();
            var createOwnPlayerRequest = new NetworkProject.Connection.ToClient.CreateOwnPlayer(netPlayer.IdNet, tran.position, tran.eulerAngles.y, stats);
            var createOwnPlayerMessage = new OutgoingMessage(createOwnPlayerRequest);

            Send(goInfoWorldMessage, message.Sender);
            Send(createOwnPlayerMessage, message.Sender);
        }
    }

    private static void ReceiveMessagePlayerMove(IncomingMessage message)
    {
        var request = (PlayerMove)message.Request;
        var netPlayer = FindAliveNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerMovement>().SetNewPosition(request.NewPosition);
    }

    private static void ReceiveMessagePlayerJump(IncomingMessage message)
    {
        //narazie niepotrzebne
        //var request = (PlayerJump)message.Request;
        var netPlayer = FindAliveNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerMovement>().JumpAndSendMessage();
    }

    private static void ReceiveMessagePlayerRotation(IncomingMessage message)
    {
        var request = (PlayerRotation)message.Request;
        var netPlayer = FindAliveNetPlayerByAddress(message.Sender);

        netPlayer.GetComponent<PlayerMovement>().SetNewRotation(request.NewRotation);
    }

    private static void ReceiveMessagePickItem(IncomingMessage message)
    {
        var request = (PickItem)message.Request;

        var netItem = FindNetItemByIdObject(request.IdNetItem);
        var player = FindAliveNetPlayerByAddress(message.Sender);

        netItem.TryPickByPlayer(player);
    }

    private static void ReceiveMessageAttack(IncomingMessage message)
    {
        var attackData = (Attack)message.Request;

        var player = FindAliveNetPlayerByAddress(message.Sender);

        player.GetComponent<PlayerCombat>().Attack(attackData.Direction);
    }

    private static void ReceiveMessageRespawn(IncomingMessage message)
    {
        NetPlayer player = FindDeadNetPlayerByAddress(message.Sender);

        PlayerRespawn respawn = FindNearestPlayerRespawnOnMap(player.GetMap(), player.transform.position);

        respawn.Respawn(player);
    }

    private static void ReceiveMessageChangeItemsInEquipment(IncomingMessage message)
    {
        var request = (ChangeItemsInEquipment)message.Request;

        NetPlayer netPlayer = FindAliveNetPlayerByAddress(message.Sender);
        Equipment eq = netPlayer.GetComponent<Equipment>();

        eq.ChangeItemInEquipment(request.Slot1, request.Slot2);

        eq.SendUpdateSlot(request.Slot1, message.Sender);
        eq.SendUpdateSlot(request.Slot2, message.Sender);
    }

    private static void ReceiveMessageChangeEquipedItem(IncomingMessage message)
    {
        var request = (ChangeEquipedItem)message.Request;

        var player = FindAliveNetPlayerByAddress(message.Sender);
        var eq = player.GetComponent<PlayerEquipment>();

        eq.TryEquipeItemInEquipmentOnThisBodyPart(request.SlotInEquipment, request.EquipedItemSlot);

        Item equipedItem = eq.GetEquipedItem(request.EquipedItemSlot);
        Item itemInEquipment = eq.GetItemBySlot(request.SlotInEquipment);

        var updateEquipedItem = new NetworkProject.Connection.ToClient.UpdateEquipedItem(player.IdNet, request.EquipedItemSlot, equipedItem);
        var updateItemInEquipment = new NetworkProject.Connection.ToClient.UpdateItemInEquipment(player.IdNet, request.SlotInEquipment, itemInEquipment);

        SendRequestAsMessage(updateEquipedItem, message.Sender);
        SendRequestAsMessage(updateItemInEquipment, message.Sender);

        player.GetComponent<PlayerStats>().CalculateStatsAndSendUpdate();

        player.SendUpdateEquipedItem(request.EquipedItemSlot, equipedItem);
    }

    private static void ReceiveMessageChangeEquipedItems(IncomingMessage message)
    {
        var request = (ChangeEquipedItems)message.Request;

        var player = FindAliveNetPlayerByAddress(message.Sender);
        var eq = player.GetComponent<PlayerEquipment>();

        eq.TryChangeEquipedItems(request.Slot1, request.Slot2);

        Item item1 = eq.GetEquipedItem(request.Slot1);
        Item item2 = eq.GetEquipedItem(request.Slot2);

        var updateEquipedItem1 = new NetworkProject.Connection.ToClient.UpdateEquipedItem(player.IdNet, request.Slot1, item1);
        var updateEquipedItem2 = new NetworkProject.Connection.ToClient.UpdateEquipedItem(player.IdNet, request.Slot2, item2);

        SendRequestAsMessage(updateEquipedItem1, message.Sender);
        SendRequestAsMessage(updateEquipedItem2, message.Sender);

        player.GetComponent<PlayerStats>().CalculateStatsAndSendUpdate();

        player.SendUpdateEquipedItem(request.Slot1, item1);
        player.SendUpdateEquipedItem(request.Slot2, item2);
    }

    private static void ReceiveMessageUseSpell(IncomingMessage message)
    {
        var request = (UseSpell)message.Request;

        var player = FindAliveNetPlayerByAddress(message.Sender);

        bool success = player.GetComponent<SpellCaster>().TryCastSpellFromSpellBook(request.IdSpell);

        if (!success)
        {
            throw new System.Exception("Nie udało się rzucić zaklęcia!");
        }
    }

    #endregion

    private static NetItem FindNetItemByIdObject(int idObject)
    {
        NetItem[] items = GameObject.FindObjectsOfType(typeof(NetItem)) as NetItem[];
        foreach (NetItem item in items)
        {
            if (item.IdNet == idObject)
            {
                return item;
            }
        }

        return null;
    }

    private static NetPlayer FindNetPlayerByAddress(IConnectionMember address)
    {
        OnlineCharacter onlineCharacter = AccountRepository.GetOnlineCharacterByAddress(address);
        return onlineCharacter.Instantiate.GetComponent<NetPlayer>();
    }

    private static NetPlayer FindAliveNetPlayerByAddress(IConnectionMember address)
    {
        var player = FindNetPlayerByAddress(address);

        if (player.GetComponent<PlayerHealthSystem>().IsDead())
        {
            throw new Exception("Gracz jest martwy!");
        }

        return player;
    }

    private static NetPlayer FindDeadNetPlayerByAddress(IConnectionMember address)
    {
        var player = FindNetPlayerByAddress(address);

        if (!player.GetComponent<PlayerHealthSystem>().IsDead())
        {
            throw new Exception("Gracz jest żywy!");
        }

        return player;
    }

    private static PlayerRespawn FindNearestPlayerRespawnOnMap(int map, Vector3 position)
    {
        PlayerRespawn[] respawns = GameObject.FindObjectsOfType<PlayerRespawn>() as PlayerRespawn[];

        float smallestSqrDistance = Mathf.Infinity;
        PlayerRespawn nearestRespawn = null;

        Vector3 distance;
        float sqrDistance;

        foreach (PlayerRespawn respawn in respawns)
        {
            if (respawn.GetMap() != map)
            {
                continue;
            }

            distance = position - respawn.transform.position;

            sqrDistance = distance.sqrMagnitude;

            if (sqrDistance < smallestSqrDistance)
            {
                nearestRespawn = respawn;
                smallestSqrDistance = sqrDistance;
            }
        }

        return nearestRespawn;
    }
}
