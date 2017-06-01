using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Quests;

public static class NPCRepository
{
    private static List<Action<GameObject>> _npcInitializators = new List<Action<GameObject>>();

    static NPCRepository()
    {
        _npcInitializators.Add(InitializeNPC_0_Normal);
        _npcInitializators.Add(InitializeNPC_1_GoldGuard_Map2);
        _npcInitializators.Add(InitializeNPC_2_Fisher);
        _npcInitializators.Add(InitializeNPC_3_GoldGuard_Map1);
        _npcInitializators.Add(InitializeNPC_4_GuardInstructor_Map1);
        _npcInitializators.Add(InitializeNPC_5_Merchant);
        _npcInitializators.Add(InitializeNPC_6_Sorcerer);
    }

    public static void InitalizeNPC(GameObject npc, int idNPC)
    {
        _npcInitializators[idNPC](npc);
    }

    private static void InitializeNPC_0_Normal(GameObject npc)
    {
        
    }

    private static void InitializeNPC_1_GoldGuard_Map2(GameObject npc)
    {
        var interlocutor = npc.AddComponent<Interlocutor>();
        interlocutor._conversationId = 4000;
    }

    private static void InitializeNPC_2_Fisher(GameObject npc)
    {
        var interlocutor = npc.AddComponent<Interlocutor>();
        interlocutor._conversationId = 0;
    }

    private static void InitializeNPC_3_GoldGuard_Map1(GameObject npc)
    {
        var interlocutor = npc.AddComponent<Interlocutor>();
        interlocutor._conversationId = 1000;
    }

    private static void InitializeNPC_4_GuardInstructor_Map1(GameObject npc)
    {
        var interlocutor = npc.AddComponent<Interlocutor>();
        interlocutor._conversationId = 2000;
    }

    private static void InitializeNPC_5_Merchant(GameObject npc)
    {
        var merchant = npc.AddComponent<Merchant>();
        merchant._shopId = 0;
    }

    private static void InitializeNPC_6_Sorcerer(GameObject npc)
    {
        var interlocutor = npc.AddComponent<Interlocutor>();
        interlocutor._conversationId = 3000;
    }
}
