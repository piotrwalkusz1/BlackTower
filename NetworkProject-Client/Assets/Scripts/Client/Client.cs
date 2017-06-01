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

    public static void Start()
    {
        _client = Standard.IoC.GetClient();
        _client.Start();
    }

    public static void Connect(string host, int port)
    {
        _client.Connect(host, port);
    }

    public static void Close()
    {
        _client.Close();
    }

    public static bool IsRunning()
    {
        return _client != null && _client.IsRunning();
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
        if (request is TextMessageToClient) return ReceiveMessageTextMessage;
        else if (request is ChatMessageToClient) return ReceiveMessageChatMessage;
        else if (request is GoToChoiceCharacterMenuToClient) return ReceiveMessageGoToChoiceCharacterMenu;
        else if (request is GoIntoWorldToClient) return ReceiveMessageGoIntoWorld;
        else if (request is InitializeAllQuestsToClient) return ReceiveMessageInitializeAllQuests;
        else if (request is CreateOwnPlayerToClient) return ReceiveMessageCreateOwnPlayer;
        else if (request is CreateOtherPlayerToClient) return ReceiveMessageCreateOtherPlayer;
        else if (request is CreateBulletToClient) return ReceiveMessageCreateBullet;
        else if (request is CreateItemToClient) return ReceiveMessageCreateItemObject;
        else if (request is CreateMonsterToClient) return ReceiveMessageCreateMonster;
        else if (request is CreateVisualObjectToClient) return ReceiveMessageCreateVisualObject;
        else if (request is CreateNPCToClient) return ReceiveMessageCreateNPC;
        else if (request is DeleteObjectToClient) return ReceiveMessageDeleteObject;
        else if (request is MoveToClient) return ReceiveMessageMove;
        else if (request is MoveYourCharacterToClient) return ReceiveMessageMoveYourCharacter;
        else if (request is ChangeMoveTypeToClient) return ReceiveMessageChangeMoveType;
        else if (request is RotateToClient) return ReceiveMessageRotate;
        else if (request is NewPositionToClient) return ReceiveMessageNewPosition;
        else if (request is UpdateItemInEquipmentToClient) return ReceiveMessageUpdateItemInEquipment;
        else if (request is UpdateEquipedItemToClient) return ReceiveMessageUpdateEquipedItem;
        else if (request is UpdateGoldToClient) return ReceiveMessageUpdateGold;
        else if (request is AttackToClient) return ReceiveMessageAttack;
        else if (request is DeadToClient) return ReceiveMessageDead;
        else if (request is RespawnToClient) return ReceiveMessageRespawn;
        else if (request is UpdateAllSpellsToClient) return ReceiveMessageUpdateAllSpells;
        else if (request is UpdateSpellToClient) return ReceiveMessageUpdateSpell;
        else if (request is UpdateAllStatsToClient) return ReceiveMessageUpdateAllStats;
        else if (request is UpdateHPToClient) return ReceiveMessageUpdateHP;
        else if (request is UpdateManaToClient) return ReceiveMessageUpdateMana;
        else if (request is UpdateExpToClient) return ReceiveMessageUpdateExp;
        else if (request is ChangeVisibilityModelToClient) return ReceiveMessageChangeVisibilityModelToClient;
        else if (request is GiveQuestToClient) return ReceiveMessageGiveQuest;
        else if (request is KillQuestUpdateToClient) return ReceiveMessageKillQuestUpdate;
        else if (request is QuestCompletedToClient) return ReceiveMessageQuestCompleted;
        else if (request is DeleteQuestToClient) return ReceiveMessageDeleteQuest;
        else if (request is ReturnQuestToClient) return ReceiveMessageReturnQuest;
        else if (request is TakeOverObjectToClient) return ReceiveMessageTakeOverObject;
        else if (request is OpenShopToClient) return ReceiveMessageOpenShop;
        else if (request is UpdateHotkeysToClient) return ReceiveMessageUpdateHotkeys;
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

    public static void ResetNetOwnPlayer()
    {
        _netOwnPlayer = null;
    }

    public static void SendRequestAsMessage(INetworkRequestToServer request)
    {
        var message = new OutgoingMessage(request);

        Send(message);
    }

    public static void Send(OutgoingMessage message)
    {
        _client.Send(message);
    }

    public static System.Collections.IEnumerator RefreshHostList(ConnectionMenuInfo info)
    {
        MasterServer.ClearHostList();
        MasterServer.RequestHostList(NetworkProject.Settings.gameName);

        HostData[] hostList;

        while(true)
        {
            hostList = MasterServer.PollHostList();

            if (hostList.Length == 0)
            {
                yield return null;
            }
            else
            {
                break;
            }
        }
        
        info.HostList = new List<HostInfo>();

        foreach (var host in hostList)
        {
            HostInfo hostInfo = new HostInfo();
            string[] ipAndPort = host.comment.Split(':');
            hostInfo.ExternalIP = ipAndPort[0];
            hostInfo.InternalIP = ipAndPort[1];
            hostInfo.Port = int.Parse(ipAndPort[2]);
            hostInfo.Name = host.gameName;

            info.HostList.Add(hostInfo);
        }
    }

    #region ToClient

    private static void ReceiveMessageChatMessage(IncomingMessageFromServer message)
    {
        var request = (ChatMessageToClient)message.Request;

        GUIController.ShowChatMessage(request.Message);
    }

    private static void ReceiveMessageTextMessage(IncomingMessageFromServer message)
    {
        var request = (TextMessageToClient)message.Request;

        string messageText = Languages.GetMessageText(request.IdMessage);

        GUIController.ShowMessage(messageText);
    }

    private static void ReceiveMessageGoToChoiceCharacterMenu(IncomingMessageFromServer message)
    {
        ApplicationControler.GoToChoiceCharacterMenu((GoToChoiceCharacterMenuToClient)message.Request);
    }

    private static void ReceiveMessageGoIntoWorld(IncomingMessageFromServer message)
    {
        var request = (GoIntoWorldToClient)message.Request;

        ApplicationControler.GoToWorld(request);
    }

    private static void ReceiveMessageInitializeAllQuests(IncomingMessageFromServer message)
    {
        var request = (InitializeAllQuestsToClient)message.Request;

        QuestRepository._quests = PackageConverter.PackageToQuest(request.Quests.ToArray());
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
        var request = (MoveToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Movement>().SetNewTargetPosition(request.NewPosition);
    }

    private static void ReceiveMessageMoveYourCharacter(IncomingMessageFromServer message)
    {
        if (IfNullDelayMessage(_netOwnPlayer, message))
        {
            return;
        }

        var request = (MoveYourCharacterToClient)message.Request;

        _netOwnPlayer.transform.position = request.NewPosition;

        var response = new ResponseMoveMyCharacterToServer();

        SendRequestAsMessage(response);
    }

    private static void ReceiveMessageChangeMoveType(IncomingMessageFromServer message)
    {
        var request = (ChangeMoveTypeToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.NetId);

        netObject.GetComponent<MonsterMovement>().MoveType = request.MoveType;
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

        IEquipment eq = (IEquipment)netObject.GetComponent(typeof(IEquipment));

        eq.SetSlot(PackageConverter.PackageToItem(request.Item), request.Slot);
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

        eq.Equip(PackageConverter.PackageToItem(request.Item), request.Slot);
    }

    private static void ReceiveMessageUpdateGold(IncomingMessageFromServer message)
    {
        var request = (UpdateGoldToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.NetId);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        var equipment = (IEquipment)netObject.GetComponent(typeof(IEquipment));

        equipment.Gold = request.Gold;

        GUIController.IfActiveEquipmentRefresh();
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

    private static void ReceiveMessageCreateNPC(IncomingMessageFromServer message)
    {
        if (IfNewSceneLoadedDelayExecutionMessage(message))
        {
            return;
        }

        var request = (CreateNPCToClient)message.Request;

        SceneBuilder.CreateNPC(request);
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
        var request = (DeadToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Health>().Dead();
    }

    private static void ReceiveMessageRespawn(IncomingMessageFromServer message)
    {
        var request = (RespawnToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.Respawn(request);
    }

    private static void ReceiveMessageUpdateHP(IncomingMessageFromServer message)
    {
        var request = (UpdateHPToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.NetId);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Health>().HP = request.HP;
    }

    private static void ReceiveMessageUpdateMana(IncomingMessageFromServer message)
    {
        var request = (UpdateManaToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.NetId);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<ManaInfo>().Mana = request.Mana;
    }

    private static void ReceiveMessageUpdateAllStats(IncomingMessageFromServer message)
    {
        var request = (UpdateAllStatsToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        var stats = (IStats)netObject.GetComponent(typeof(IStats));

        request.Stats.CopyToStats(stats);

        GUIController.IfActiveCharacterGUIRefresh();
    }

    private static void ReceiveMessageUpdateAllSpells(IncomingMessageFromServer message)
    {
        var request = (UpdateAllSpellsToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<SpellCaster>().SetSpells(PackageConverter.PackageToSpell(request.Spells.ToArray()).ToArray());
    }

    private static void ReceiveMessageUpdateSpell(IncomingMessageFromServer message)
    {
        var request = (UpdateSpellToClient)message.Request;

        Client.GetNetOwnPlayer().GetComponent<SpellCaster>().Spells.Add(PackageConverter.PackageToSpell(request.Spell));

        GUIController.IfActiveMagicBookRefresh();
    }

    private static void ReceiveMessageUpdateExp(IncomingMessageFromServer message)
    {
        var request = (UpdateExpToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if (IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.GetComponent<Experience>().Exp = request.Exp;
    }

    private static void ReceiveMessageChangeVisibilityModelToClient(IncomingMessageFromServer message)
    {
        var request = (ChangeVisibilityModelToClient)message.Request;

        var netObject = SceneBuilder.GetNetObjectByIdNet(request.IdNet);

        if(IfNullDelayMessage(netObject, message))
        {
            return;
        }

        netObject.IsModelVisible = request.IsVisibleModel;
    }

    private static void ReceiveMessageGiveQuest(IncomingMessageFromServer message)
    {
        var request = (GiveQuestToClient)message.Request;

        QuestRepository._quests.Find(x => x.IdQuest == request.IdQuest).Status = QuestStatus.InProgress;

        GUIController.IfActiveQuestManagerRefresh();

        GUIController.IfActiveDialogRefresh();
    }

    private static void ReceiveMessageKillQuestUpdate(IncomingMessageFromServer message)
    {
        var request = (KillQuestUpdateToClient)message.Request;

        var quest = QuestRepository.GetQuest(request.QuestId);
        var target = (KillQuestTarget)quest.GetTargets()[request.TargetId];

        target.AlreadyKill = request.Kills;

        GUIController.IfActiveQuestManagerRefresh();
    }

    private static void ReceiveMessageQuestCompleted(IncomingMessageFromServer message)
    {
        var request = (QuestCompletedToClient)message.Request;

        QuestRepository.GetQuest(request.QuestId).Status = QuestStatus.Completed;

        GUIController.IfActiveQuestManagerRefresh();

        GUIController.IfActiveDialogRefresh();
    }

    private static void ReceiveMessageDeleteQuest(IncomingMessageFromServer message)
    {
        var request = (DeleteQuestToClient)message.Request;

        QuestRepository.GetQuest(request.IdQuest).DeleteQuest();

        GUIController.IfActiveQuestManagerRefresh();

        GUIController.IfActiveDialogRefresh();
    }

    private static void ReceiveMessageReturnQuest(IncomingMessageFromServer message)
    {
        var request = (ReturnQuestToClient)message.Request;

        QuestRepository.GetQuest(request.IdQuest).Status = QuestStatus.Returned;

        GUIController.IfActiveQuestManagerRefresh();

        GUIController.IfActiveDialogRefresh();
    }

    private static void ReceiveMessageTakeOverObject(IncomingMessageFromServer message)
    {
        var request = (TakeOverObjectToClient)message.Request;

        NetObject netObject = SceneBuilder.GetNetObjectByIdNet(request.NetId);

        netObject.GetComponent<TakeOver>().Activate();
    }

    private static void ReceiveMessageOpenShop(IncomingMessageFromServer message)
    {
        var request = (OpenShopToClient)message.Request;

        GUIController.ShowShop(PackageConverter.PackageToShop(request.Shop), request.IdNetMerchant);
    }

    private static void ReceiveMessageUpdateHotkeys(IncomingMessageFromServer message)
    {
        var request = (UpdateHotkeysToClient)message.Request;

        GUIHotkeys.UpdateHotkeys(PackageConverter.PackageToHotkeys(request.Hotkeys.ToArray()).ToArray());
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
