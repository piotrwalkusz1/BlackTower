using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Connection.ToServer;
using NetworkProject.Items;
using UnityEngine;
using Standard;

[System.CLSCompliant(false)]
public static class Client
{
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
        IncomingMessageFromServer m;
        while ((m = _client.ReadMessage()) != null)
        {
            ReceiveMessage(m);
        }
    }

    public static void ExecuteMessage(IncomingMessageFromServer message)
    {
        ReceiveMessage(message);
    }

    private static void ReceiveMessage(IncomingMessageFromServer message)
    {
        try
        {
            Action<IncomingMessageFromServer> method = ChooseReceiveMessageMethod(message.Request);
            method(message);
        }
        catch (Exception ex)
        {
            MonoBehaviour.print(ex.Message + '\n' + ex.TargetSite + '\n' + ex.StackTrace);
        }
    }

    private static Action<IncomingMessageFromServer> ChooseReceiveMessageMethod(INetworkRequest request)
    {
        if(request is ErrorMessageToClient)
            return ReceiveMessageErrorMessage;
        else if(request is GoToChoiceCharacterMenuToClient)
            return ReceiveMessageGoToChoiceCharacterMenu;
        else if(request is GoIntoWorldToClient)
            return ReceiveMessageGoIntoWorld;
        else if(request is CreateOwnPlayerToClient)
            return ReceiveMessageCreateOwnPlayer;
        else if(request is CreateOtherPlayerToClient)
            return ReceiveMessageCreateOtherPlayer;
        else if(request is CreateBulletToClient)
            return ReceiveMessageCreateBullet;
        else if(request is CreateItemToClient)
            return ReceiveMessageCreateItemObject;
        else if(request is CreateMonsterToClient)
            return ReceiveMessageCreateMonster;
        else if(request is CreateVisualObjectToClient)
            return ReceiveMessageCreateVisualObject;
        else if(request is DeleteObjectToClient)
            return ReceiveMessageDeleteObject;
        else if(request is MoveToClient)
            return ReceiveMessageMove;
        else if(request is RotateToClient)
            return ReceiveMessageRotate;
        else if(request is NewPositionToClient)
            return ReceiveMessageNewPosition;
        else if(request is UpdateItemInEquipmentToClient)
            return ReceiveMessageUpdateItemInEquipment;
        else if(request is UpdateEquipedItemToClient)
            return ReceiveMessageUpdateEquipedItem;
        else if(request is AttackToClient)
            return ReceiveMessageAttack;
        else if(request is DeadToClient)
            return ReceiveMessageDead;
        else if(request is RespawnToClient)
            return ReceiveMessageRespawn;
        else if(request is UpdateAllSpellsToClient)
            throw new NotImplementedException();
        else if(request is UpdateAllStatsToClient)
            return ReceiveMessageUpdateAllStats;
        else if(request is UpdateHPToClient)
            return ReceiveMessageUpdateHP;
        else if(request is UpdateExpToClient)
            return ReceiveMessageUpdateExp;
        else throw new NotImplementedException("Brak obsługi podanego typu wiadomości : " + request.GetType().ToString());
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

    public static void SendRequestAsMessage(INetworkRequest request)
    {
        var message = new OutgoingMessage(request);

        Send(message);
    }

    public static void Send(OutgoingMessage message)
    {
        _client.Send(message);
    }

    #region ToClient

    private static void ReceiveMessageErrorMessage(IncomingMessageFromServer message)
    {
        var request = (ErrorMessageToClient)message.Request;

        string errorText = Languages.GetErrorText((int)request.ErrorCode);

        GUIController.ShowWindow(Languages.GetSentence("error"), errorText);
    }

    private static void ReceiveMessageGoToChoiceCharacterMenu(IncomingMessageFromServer message)
    {
        ApplicationControler.GoToChoiceCharacterMenu((GoToChoiceCharacterMenuToClient)message.Request);
    }

    private static void ReceiveMessageCreateOwnPlayer(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        var request = (CreateOwnPlayerToClient)message.Request;

        SceneBuilder.CreateOwnPlayer(request);
    }

    private static void ReceiveMessageMove(IncomingMessageFromServer message)
    {
        int id = message.ReadInt();
        Vector3 newPosition = message.ReadVector3();
        NetOtherPlayer player = FindOtherPlayerByNetId(id);
        player.Movement.SetNewTargetPosition(newPosition);
    }

    private static void ReceiveMessageRotate(IncomingMessageFromServer message)
    {
        int id = message.ReadInt();
        float newRotation = message.ReadFloat();
        NetOtherPlayer player = FindOtherPlayerByNetId(id);
        player.Movement.SetNewRotation(newRotation);
    }

    private static void ReceiveMessageJump(IncomingMessageFromServer message)
    {
        int id = message.ReadInt();
        Vector3 position = message.ReadVector3();
        Vector3 direction = message.ReadVector3();
        NetOtherPlayer player = FindOtherPlayerByNetId(id);
        player.Movement.Jump(position, direction);
    }

    private static void ReceiveMessageGoIntoWorld(IncomingMessageFromServer message)
    {
        WorldInfoPackage worldInfo = message.Read<WorldInfoPackage>();
        ApplicationControler.GoToWorld(worldInfo);
    }

    private static void ReceiveMessageCreateOtherPlayer(IncomingMessageFromServer message)
    {
        OtherPlayerPackage otherPlayer = message.Read<OtherPlayerPackage>();
        SceneBuilder.CreateOtherPlayer(otherPlayer);
    }

    private static void ReceiveMessageUpdateItemInEquipment(IncomingMessageFromServer message)
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

    private static void ReceiveMessageUpdateEquipedItem(IncomingMessageFromServer message)
    {
        var item = message.Read<ItemInEquipmentPackage>();
        ItemEquipableType type = (ItemEquipableType)message.ReadInt();

        Item equipedItem = ItemInEquipmentPackageToItem(item);

        _netOwnPlayer.GetComponent<PlayerEquipement>().Equipe(equipedItem, type);

        _netOwnPlayer.GetComponent<PlayerGeneratorModel>().UpdateEquipedItems();

        GUIController.IsActiveCharacterGUIRefresh();
    }

    private static void ReceiveMessageNewPosition(IncomingMessageFromServer message)
    {
        int idNet = message.ReadInt();

        Vector3 position = message.ReadVector3();

        NetObject netObject = FindNetObjectByNetId(idNet);

        netObject.GetComponent<Movement>().SetNewTargetPosition(position);
    }

    private static void ReceiveMessageNewRotation(IncomingMessageFromServer message)
    {
        int idNet = message.ReadInt();
        float rotation = message.ReadFloat();

        NetObject netObject = FindNetObjectByNetId(idNet);

        netObject.GetComponent<Movement>().SetNewRotation(rotation);
    }

    private static void ReceiveMessageCreateBullet(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        BulletPackage bullet = message.Read<BulletPackage>();

        SceneBuilder.CreateBullet(bullet);      
    }

    private static void ReceiveMessageCreateMonster(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        MonsterPackage monster = message.Read<MonsterPackage>();

        SceneBuilder.CreateMonster(monster);
    }

    private static void ReceiveMessageCreateItemObject(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        ItemPackage item = message.Read<ItemPackage>();

        SceneBuilder.CreateItem(item);
    }

    private static void ReceiveMessageCreateVisualObject(IncomingMessageFromServer message)
    {
        var visualObjectPackage = message.Read<VisualObjectPackage>();

        SceneBuilder.CreateVisualObject(visualObjectPackage);
    }

    private static void ReceiveMessageDeleteObject(IncomingMessageFromServer message)
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

    private static void ReceiveMessageAttack(IncomingMessageFromServer message)
    {
        int idNetPlayer = message.ReadInt();

        NetOtherPlayer player = FindOtherPlayerByNetId(idNetPlayer);

        player.Combat.Attack();
    }

    private static void ReceiveMessageDead(IncomingMessageFromServer message)
    {
        OwnDeadEventPackage deadInfo = message.Read<OwnDeadEventPackage>();

        _netOwnPlayer.Dead(deadInfo);
    }

    private static void ReceiveMessageRespawn(IncomingMessageFromServer message)
    {
        Vector3 position = message.ReadVector3();

        _netOwnPlayer.transform.position = position;
    }

    private static void ReceiveMessageUpdateHP(IncomingMessageFromServer message)
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

    private static void ReceiveMessageUpdateAllStats(IncomingMessageFromServer message)
    {
        int netId = message.ReadInt();

        var netObject = FindNetObjectByNetId(netId);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Stats>().Set(message);
    }

    private static void ReceiveMessageUpdateEquipedItems(IncomingMessageFromServer message)
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

    private static void ReceiveMessageUpdateSpells(IncomingMessageFromServer message)
    {
        if (_netOwnPlayer == null)
        {
            BufferMessages.DelayExecutionMessage(message);
            return;
        }

        var spells = message.Read<ListPackage<SpellPackage>>();

        _netOwnPlayer.GetComponent<SpellCaster>().Set(spells);
    }

    private static void ReceiveMessageUpdateExp(IncomingMessageFromServer message)
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
            if (netObject.IdNet == netId)
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
            if (player.IdNet == netId)
            {
                return player;
            }
        }
        return null;
    }

    private static bool IfNewSceneLoadedDelayExecutionMessage(IncomingMessageFromServer message)
    {
        if (SceneBuilder.SceneIsLoadind())
        {
            BufferMessages.DelayExecutionMessage(message);
            return true;
        }

        return false;
    }

    private static bool IfNullDelayMessage(object flag, IncomingMessageFromServer message)
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
