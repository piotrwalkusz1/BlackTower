using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Connection;
using NetworkProject.Connection.ToServer;
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

    public static void Start(ServerConfig config)
    {
        _server = Standard.IoC.GetImplementationServer();
        _server.Start(config);
    }       

    public static void Close()
    {
        _server.Close();
    }

    public static void Listen()
    {
        IncomingMessage m;
        while ((m = _server.ReadMessage()) != null)
        {
            ReceiveMessage(m);
        }
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

    private static void ReceiveMessage(IncomingMessage message)
    {
        try
        {
            MessageToServerType type = (MessageToServerType)message.ReadInt();
            ReceiveMessageMethod method = ChooseMethodReceiveMessage(type);
            IConnectionMember sender = message.Sender;
            method(message, sender);
        }
        catch (Exception ex)
        {
            MonoBehaviour.print(ex.Message + '\n' + ex.TargetSite + '\n' + ex.StackTrace);
        }
    }

    private static Action<IncomingMessage> ChooseMethodReceiveMessage(INetworkRequest message)
    {
        if (message is LoginToGame) return ReceiveMessageLogin;
        else if (message is GoIntoWorld) return ReceiveMessageGoIntoWorld;
        else if (message is PlayerMove) return ReceiveMessagePlayerMove;
        else if (message is PlayerJump) return ReceiveMessagePlayerJump;
        else if (message is PlayerRotation) return ReceiveMessagePlayerRotation;
        else if (message is PickItem) return ReceiveMessagePickItem;
        else if (message is Attack) return ReceiveMessageAttack;
        else if (message is Respawn) return ReceiveMessageRespawn;
        else if (message is ChangeItemsInEquipment) return ReceiveMessageChangeItemsInEquipment;
        else if (message is ChangeEquipedItem) return ReceiveMessageChangeEquipedItem;
        else if (message is ChangeEquipedItems) return ReceiveMessageChangeEquipedItems;
        else if (message is UseSpell) return ReceiveMessageUseSpell;
        else throw new Exception("Nie ma takiego typu żądania!");
    }

    #region Receive

    private static void ReceiveMessageLogin(IncomingMessage message)
    {
        var request = (LoginToGame)message.Request;
        
        RegisterAccount account = AccountsRepository.GetAccountByLogin(login);

        if (account != null && account.CheckPassword(password))
        {
            OnlineAccount onlineAccount = AccountsRepository.GetOnlineAccountByIdAccount(account.IdAccount);
            if (onlineAccount == null)
            {
                AccountsRepository.LoginAccount(account, sender);

                CharacterChoiceMenuPackage menuInfo = AccountToCharacterChoiceMenuInfo(account);

                SendMessageGoToChoiceCharacterMenu(menuInfo, sender);
            }
            else
            {
                AccountsRepository.LogoutAccount(onlineAccount);
                SendMessageTextMessage(TextRepository.AccountHasAlreadyBeenLogged, sender);
            }    
        }        
        else 
        {
            SendMessageWrongLoginOrPassword(sender);
        }
    }

    private static void ReceiveMessageGoIntoWorld(IncomingMessage message)
    {
        OnlineAccount account = AccountsRepository.GetOnlineAccountByAddress(sender);
        if (account != null)
        {
            if (!account.IsLoggedCharacter())
            {
                int numberCharacter = message.ReadInt();
                RegisterAccount registerAccount = AccountsRepository.GetAccountById(account.IdAccount);
                RegisterCharacter registerCharacter = registerAccount.Characters[numberCharacter];
                OnlineCharacter onlineCharacter = AccountsRepository.LoginAndCreateCharacter(registerCharacter);
                OwnPlayerPackage ownPlayer = OnlineCharacterToOwnPlayer(onlineCharacter);

                WorldInfoPackage worldInfo = new WorldInfoPackage();
                worldInfo.MapNumber = onlineCharacter.NetPlayerObject.GetMap();

                SendMessageGoIntoWorld(worldInfo, sender);
                SendMessageCreateYourOwnPlayer(ownPlayer, sender);   
            }
            else
            {
                AccountsRepository.LogoutAndDeleteCharacter(account.OnlineCharacter);
                SendMessageTextMessage(TextRepository.SomeCharacterHasAlreadyBeenLogged, sender);
            }            
        }
        else
        {
            SendMessageTextMessage(TextRepository.AccountIsNotLogged, sender);
        }
    }

    private static void ReceiveMessagePlayerMove(IncomingMessage message)
    {
        NetPlayer player = FindAliveNetPlayerByAddress(sender);

        Vector3 newTargetPosition = message.ReadVector3();
        player.PlayerMovement.SetNewPosition(newTargetPosition);
    }

    private static void ReceiveMessagePlayerJump(IncomingMessage message)
    {
        NetPlayer player = FindAliveNetPlayerByAddress(sender);

        Vector3 position = message.ReadVector3();
        Vector3 direction = message.ReadVector3();
        player.PlayerMovement.Jump(position, direction);
    }

    private static void ReceiveMessagePlayerRotation(IncomingMessage message)
    {
        NetPlayer player = FindAliveNetPlayerByAddress(sender);

        float rotationY = message.ReadFloat();
        player.PlayerMovement.SetNewRotation(rotationY);
    }

    private static void ReceiveMessagePickItem(IncomingMessage message)
    {
        int idObject = message.ReadInt();
        NetItem netItem = FindNetItemByIdObject(idObject);

        if (netItem != null)
        {
            NetPlayer player = FindAliveNetPlayerByAddress(sender);

            if (netItem.PlayerIsEnoughCloseToPick(player) && player.Equipment.IsFreePlace())
            {
                int numberPlace = player.Equipment.AddItem(netItem.Item);

                ItemInEquipmentPackage item = ItemToItemInEquipmentPackage(netItem.Item);

                SendMessageUpdateItemInEquipment(item, numberPlace, sender);

                SceneBuilder.DeleteObject(netItem.gameObject);
            }
        }
        else
        {
            MonoBehaviour.print("Item is null");
        }
    }

    private static void ReceiveMessageAttack(IncomingMessage message)
    {
        Vector3 direction = message.ReadVector3();

        OnlineCharacter player = AccountsRepository.GetOnlineCharacterByAddress(sender);

        player.PlayerCombat.Attack(direction);
    }

    private static void ReceiveMessageRespawn(IncomingMessage message)
    {
        NetPlayer player = FindDeadNetPlayerByAddress(sender);

        PlayerRespawn respawn = FindNearestPlayerRespawnOnMap(player.GetMap(), player.transform.position);

        respawn.Respawn(player);
    }

    private static void ReceiveMessageChangeItemsInEquipment(IncomingMessage message)
    {
        int slot1 = message.ReadInt();
        int slot2 = message.ReadInt();

        NetPlayer netPlayer = FindAliveNetPlayerByAddress(sender);
        Equipment eq = netPlayer.GetComponent<Equipment>();

        eq.ChangeItemInEquipment(slot1, slot2);

        eq.SendUpdateSlot(slot1, sender);
        eq.SendUpdateSlot(slot2, sender);
    }

    private static void ReceiveMessageChangeEquipedItem(IncomingMessage message)
    {
        int slot = message.ReadInt();
        BodyPartSlot bodyPart = (BodyPartSlot)message.ReadInt();

        NetPlayer player = FindAliveNetPlayerByAddress(sender);
        PlayerEquipment eq = player.GetComponent<PlayerEquipment>();

        Item equipedItem = eq.GetEquipedItem(bodyPart);
        Item itemInSlot = eq.GetItemBySlot(slot);

        if (itemInSlot != null && !itemInSlot.CanBeEquipedByPlayerOnThisBodyPart(player, bodyPart))
        {
            throw new Exception("Nie można założyć!");
        }

        eq.EquipeItem(itemInSlot, bodyPart);
        eq.SetItem(equipedItem, slot);    

        SendMessageUpdateEquipedItem(ItemToItemInEquipmentPackage(itemInSlot), bodyPart, sender);
        SendMessageUpdateItemInEquipment(ItemToItemInEquipmentPackage(equipedItem), slot, sender);

        player.GetComponent<PlayerStats>().CalculateStatsAndSendUpdate();

        player.SendUpdateEquipedItems();
    }

    private static void ReceiveMessageChangeEquipedItems(IncomingMessage message)
    {
        BodyPartSlot bodyPart1 = (BodyPartSlot)message.ReadInt();
        BodyPartSlot bodyPart2 = (BodyPartSlot)message.ReadInt();

        if (bodyPart1 == bodyPart2)
        {
            throw new Exception("Te same miejsca!");
        }  

        NetPlayer player = FindAliveNetPlayerByAddress(sender);
        PlayerEquipment eq = player.GetComponent<PlayerEquipment>();

        Item item1 = eq.GetEquipedItem(bodyPart1);
        Item item2 = eq.GetEquipedItem(bodyPart2);

        if ((item1 != null && !item1.CanBeEquipedByPlayerOnThisBodyPart(player, bodyPart2)) || (item2 != null && !item2.CanBeEquipedByPlayerOnThisBodyPart(player, bodyPart1)))
        {
            throw new Exception("Nie można założyć itemu.");
        }

        eq.EquipeItem(item1, bodyPart2);
        eq.EquipeItem(item2, bodyPart1);

        SendMessageUpdateEquipedItem(ItemToItemInEquipmentPackage(item1), bodyPart2, sender);
        SendMessageUpdateEquipedItem(ItemToItemInEquipmentPackage(item2), bodyPart1, sender);

        player.GetComponent<PlayerStats>().CalculateStatsAndSendUpdate();

        player.SendUpdateEquipedItems();
    }

    private static void ReceiveMessageUseSpell(IncomingMessage message)
    {
        var player = FindAliveNetPlayerByAddress(sender);

        var spellCastInfo = message.Read<SpellCastPackage>();

        string reason;
        bool success = player.GetComponent<SpellCaster>().CastSpell(spellCastInfo._idSpell, out reason);
        
        if (!success)
        {
            MonoBehaviour.print(reason);
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
        OnlineCharacter onlineCharacter = AccountsRepository.GetOnlineCharacterByAddress(address);
        return onlineCharacter.NetPlayerObject;
    }

    private static NetPlayer FindAliveNetPlayerByAddress(IConnectionMember address)
    {
        OnlineCharacter onlineCharacter = AccountsRepository.GetOnlineCharacterByAddress(address);
        
        if(onlineCharacter.NetPlayerObject.IsDead())
        {
            throw new Exception("Gracz jest martwy");
        }

        return onlineCharacter.NetPlayerObject;
    }

    private static NetPlayer FindDeadNetPlayerByAddress(IConnectionMember address)
    {
        OnlineCharacter onlineCharacter = AccountsRepository.GetOnlineCharacterByAddress(address);
        
        if (!onlineCharacter.NetPlayerObject.IsDead())
        {
            throw new Exception("Gracz jest żywy");
        }

        return onlineCharacter.NetPlayerObject;
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
