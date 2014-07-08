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
        var request = new NetworkProject.Connection.ToClient.ErrorMessageToClient(errorCode);
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
        else if (request is GoIntoWorldToServer) return ReceiveMessageGoIntoWorld;
        else if (request is PlayerMoveToServer) return ReceiveMessagePlayerMove;
        else if (request is PlayerJumpToServer) return ReceiveMessagePlayerJump;
        else if (request is PlayerRotationToServer) return ReceiveMessagePlayerRotation;
        else if (request is PickItemToServer) return ReceiveMessagePickItem;
        else if (request is AttackToServer) return ReceiveMessageAttack;
        else if (request is RespawnToServer) return ReceiveMessageRespawn;
        else if (request is ChangeItemsInEquipmentToServer) return ReceiveMessageChangeItemsInEquipment;
        else if (request is ChangeEquipedItemToServer) return ReceiveMessageChangeEquipedItem;
        else if (request is ChangeEquipedItemsToServer) return ReceiveMessageChangeEquipedItems;
        else if (request is UseSpellToServer) return ReceiveMessageUseSpell;
        else throw new Exception("Nie ma takiego typu żądania!");
    }

    #region Receive

    private static void ReceiveMessageLogin(IncomingMessage message) 
    {
        try
        {
            var loginData = (LoginToGame)message.Request;

            OnlineAccount account = AccountRepository.LoginAccount(loginData.Login, loginData.Password, message.Sender);

            var requestToClient = new NetworkProject.Connection.ToClient.GoToChoiceCharacterMenuToClient();

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
        var requestData = (GoIntoWorldToServer)message.Request;

        if (account.IsLoggedCharacter())
        {
            Server.SendErrorMessage(ErrorCode.CharacterAlreadyLogin, message.Sender);
        }
        else
        {  
            OnlineCharacter player = AccountRepository.LoginCharacter(account, requestData.CharacterSlot);

            player.CreatePlayerInstantiate();

            int map = Standard.Settings.GetMap(player.Instantiate.transform.position);
            var goIntoWorldRequest = new NetworkProject.Connection.ToClient.GoIntoWorldToClient(map);
            var goInfoWorldMessage = new OutgoingMessage(goIntoWorldRequest);

            var netPlayer = player.Instantiate.GetComponent<NetPlayer>();
            var tran = player.Instantiate.transform;
            var stats = player.Instantiate.GetComponent<PlayerStats>();
            var createOwnPlayerRequest = new CreateOwnPlayerToClient(netPlayer.IdNet, tran.position, tran.eulerAngles.y, stats, netPlayer.Name);
            var createOwnPlayerMessage = new OutgoingMessage(createOwnPlayerRequest);

            Send(goInfoWorldMessage, message.Sender);
            Send(createOwnPlayerMessage, message.Sender);
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
        Equipment eq = netPlayer.GetComponent<Equipment>();

        eq.ChangeItemInEquipment(request.Slot1, request.Slot2);

        eq.SendUpdateSlot(request.Slot1, message.Sender);
        eq.SendUpdateSlot(request.Slot2, message.Sender);
    }

    private static void ReceiveMessageChangeEquipedItem(IncomingMessage message)
    {
        var request = (ChangeEquipedItemToServer)message.Request;

        var player = AccountRepository.FindAliveNetPlayerByAddress(message.Sender);
        var eq = player.GetComponent<PlayerEquipment>();

        eq.TryEquipeItemInEquipmentOnThisBodyPart(request.SlotInEquipment, request.EquipedItemSlot);

        Item equipedItem = eq.GetEquipedItem(request.EquipedItemSlot);
        Item itemInEquipment = eq.GetItemBySlot(request.SlotInEquipment);

        var updateEquipedItem = new NetworkProject.Connection.ToClient.UpdateEquipedItemToClient(player.IdNet, request.EquipedItemSlot, equipedItem);
        var updateItemInEquipment = new NetworkProject.Connection.ToClient.UpdateItemInEquipmentToClient(player.IdNet, request.SlotInEquipment, itemInEquipment);

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

        var updateEquipedItem1 = new NetworkProject.Connection.ToClient.UpdateEquipedItemToClient(player.IdNet, request.Slot1, item1);
        var updateEquipedItem2 = new NetworkProject.Connection.ToClient.UpdateEquipedItemToClient(player.IdNet, request.Slot2, item2);

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

        bool success = player.GetComponent<SpellCaster>().TryCastSpellFromSpellBook(request.IdSpell);

        if (!success)
        {
            throw new System.Exception("Nie udało się rzucić zaklęcia!");
        }
    }

    #endregion
}
