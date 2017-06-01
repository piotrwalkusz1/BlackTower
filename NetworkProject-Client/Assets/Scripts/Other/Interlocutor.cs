using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Interlocutor : MonoBehaviour
{
    public int _conversationId;

    void OnMouseDown()
    {
        var netOwnPlayer = Client.GetNetOwnPlayer();

        Vector3 offset = netOwnPlayer.transform.position - transform.position;

        if (offset.sqrMagnitude <= NetworkProject.Settings.talkNPCRange * NetworkProject.Settings.talkNPCRange)
        {
            StartTalk();
        } 
    }

    public void StartTalk()
    {
        GUIController.ShowDialogWindow(ConversationRepository.GetConversation(_conversationId), this);
    }
}
