using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using UnityEngine;

[System.CLSCompliant(false)]
public static class Client
{
    private delegate void ReceiveMessageMethod(IncomingMessage message);

    public static ClientStatus Status
    {
        get
        {
            if (_client == null)
            {
                return ClientStatus.Disconnected;
            }
            return _client.Status;
        }
    }

    private static IClient _client;
    private static NetOwnPlayer _netOwnPlayer;

    public static void Start(ClientConfig config)
    {
        _client = IoC.GetImplementationClient();
        _client.Start(config);
    }

    public static void Connect(string host, int port)
    {
        _client.Connect(host, port);
    }

    public static void Close()
    {
        _client.Close();
    }

    public static void Listen()
    {
        IncomingMessage m;
        while ((m = _client.ReadMessage()) != null)
        {
            ReceiveMessage(m);
        }
    }

    public static void ExecuteMessage(IncomingMessage message)
    {
        ReceiveMessage(message);
    }

    private static void ReceiveMessage(IncomingMessage message)
    {
        try
        {
            MessageToClientType type = (MessageToClientType)message.ReadInt();
            ReceiveMessageMethod method = ChooseReceiveMessageMethod(type);
            method(message);
        }
        catch (Exception ex)
        {
            MonoBehaviour.print(ex.Message + '\n' + ex.TargetSite + '\n' + ex.StackTrace);
        }
    }

    private static ReceiveMessageMethod ChooseReceiveMessageMethod(MessageToClientType type)
    {
        switch (type)
        {
            case MessageToClientType.WrongLoginOrPassword:
                return ReceiveMessageWrongLoginOrPassword;
            case MessageToClientType.GoToChoiceCharacterMenu:
                return ReceiveMessageGoToChoiceCharacterMenu;
            case MessageToClientType.GoIntoWorld:
                return ReceiveMessageGoIntoWorld;
            case MessageToClientType.CreateYourOwnPlayer:
                return ReceiveMessageCreateYourOwnPlayer;
            case MessageToClientType.MoveOtherPlayer:
                return ReceiveMessageMoveOtherPlayer;
            case MessageToClientType.RotationOtherPlayer:
                return ReceiveMessageRotationOtherPlayer;
            case MessageToClientType.JumpOtherPlayer:
                return ReceiveMessageJumpOtherPlayer;
            case MessageToClientType.CreateOtherPlayer:
                return ReceiveMessageCreateOtherPlayer;
            case MessageToClientType.UpdateItemInEquipment:
                return ReceiveMessageUpdateItemInEquipment;
            case MessageToClientType.UpdateEquipedItem:
                return ReceiveMessageUpdateEquipedItem;
            case MessageToClientType.NewPosition:
                return ReceiveMessageNewPosition;
            case MessageToClientType.NewRotation:
                return ReceiveMessageNewRotation;
            case MessageToClientType.Jump:
                return ReceiveMessageJump;
            case MessageToClientType.CreateBullet:
                return ReceiveMessageCreateBullet;
            case MessageToClientType.CreateMonster:
                return ReceiveMessageCreateMonster;
            case MessageToClientType.CreateItemObject:
                return ReceiveMessageCreateItemObject;
            case MessageToClientType.CreateVisualObject:
                return ReceiveMessageCreateVisualObject;
            case MessageToClientType.DeleteObject:
                return ReceiveMessageDeleteObject;
            case MessageToClientType.PlayerAttack:
                return ReceiveMessagePlayerAttack;
            case MessageToClientType.MonsterAttackTarget:
                return ReceiveMessageMonsterAttackTartget;
            case MessageToClientType.YouAreDead:
                return ReceiveMessageYouAreDead;
            case MessageToClientType.YourRespawn:
                return ReceiveMessageYourRespawn;
            case MessageToClientType.UpdateOtherHp:
                return ReceiveMessageUpdateOtherHp;
            case MessageToClientType.UpdateOtherAllStats:
                return ReceiveMessageUpdateOtherAllStats;
            case MessageToClientType.UpdateYourHp:
                return ReceiveMessageUpdateYourHp;
            case MessageToClientType.UpdateYourAllStats:
                return ReceiveMessageUpdateYourAllStats;
            case MessageToClientType.UpdateYourSpells:
                return ReceiveMessageUpdateYourSpells;
            case MessageToClientType.UpdateYourExperience:
                return ReceiveMessageUpdateYourExperience;
            case MessageToClientType.UpdateOtherEquipedItmes:
                return ReceiveMessageUpdateOtherEquipedItems;
            default:
                throw new NotImplementedException("Brak obsługi podanego typu wiadomości : " + type.ToString());
        }
    }

    public static void SetNetOwnPlayer(NetOwnPlayer player)
    {
        _netOwnPlayer = player;
    }

    public static void ResetNetOnwPlayer()
    {
        _netOwnPlayer = null;
    }

    public static NetOwnPlayer GetNetOwnPlayer()
    {
        return _netOwnPlayer;
    }

    #region ToClient

    private static void ReceiveMessageWrongLoginOrPassword(IncomingMessage message)
    {
        GUIController.ShowFreezingWindow("Error", "Wrong login or password");
    }

    private static void ReceiveMessageGoToChoiceCharacterMenu(IncomingMessage message)
    {
        ApplicationControler.GoToChoiceCharacterMenu(message.Read<CharacterChoiceMenuPackage>());
    }

    private static void ReceiveMessageCreateYourOwnPlayer(IncomingMessage message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        OwnPlayerPackage ownPlayer = message.Read<OwnPlayerPackage>();
        SceneBuilder.CreateOwnPlayer(ownPlayer);
    }   

    private static void ReceiveMessageMoveOtherPlayer(IncomingMessage message)
    {
        int id = message.ReadInt();
        Vector3 newPosition = message.ReadVector3();
        NetOtherPlayer player = FindOtherPlayerByNetId(id);
        player.Movement.SetNewTargetPosition(newPosition);
    }

    private static void ReceiveMessageRotationOtherPlayer(IncomingMessage message)
    {
        int id = message.ReadInt();
        float newRotation = message.ReadFloat();
        NetOtherPlayer player = FindOtherPlayerByNetId(id);
        player.Movement.SetNewRotation(newRotation);
    }

    private static void ReceiveMessageJumpOtherPlayer(IncomingMessage message)
    {
        int id = message.ReadInt();
        Vector3 position = message.ReadVector3();
        Vector3 direction = message.ReadVector3();
        NetOtherPlayer player = FindOtherPlayerByNetId(id);
        player.Movement.Jump(position, direction);
    }

    private static void ReceiveMessageGoIntoWorld(IncomingMessage message)
    {
        WorldInfoPackage worldInfo = message.Read<WorldInfoPackage>();
        ApplicationControler.GoToWorld(worldInfo);
    }

    private static void ReceiveMessageCreateOtherPlayer(IncomingMessage message)
    {
        OtherPlayerPackage otherPlayer = message.Read<OtherPlayerPackage>();
        SceneBuilder.CreateOtherPlayer(otherPlayer);
    }

    private static void ReceiveMessageUpdateItemInEquipment(IncomingMessage message)
    {
        if (_netOwnPlayer == null)
        {
            BufferMessages.DelayExecutionMessage(message);
            return;
        }

        var itemPackage = message.Read<ItemInEquipmentPackage>();
        int numberPlace = message.ReadInt();

        Item item = new Item(itemPackage.IdItem);

        _netOwnPlayer.GetComponent<Equipment>().UpdateSlot(numberPlace, item);

        GUIController.IsActiveEquipmentRefresh();
    }

    private static void ReceiveMessageUpdateEquipedItem(IncomingMessage message)
    {
        var item = message.Read<ItemInEquipmentPackage>();
        ItemEquipableType type = (ItemEquipableType)message.ReadInt();

        Item equipedItem = ItemInEquipmentPackageToItem(item);

        _netOwnPlayer.GetComponent<PlayerEquipement>().Equipe(equipedItem, type);

        _netOwnPlayer.GetComponent<PlayerGeneratorModel>().UpdateEquipedItems();

        GUIController.IsActiveCharacterGUIRefresh();
    }

    private static void ReceiveMessageNewPosition(IncomingMessage message)
    {
        int idNet = message.ReadInt();

        Vector3 position = message.ReadVector3();

        NetObject netObject = FindNetObjectByNetId(idNet);

        netObject.GetComponent<Movement>().SetNewTargetPosition(position);
    }

    private static void ReceiveMessageNewRotation(IncomingMessage message)
    {
        int idNet = message.ReadInt();
        float rotation = message.ReadFloat();

        NetObject netObject = FindNetObjectByNetId(idNet);

        netObject.GetComponent<Movement>().SetNewRotation(rotation);
    }

    private static void ReceiveMessageJump(IncomingMessage message)
    {
        int idNet = message.ReadInt();
        Vector3 position = message.ReadVector3();
        Vector3 direction = message.ReadVector3();

        NetObject netObject = FindNetObjectByNetId(idNet);

        netObject.GetComponent<Movement>().Jump(position, direction);
    }

    private static void ReceiveMessageCreateBullet(IncomingMessage message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        BulletPackage bullet = message.Read<BulletPackage>();

        SceneBuilder.CreateBullet(bullet);      
    }

    private static void ReceiveMessageCreateMonster(IncomingMessage message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        MonsterPackage monster = message.Read<MonsterPackage>();

        SceneBuilder.CreateMonster(monster);
    }

    private static void ReceiveMessageCreateItemObject(IncomingMessage message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        ItemPackage item = message.Read<ItemPackage>();

        SceneBuilder.CreateItem(item);
    }

    private static void ReceiveMessageCreateVisualObject(IncomingMessage message)
    {
        var visualObjectPackage = message.Read<VisualObjectPackage>();

        SceneBuilder.CreateVisualObject(visualObjectPackage);
    }

    private static void ReceiveMessageDeleteObject(IncomingMessage message)
    {
        int idObject = message.ReadInt();

        NetObject netObject = FindNetObjectByNetId(idObject);

        if (netObject == null)
        {
            BufferMessages.DelayExecutionMessage(message);
            return;
        }

        GameObject.Destroy(netObject.gameObject);
    }

    private static void ReceiveMessagePlayerAttack(IncomingMessage message)
    {
        int idNetPlayer = message.ReadInt();

        NetOtherPlayer player = FindOtherPlayerByNetId(idNetPlayer);

        player.Combat.Attack();
    }

    private static void ReceiveMessageMonsterAttackTartget(IncomingMessage message)
    {
        int idAttacker = message.ReadInt();
        int idVictim = message.ReadInt();

        NetMonster monster = FindNetObjectByNetId(idAttacker) as NetMonster;

        monster.Combat.AttackTarget(FindNetObjectByNetId(idVictim));
    }

    private static void ReceiveMessageYouAreDead(IncomingMessage message)
    {
        OwnDeadEventPackage deadInfo = message.Read<OwnDeadEventPackage>();

        _netOwnPlayer.Dead(deadInfo);
    }

    private static void ReceiveMessageYourRespawn(IncomingMessage message)
    {
        Vector3 position = message.ReadVector3();

        _netOwnPlayer.transform.position = position;
    }

    private static void ReceiveMessageUpdateOtherHp(IncomingMessage message)
    {
        int idObject = message.ReadInt();      

        NetObject netObject = FindNetObjectByNetId(idObject);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        int hp = message.ReadInt();

        netObject.GetComponent<HP>()._hp = hp;  
    }

    private static void ReceiveMessageUpdateOtherAllStats(IncomingMessage message)
    {
        int netId = message.ReadInt();

        var netObject = FindNetObjectByNetId(netId);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Stats>().Set(message);
    }

    private static void ReceiveMessageUpdateOtherEquipedItems(IncomingMessage message)
    {
        int netId = message.ReadInt();

        NetObject netObject = FindNetObjectByNetId(netId);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        IEquippedItems items = (IEquippedItems)netObject.GetComponent(typeof(IEquippedItems));

        items.Set(message.Read<EquipedItemsPackage>());

        PlayerGeneratorModel generatorModel = netObject.GetComponent<PlayerGeneratorModel>();

        if (generatorModel != null)
        {
            generatorModel.UpdateEquipedItems();
        }
    }

    private static void ReceiveMessageUpdateYourHp(IncomingMessage message)
    {
        if (_netOwnPlayer == null)
        {
            BufferMessages.DelayExecutionMessage(message);
            return;
        }

        _netOwnPlayer.GetComponent<HP>()._hp = message.ReadInt();

        GUIController.IsActiveCharacterGUIRefresh();
    }

    private static void ReceiveMessageUpdateYourAllStats(IncomingMessage message)
    {
        _netOwnPlayer.GetComponent<OwnPlayerStats>().Set(message);

        GUIController.IsActiveCharacterGUIRefresh();
    }

    private static void ReceiveMessageUpdateYourSpells(IncomingMessage message)
    {
        if (_netOwnPlayer == null)
        {
            BufferMessages.DelayExecutionMessage(message);
            return;
        }

        var spells = message.Read<ListPackage<SpellPackage>>();

        _netOwnPlayer.GetComponent<SpellCaster>().Set(spells);
    }

    private static void ReceiveMessageUpdateYourExperience(IncomingMessage message)
    {
        if (_netOwnPlayer == null)
        {
            BufferMessages.DelayExecutionMessage(message);
            return;
        }

        int lvl = message.ReadInt();
        int exp = message.ReadInt();

        _netOwnPlayer.GetComponent<PlayerExperience>().Set(lvl, exp);
    }

    #endregion

    #region ToServer

    public static void SendMessageLogin(string login, string password)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToServerType.Login);
        message.Write(login);
        message.Write(password);
        Send(message);
    }

    public static void SendMessageGoIntoWorld(int characterIndex)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToServerType.GoIntoWorld);
        message.Write(characterIndex);
        Send(message);
    }

    public static void SendMessagePlayerMove(Vector3 position)
    {
        OutgoingMessage message = new OutgoingMessage();
        message.Write((int)MessageToServerType.PlayerMove);
        message.Write(position);
        Send(message);
    }

    public static void SendMessageAskForImage(int idImage)
    {
        OutgoingMessage message = new OutgoingMessage();
        message.Write((int)MessageToServerType.AskForImage);
        message.Write(idImage);
        Send(message);
    }

    public static void SendMessageAskForItem(int idItem)
    {
        OutgoingMessage message = new OutgoingMessage();
        message.Write((int)MessageToServerType.AskForItem);
        message.Write(idItem);
        Send(message);
    }

    public static void SendMessageAttack(Vector3 direction)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToServerType.Attack);
        message.Write(direction);
        Send(message);
    }

    public static void SendMessagePlayerRotation(float newRotation)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToServerType.PlayerRotation);
        message.Write(newRotation);
        Send(message);
    }

    public static void SendMessagePlayerJump(Vector3 position, Vector3 direction)
    {
        OutgoingMessage message = new OutgoingMessage((int)MessageToServerType.PlayerJump);
        message.Write(position);
        message.Write(direction);
        Send(message);
    }

    public static void SendMessageRespawn()
    {
        var message = new OutgoingMessage((int)MessageToServerType.Respawn);
        Send(message);
    }

    public static void SendMessagePickItem(int idObjectItem)
    {
        var message = new OutgoingMessage((int)MessageToServerType.PickItem);
        message.Write(idObjectItem);
        Send(message);
    }

    public static void SendMessageChangeItemsInEquipment(int slot1, int slot2)
    {
        var message = new OutgoingMessage((int)MessageToServerType.ChangeItemsInEquipment);
        message.Write(slot1);
        message.Write(slot2);
        Send(message);
    }

    public static void SendMessageChangeEquipedItem(int slot, ItemEquipableType place)
    {
        var message = new OutgoingMessage(MessageToServerType.ChangeEquipedItem);
        message.Write(slot);
        message.Write((int)place);
        Send(message);
    }

    public static void SendMessageChangeEquipedItems(ItemEquipableType place1, ItemEquipableType place2)
    {
        var message = new OutgoingMessage(MessageToServerType.ChangeEquipedItems);
        message.Write((int)place1);
        message.Write((int)place2);
        Send(message);
    }

    public static void SendMessageUseSpell(SpellCastPackage spellCastInfo)
    {
        var message = new OutgoingMessage(MessageToServerType.UseSpell);
        message.Write(spellCastInfo);
        Send(message);
    }

    #endregion

    private static void Send(OutgoingMessage message)
    {
        _client.Send(message);
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

    private static NetObject FindNetObjectByNetId(int netId)
    {
        foreach (NetObject netObject in GameObject.FindObjectsOfType(typeof(NetObject)) as NetObject[])
        {
            if (netObject.IdObject == netId)
            {
                return netObject;
            }
        }

        return null;
    }

    private static NetOtherPlayer FindOtherPlayerByNetId(int netId)
    {
        foreach(NetOtherPlayer player in GameObject.FindObjectsOfType(typeof(NetOtherPlayer))  as NetOtherPlayer[])
        {
            if (player.IdObject == netId)
            {
                return player;
            }
        }
        return null;
    }

    private static bool IfNewSceneLoadedDelayExecutionMessage(IncomingMessage message)
    {
        if (SceneBuilder.SceneIsLoadind())
        {
            BufferMessages.DelayExecutionMessage(message);
            return true;
        }

        return false;
    }

    private static Item ItemInEquipmentPackageToItem(ItemInEquipmentPackage itemPackage)
    {
        if (itemPackage.IdItem == -1)
        {
            return null;
        }
        else
        {
            return new Item(itemPackage.IdItem);
        }
    }

    private static bool IfNullDelayMessage(object flag, IncomingMessage message)
    {
        if (flag == null)
        {
            BufferMessages.DelayExecutionMessage(message);
            return true;
        }
        else
        {
            return false;
        }
    }
}
