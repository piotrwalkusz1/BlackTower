using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;
using NetworkProject.Connection.ToClient;
using NetworkProject.Quests;

public class QuestExecutor : MonoBehaviour, IRewardable
{
    protected List<Quest> _currentQuests = new List<Quest>();
    protected List<int> _returnedQuests = new List<int>();

    private NetPlayer _netPlayer;

    void Awake()
    {
        _netPlayer = GetComponent<NetPlayer>();
    }

    void Update()
    {
        foreach (var quest in _currentQuests)
        {
            quest.IsCompleteSendMessage(_netPlayer.OwnerAddress);
        }
    }

    public void TakeQuest(int idQuest, int idNetQuester)
    {
        if(_currentQuests.Exists(x => x.IdQuest == idQuest))
        {
            throw new InvalidOperationException("Ta osoba ma już ten quest.");
        }

        if (_returnedQuests.Exists(x => x == idQuest))
        {
            throw new InvalidOperationException("Ta osoba skończyła już ten quest.");
        }

        var quest = new Quest(idQuest, idNetQuester);

        _currentQuests.Add(quest);

        quest.Initialize(gameObject);
    }

    public void InitializeAllQuests()
    {
        _currentQuests.ForEach(x => x.Initialize(gameObject));
    }

    public void DeleteQuest(Quest quest)
    {
        _currentQuests.Remove(quest);

        quest.Dispose(gameObject);
    }

    public bool IsQuestCompleted(int idQuest)
    {
        return _currentQuests.First(x => x.IdQuest == idQuest).IsComplete();
    }

    public bool IsQuestReturned(int idQuest)
    {
        return _returnedQuests.Exists(x => x == idQuest);
    }

    public void ReturnQuest(int idQuest)
    {
        var quest = _currentQuests.First(x => x.IdQuest == idQuest);

        GiveRewardAndSendUpdate(quest.QuestData);

        ReturnQuest(quest);

        Server.SendRequestAsMessage(new ReturnQuestToClient(idQuest), _netPlayer.OwnerAddress);
    }

    public void SetQuests(Quest[] currentQuests, int[] returnedQuests)
    {
        _currentQuests = new List<Quest>(currentQuests);
        _returnedQuests = new List<int>(returnedQuests);
    }

    public bool IsQuestExecuting(int idQuest)
    {
        return _currentQuests.Exists(x => x.IdQuest == idQuest);
    }

    public Quest GetCurrentQuest(int idQuest)
    {
        return _currentQuests.First(x => x.IdQuest == idQuest);
    }

    public void AddExp(int exp)
    {
        GetComponent<Experience>().AddExp(exp);
    }

    public void AddGold(int gold)
    {
        GetComponent<PlayerEquipment>().Gold += gold;
    }

    public int AddItem(Item item)
    {
        return GetComponent<PlayerEquipment>().AddItem(item);
    }

    public void SendQuestsInitialize()
    {
        List<QuestDataPackage> quests = PackageConverter.QuestDataToPackage(QuestRepository._quests.ToArray());

        foreach (var currentQuest in _currentQuests)
        {
            var quest = quests.Find(x => x.IdQuest == currentQuest.IdQuest);
            quest.NetIdQuester = currentQuest.NetIdQuester;
            quest.QuestStatus = currentQuest.IsComplete() ? QuestStatusPackage.Completed : QuestStatusPackage.InProgress;
            quest.SetTargets(PackageConverter.TargetToPackage(currentQuest.GetTargets()).ToArray());
        }

        foreach (var completedQuest in _returnedQuests)
        {
            quests.Find(x => x.IdQuest == completedQuest).QuestStatus = QuestStatusPackage.Returned;
        }

        var request = new InitializeAllQuestsToClient(quests.ToArray());

        Server.SendRequestAsMessage(request, GetComponent<NetPlayer>().OwnerAddress);
    }

    public bool CanTakeRewardForQuest(int idQuest)
    {
        Quest quest = _currentQuests.First(x => x.IdQuest == idQuest);

        var itemRewards = from reward in quest.QuestData.Rewards
                          where reward is ItemReward
                          select reward;

        return GetComponent<PlayerEquipment>().AreEmptyBagSlots(itemRewards.ToArray().Length);
    }

    public Quest[] GetCurrentQuests()
    {
        return _currentQuests.ToArray();
    }

    public int[] GetReturnedQuest()
    {
        return _returnedQuests.ToArray();
    }

    protected void ReturnQuest(Quest quest)
    {
        _currentQuests.Remove(quest);

        quest.Dispose(gameObject);

        _returnedQuests.Add(quest.IdQuest);
    }

    protected void GiveRewardAndSendUpdate(QuestData quest)
    {
        quest.GiveRevard(this);

        var sender = new RewardSenderUpdate();

        quest.SelectChangesByRewards(sender);

        sender.SendUpdate(gameObject, GetComponent<NetPlayer>().OwnerAddress);
    }
}
