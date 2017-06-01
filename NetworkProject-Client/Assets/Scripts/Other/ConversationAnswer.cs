using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class ConversationAnswer
{
    public int IdDialog { get; set; }
    public int IdNextConversation { get; set; }

    public ConversationAnswer(int idDialog, int idNextConversation)
    {
        IdDialog = idDialog;
        IdNextConversation = idNextConversation;
    }

    public virtual void GiveAnswer()
    {
        if (IdNextConversation == -1)
        {
            GUIController.HideDialogWindow();
        }
        else
        {
            GUIController.ShowDialogWindow(ConversationRepository.GetConversation(IdNextConversation), GUIController.CurrentInterlocutor);
        }
    }

    public virtual bool IsShowAnswer()
    {
        return true;
    }
}
