using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EditorExtension
{
    public class NPCConversations
    {
        public int IdNPC { get; set; }
        public int IdDefaultConversation { get; set; }
        public List<Conversation> Conversations { get; set; }

        public NPCConversations(int idNPC)
        {
            IdNPC = idNPC;

            Conversations = new List<Conversation>();
        }

        public bool IfMatchAddConversation(Conversation conversation)
        {
            if (IsConversationMatch(conversation))
            {
                Conversations.Add(conversation);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsConversationMatch(Conversation conversation)
        {
            return IdNPC == ConversationsWindow.GetIdNPCByIdConversation(conversation.IdConversation);
        }

        public void AddConversation()
        {
            int number;

            if (Conversations.Count == 0)
            {
                number = ConversationsWindow.MAX_CONVERSATIONS_IN_NPC * IdNPC;
            }
            else
            {
                number = Conversations.Max(x => x.IdConversation) + 1;
            }

            Conversations.Add(new Conversation(number, 0));
        }
    }
}
