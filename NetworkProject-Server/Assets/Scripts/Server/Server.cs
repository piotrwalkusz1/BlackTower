using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.BodyParts;
using UnityEngine;

[System.CLSCompliant(false)]
public static class Server
{
    private delegate void ReceiveMessageMethod(IncomingMessage message, IConnectionMember sender);

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

    public static void Send(INetworkPackage message)
    {

    }

    private static void ReceiveMessage(IncomingMessage message)
    {
        try
        {
            MessageToServerType type = (MessageToServerType)message.ReadInt();
            ReceiveMessageMethod method = ChooseReceiveMessageMethod(type);
            IConnectionMember sender = message.Sender;
            method(message, sender);
        }
        catch (Exception ex)
        {
            MonoBehaviour.print(ex.Message + '\n' + ex.TargetSite + '\n' + ex.StackTrace);
        }
    }

    private static ReceiveMessageMethod ChooseReceiveMessageMethod(MessageToServerType type)
    {
        switch (type)
        {
            case MessageToServerType.Login:
                return ReceiveMessageLogin;
            case MessageToServerType.GoIntoWorld:
                return ReceiveMessageGoIntoWorld;
            case MessageToServerType.PlayerMove:
                return ReceiveMessagePlayerMove;
            case MessageToServerType.PlayerJump:
                return ReceiveMessagePlayerJump;
            case MessageToServerType.PlayerRotation:
                return ReceiveMessagePlayerRotation;
            case MessageToServerType.PickItem:
                return ReceiveMessagePickItem;
            case MessageToServerType.Register:
                throw new NotImplementedException("Brak obsługi podanego typu wiadomości.");
            case MessageToServerType.Attack:
                return ReceiveMessageAttack;
            case MessageToServerType.Respawn:
                return ReceiveMessageRespawn;
            case MessageToServerType.ChangeItemsInEquipment:
                return ReceiveMessageChangeItemsInEquipment;
            case MessageToServerType.ChangeEquipedItem:
                return ReceiveMessageChangeEquipedItem;
            case MessageToServerType.ChangeEquipedItems:
                return ReceiveMessageChangeEquipedItems;
            case MessageToServerType.UseSpell:
                return ReceiveMessageUseSpell;
            default:
                throw new NotImplementedException("Brak obsługi podanego typu wiadomości.");
        }
    }

    #region ToServer

    private static void ReceiveMessageLogin(IncomingMessage message, IConnectionMember sender)
    {
        string login = message.ReadString();
        string password = message.ReadString();
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

    private static void ReceiveMessageGoIntoWorld(IncomingMessage message, IConnectionMember sender)
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

    private static void ReceiveMessagePlayerMove(IncomingMessage message, IConnectionMember sender)
    {
        NetPlayer player = FindAliveNetPlayerByAddress(sender);

        Vector3 newTargetPosition = message.ReadVector3();
        player.PlayerMovement.SetNewPosition(newTargetPosition);
    }

    private static void ReceiveMessagePlayerJump(IncomingMessage message, IConnectionMember sender)
    {
        NetPlayer player = FindAliveNetPlayerByAddress(sender);

        Vector3 position = message.ReadVector3();
        Vector3 direction = message.ReadVector3();
        player.PlayerMovement.Jump(position, direction);
    }

    private static void ReceiveMessagePlayerRotation(IncomingMessage message, IConnectionMember sender)
    {
        NetPlayer player = FindAliveNetPlayerByAddress(sender);

        float rotationY = message.ReadFloat();
        player.PlayerMovement.SetNewRotation(rotationY);
    }

    private static void ReceiveMessagePickItem(IncomingMessage message, IConnectionMember sender)
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

    private static void ReceiveMessageAttack(IncomingMessage message, IConnectionMember sender)
    {
        Vector3 direction = message.ReadVector3();

        OnlineCharacter player = AccountsRepository.GetOnlineCharacterByAddress(sender);

        player.PlayerCombat.Attack(direction);
    }

    private static void ReceiveMessageRespawn(IncomingMessage message, IConnectionMember sender)
    {
        NetPlayer player = FindDeadNetPlayerByAddress(sender);

        PlayerRespawn respawn = FindNearestPlayerRespawnOnMap(player.GetMap(), player.transform.position);

        respawn.Respawn(player);
    }

    private static void ReceiveMessageChangeItemsInEquipment(IncomingMessage message, IConnectionMember sender)
    {
        int slot1 = message.ReadInt();
        int slot2 = message.ReadInt();

        NetPlayer netPlayer = FindAliveNetPlayerByAddress(sender);
        Equipment eq = netPlayer.GetComponent<Equipment>();

        eq.ChangeItemInEquipment(slot1, slot2);

        eq.SendUpdateSlot(slot1, sender);
        eq.SendUpdateSlot(slot2, sender);
    }

    private static void ReceiveMessageChangeEquipedItem(IncomingMessage message, IConnectionMember sender)
    {
        int slot = message.ReadInt();
        BodyPartType bodyPart = (BodyPartType)message.ReadInt();

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

    private static void ReceiveMessageChangeEquipedItems(IncomingMessage message, IConnectionMember sender)
    {
        BodyPartType bodyPart1 = (BodyPartType)message.ReadInt();
        BodyPartType bodyPart2 = (BodyPartType)message.ReadInt();

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

    private static void ReceiveMessageUseSpell(IncomingMessage message, IConnectionMember sender)
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

    #region ToClient

    public static void SendMessageNewPosition(int idNetObject, Vector3 newPosition, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage();
        message.Write((int)MessageToClientType.NewPosition);
        message.Write(idNetObject);
        message.Write(newPosition);
        Send(message, address);
    }

    public static void SendMessageNewRotation(int idNetObject, float rotation, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.NewRotation);
        message.Write(idNetObject);
        message.Write(rotation);
        Send(message, address);
    }

    public static void SendMessageJump(int idNetObject, Vector3 position, Vector3 direction, IConnectionMember address)
    {
        var message = new OutgoingMessage((int)MessageToClientType.Jump);
        message.Write(idNetObject);
        message.Write(position);
        message.Write(direction);
        Send(message, address);
    }

    public static void SendMessageGoToChoiceCharacterMenu(CharacterChoiceMenuPackage choiceCharacterMenuPackage, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.GoToChoiceCharacterMenu);
        message.Write(choiceCharacterMenuPackage);       
        Send(message, address);
    }

    public static void SendMessageGoIntoWorld(WorldInfoPackage worldInfo, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.GoIntoWorld);
        message.Write(worldInfo);
        Send(message, address);
    }

    public static void SendMessageWrongLoginOrPassword(IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage();
        message.Write((int)MessageToClientType.WrongLoginOrPassword);
        Send(message, address);
    }

    public static void SendMessageTextMessage(string text, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage();
        message.Write((int)MessageToClientType.TextMessage);
        message.Write(text);
        Send(message, address);
    }

    public static void SendMessageCreateYourOwnPlayer(OwnPlayerPackage player, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.CreateYourOwnPlayer);
        message.Write(player);
        Send(message, address);
    }

    public static void SendMessageCreateOtherPlayer(OtherPlayerPackage player, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.CreateOtherPlayer);
        message.Write(player);
        Send(message, address);
    }

    public static void SendMessageDeleteObject(int idNetObject, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.DeleteObject);
        message.Write(idNetObject);
        Send(message, address);
    }

    public static void SendMessageMoveOtherPlayer(int idNetObject, Vector3 newPosition, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.MoveOtherPlayer);
        message.Write(idNetObject);
        message.Write(newPosition);
        Send(message, address);
    }

    public static void SendMessageRotationOtherPlayer(int idNetObject, float newRotation, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.RotationOtherPlayer);
        message.Write(idNetObject);
        message.Write(newRotation);
        Send(message, address);
    }

    public static void SendMessageJumpOtherPlayer(int idNetObject, Vector3 position, Vector3 direction, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.JumpOtherPlayer);
        message.Write(idNetObject);
        message.Write(position);
        message.Write(direction);
        Send(message, address);
    }

    public static void SendMessageUpdateItemInEquipment(ItemInEquipmentPackage item, int numberPlace, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.UpdateItemInEquipment);
        message.Write(item);
        message.Write(numberPlace);
        Send(message, address);
    }

    public static void SendMessageUpdateEquipedItem(ItemInEquipmentPackage item, BodyPartType bodyPart, IConnectionMember address)
    {
        var message = new OutgoingMessage((int)MessageToClientType.UpdateEquipedItem);
        message.Write(item);
        message.Write((int)bodyPart);
        Send(message, address);
    }

    public static void SendMessageCreateObject(ObjectPackage netObject, Vector3 position, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.CreateObject);
        message.Write(netObject);
        message.Write(position);
        Send(message, address);
    }

    public static void SendMessageCreateVisualObject(ObjectPackage netObject, int idPrefab, Vector3 position, float rotation, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.CreateVisualObject);
        message.Write(netObject);
        message.Write(idPrefab);
        message.Write(position);
        message.Write(rotation);
        Send(message, address);
    }

    public static void SendMessageCreateBullet(BulletPackage bullet, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.CreateBullet);
        message.Write(bullet);
        Send(message, address);
    }

    public static void SendMessageCreateMonster(MonsterPackage monster, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.CreateMonster);
        message.Write(monster);
        Send(message, address);
    }

    public static void SendMessageCreateItemObject(ItemPackage item, IConnectionMember address)
    {
        var message = new OutgoingMessage((int)MessageToClientType.CreateItemObject);
        message.Write(item);
        Send(message, address);
    }

    public static void SendMessageMonsterAttackTarget(int idNetAttacker, int idNetVictim, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.MonsterAttackTarget);
        message.Write(idNetAttacker);
        message.Write(idNetVictim);
        Send(message, address);
    }

    public static void SendMessagePlayerAttack(int idNetPlayer, IConnectionMember address)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToClientType.PlayerAttack);
        message.Write(idNetPlayer);
        Send(message, address);
    }

    public static void SendMessageYouAreDead(OwnDeadEventPackage deadInfo, IConnectionMember address)
    {
        var message = new OutgoingMessage((int)MessageToClientType.YouAreDead);
        message.Write(deadInfo);
        Send(message, address);
    }

    public static void SendMessageYourRespawn(Vector3 position, IConnectionMember address)
    {
        var message = new OutgoingMessage((int)MessageToClientType.YourRespawn);
        message.Write(position);
        Send(message, address);
    }

    public static void SendMessageUpdateOtherHp(int netId, int hp, IConnectionMember address)
    {
        var message = new OutgoingMessage((int)MessageToClientType.UpdateOtherHp);
        message.Write(netId);
        message.Write(hp);
        Send(message, address);
    }

    public static void SendMessageUpdateOtherMaxHp(int netId, int maxHp, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.UpdateOtherMaxHp);
        message.Write(netId);
        message.Write(maxHp);
        Send(message, address);
    }

    public static void SendMessageUpdateOtherAllStats(int netId, StatsPackage statsPackage, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.UpdateOtherAllStats);
        message.Write(netId);
        message.Write(statsPackage);
        Send(message, address);
    }

    public static void SendMessageUpdateOtherEquipedItems(int netId, EquipedItems equipedItems, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.UpdateOtherEquipedItmes);
        message.Write(netId);
        message.Write(equipedItems);
        Send(message, address);
    }

    public static void SendMessageUpdateYourHp(int hp, IConnectionMember address)
    {
        var message = new OutgoingMessage((int)MessageToClientType.UpdateYourHp);
        message.Write(hp);
        Send(message, address);
    }

    public static void SendMessageUpdateYourMaxHp(int maxHp, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.UpdateYourMaxHp);
        message.Write(maxHp);
        Send(message, address);
    }

    public static void SendMessageUpdateYourAllStats(OwnPlayerStatsPackage statsPackage, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.UpdateYourAllStats);
        message.Write(statsPackage);
        Send(message, address);
    }

    public static void SendMessageUpdateYourSpells(ListPackage<SpellPackage> spells, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.UpdateYourSpells);
        message.Write(spells);
        Send(message, address);
    }

    public static void SendMessageUpdateYourExperience(int lvl, int exp, IConnectionMember address)
    {
        var message = new OutgoingMessage(MessageToClientType.UpdateYourExperience);
        message.Write(lvl);
        message.Write(exp);
        Send(message, address);
    }

    #endregion

    private static void Send(OutgoingMessage message, IConnectionMember address)
    {
        _server.Send(message, address);
    }

    private static void Send(OutgoingMessage message, IConnectionMember[] addresses)
    {
        _server.Send(message, addresses);
    }

    private static string[] BitsToStrings(byte[] bytes, int length, char separator)
    {
        string text = BitsToString(bytes, length);
        return text.Split(new char[] { separator });
    }

    private static string BitsToString(byte[] bytes, int length)
    {
        string text = "";
        for (int i = 0; i < length; i += 2)
        {
            text += BitConverter.ToChar(bytes, i);
        }
        return text;
    }

    private static CharacterChoiceMenuPackage AccountToCharacterChoiceMenuInfo(RegisterAccount account)
    {
        var menuInfo = new CharacterChoiceMenuPackage();

        int count = account.Characters.Count;

        for (int i = 0; i < count; i++)
        {           
            if (account.Characters[i] != null)
            {
                var character = new CharacterInChoiceMenuPackage();
                character.Name = account.Characters[i].Name;

                menuInfo.AddCharacter(character);
            }
        }

        return menuInfo;
    }

    private static OwnPlayerPackage OnlineCharacterToOwnPlayer(OnlineCharacter onlineCharacter)
    {
        return onlineCharacter.GameObject.GetComponent<NetPlayer>().GetOwnPlayerPackage();
    }

    private static NetItem FindNetItemByIdObject(int idObject)
    {
        NetItem[] items = GameObject.FindObjectsOfType(typeof(NetItem)) as NetItem[];
        foreach (NetItem item in items)
        {
            if (item.IdObject == idObject)
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

    public static ItemInEquipmentPackage ItemToItemInEquipmentPackage(Item item)
    {
        if (item == null)
        {
            return new ItemInEquipmentPackage(-1);
        }
        else
        {
            return new ItemInEquipmentPackage(item.IdItem);
        } 
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
