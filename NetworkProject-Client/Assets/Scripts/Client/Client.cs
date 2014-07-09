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
        _client = Standard.IoC.GetClient();
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
            ExecuteMessage(m);
        }
    }

    public static void ExecuteMessage(IncomingMessageFromServer message)
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

        GameObject player = SceneBuilder.CreateOwnPlayer(request);

        SetNetOwnPlayer(player.GetComponent<NetOwnPlayer>());
    }

    private static void ReceiveMessageMove(IncomingMessageFromServer message)
    {
        var request = (MoveToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Movement>().SetNewTargetPosition(request.NewPosition);
    }

    private static void ReceiveMessageRotate(IncomingMessageFromServer message)
    {
        var request = (RotateToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Movement>().SetNewRotation(request.NewRotation);
    }

    private static void ReceiveMessageJump(IncomingMessageFromServer message)
    {
        /*narazie nie używamy komponentu movement do skakania

        var request = (JumpToClient)message.Request;

        var netObject = GameObjectRepository.GetNetObjectByIdNet(request.IdNet);*/
    }

    private static void ReceiveMessageGoIntoWorld(IncomingMessageFromServer message)
    {
        var request = (GoIntoWorldToClient)message.Request;

        ApplicationControler.GoToWorld(request);
    }

    private static void ReceiveMessageCreateOtherPlayer(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        var request = (CreateOtherPlayerToClient)message.Request;

        SceneBuilder.CreateOtherPlayer(request);
    }

    private static void ReceiveMessageUpdateItemInEquipment(IncomingMessageFromServer message)
    {
        var request = (UpdateItemInEquipmentToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        IEquipment eq = (IEquipment)netObject.GetComponent(typeof(IEquipment));
        eq.SetSlot(request.Item, request.Slot);
    }

    private static void ReceiveMessageUpdateEquipedItem(IncomingMessageFromServer message)
    {
        var request = (UpdateEquipedItemToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        IEquiper eq = (IEquiper)netObject.GetComponent(typeof(IEquiper));

        eq.Equipe(request.Item, request.Slot);
    }

    private static void ReceiveMessageNewPosition(IncomingMessageFromServer message)
    {
        var request = (NewPositionToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.transform.position = request.Position;
    }

    private static void ReceiveMessageNewRotation(IncomingMessageFromServer message)
    {
        var request = (NewRotationToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.transform.eulerAngles = new Vector3(netObject.transform.position.x, request.Rotation, netObject.transform.position.z);
    }

    private static void ReceiveMessageCreateBullet(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        var request = (CreateBulletToClient)message.Request;

        SceneBuilder.CreateBullet(request);
    }

    private static void ReceiveMessageCreateMonster(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        var request = (CreateMonsterToClient)message.Request;

        SceneBuilder.CreateMonster(request);
    }

    private static void ReceiveMessageCreateItemObject(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        var request = (CreateItemToClient)message.Request;

        SceneBuilder.CreateItem(request);
    }

    private static void ReceiveMessageCreateVisualObject(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        var request = (CreateVisualObjectToClient)message.Request;

        SceneBuilder.CreateVisualObject(request);
    }

    private static void ReceiveMessageDeleteObject(IncomingMessageFromServer message)
    {
        var request = (DeleteObjectToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        SceneBuilder.DeleteObject(netObject.gameObject);
    }

    private static void ReceiveMessageAttack(IncomingMessageFromServer message)
    {
        var request = (AttackToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Combat>().Attack(request);
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

    private static void ReceiveMessageUpdateSpells(IncomingMessageFromServer message)
    {
        var request = (UpdateAllSpellsToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<SpellCaster>().SetSpells(request.Spells.ToArray());
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
