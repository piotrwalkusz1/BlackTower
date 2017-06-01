using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace EditorExtension
{
    public class ConversationsWindow : EditorWindow
    {
        public const int MAX_CONVERSATIONS_IN_NPC = 1000;
        public static List<NPCConversations> ConversationsByNPC { get; set; }

        private Vector2 _scroll;
        private NPCConversations _chosenNPC;
        private Conversation _chosenConversation;

        private NPCConversations _npcToDelete;
        private Conversation _conversationToDelete;
        private ConversationAnswer _answerToDelete;
        private ConversationAnswer _answerToTop;
        private Conversation _answerToTopConversation;
        private int _idNPCToAdd;

        [MenuItem("Extension/Conversations")]
        static void ShowWindow()
        {
            var window = EditorWindow.GetWindow(typeof(ConversationsWindow)) as ConversationsWindow;
            window.Start();
        }

        public void Start()
        {
            ConversationsByNPC = new List<NPCConversations>();

            List<Conversation> conversations = EditorSaveLoad.LoadConversation();

            bool found = false;

            foreach (var conversation in conversations)
            {  
                found = false;

                foreach (var npc in ConversationsByNPC)
                {
                    if (npc.IfMatchAddConversation(conversation))
                    {
                        found = true;

                        break;
                    }
                }

                if (!found)
                {
                    var newNPCConversations = new NPCConversations(GetIdNPCByIdConversation(conversation.IdConversation));

                    ConversationsByNPC.Add(newNPCConversations);

                    newNPCConversations.Conversations.Add(conversation);
                }
            }
        }

        public static int GetIdNPCByIdConversation(int idConversation)
        {
            return idConversation / MAX_CONVERSATIONS_IN_NPC;
        }

        public static string GetDialog(int idDialog)
        {
            return LanguagesWindow.LanguagesList[0].Language.GetDialog(idDialog);
        }

        void OnGUI()
        {
            ShowContextMenu();

            DeleteChecked();

            OnTopChecked();

            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            ShowAddNPC();

            foreach (var npc in ConversationsByNPC)
            {
                ShowNPC(npc);
            }

            ShowNPCConversationsEditor();

            ShowSave();

            EditorGUILayout.EndScrollView();
        }

        private void ShowAddNPC()
        {
            _idNPCToAdd = EditorGUILayout.IntField("NPC id to add", _idNPCToAdd);

            if (GUILayout.Button("Add NPC"))
            {
                var newNPC = new NPCConversations(_idNPCToAdd);

                newNPC.AddConversation();

                ConversationsByNPC.Add(newNPC);
            }
        }

        private void ShowNPC(NPCConversations npc)
        {
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Choose npc : " + npc.IdNPC.ToString(), GUILayout.MinWidth(50)))
            {
                _chosenNPC = npc;

                _chosenConversation = _chosenNPC.Conversations.FirstOrDefault(x => x.IdConversation == _chosenNPC.IdDefaultConversation);
            }

            if (GUILayout.Button("Delete npc : " + npc.IdNPC.ToString(), GUILayout.MinWidth(50)))
            {
                _npcToDelete = npc;
            }

            EditorGUILayout.EndHorizontal();
        }

        private void ShowNPCConversationsEditor()
        {
            if (_chosenNPC != null)
            {
                EditorGUILayout.LabelField("Chosen npc : " + _chosenNPC.IdNPC);

                foreach (var conversation in _chosenNPC.Conversations)
                {
                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button("Choose " + conversation.IdConversation.ToString()))
                    {
                        _chosenConversation = conversation;
                    }

                    if (GUILayout.Button("Delete" + conversation.IdConversation.ToString()))
                    {
                        _conversationToDelete = conversation;
                    }

                    EditorGUILayout.EndHorizontal();
                }

                if (_chosenConversation != null)
                {
                    _chosenConversation.IdMainDialog = EditorGUILayout.IntField("Dialog id", _chosenConversation.IdMainDialog);

                    GUIStyle guiStyle = new GUIStyle(GUI.skin.box);
                    guiStyle.alignment = TextAnchor.UpperLeft;

                    GUILayout.Box(GetDialog(_chosenConversation.IdMainDialog), guiStyle);

                    foreach (var answer in _chosenConversation.GetAnswers())
                    {
                        ShowAnswer(answer, _chosenConversation);
                    }

                    if (GUILayout.Button("Delete conversation"))
                    {
                        _conversationToDelete = _chosenConversation;
                    }
                }
            }
        }

        private void ShowContextMenu()
        {
            Event evt = Event.current;

            if (evt.type == EventType.ContextClick)
            {
                var menu = new GenericMenu();

                menu.AddItem(new GUIContent("Add conversation"), false, delegate() { _chosenNPC.AddConversation(); });
                menu.AddItem(new GUIContent("Add answer"), false, delegate() { _chosenConversation.AddAnswer(new ConversationAnswer(0, 0)); });
                menu.AddItem(new GUIContent("Add answer take quest"), false, delegate() { _chosenConversation.AddAnswer(new ConversationAnswerQuest(0, 0, 0)); });
                menu.AddItem(new GUIContent("Add answer return quest"), false, delegate() { _chosenConversation.AddAnswer(new ConversationAnswerReturnQuest(0, 0, 0)); });
                menu.AddItem(new GUIContent("Add answer if can take quest"), false, delegate() { _chosenConversation.AddAnswer(new ConversationAnswerIfCanTakeQuest(0, 0, 0)); });
                menu.AddItem(new GUIContent("Add answer action"), false, delegate() { _chosenConversation.AddAnswer(new ConversationAnswerAction(0, 0, 0)); });

                menu.ShowAsContext();

                evt.Use();
            }
        }

        private void ShowAnswer(ConversationAnswer answer, Conversation conversation)
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(GetDialog(answer.IdDialog)))
            {
                _chosenConversation = _chosenNPC.Conversations.FirstOrDefault(x => x.IdConversation == answer.IdNextConversation);
            }

            answer.IdDialog = EditorGUILayout.IntField("Dialog id", answer.IdDialog);
            answer.IdNextConversation = EditorGUILayout.IntField("Next conversation id", answer.IdNextConversation);

            if (answer is ConversationAnswerQuest)
            {
                var a = (ConversationAnswerQuest)answer;
                a.QuestId = EditorGUILayout.IntField("Take quest id", a.QuestId);
            }
            else if (answer is ConversationAnswerReturnQuest)
            {
                var a = (ConversationAnswerReturnQuest)answer;
                a.QuestId = EditorGUILayout.IntField("Return quest id", a.QuestId);
            }
            else if (answer is ConversationAnswerIfCanTakeQuest)
            {
                var a = (ConversationAnswerIfCanTakeQuest)answer;
                a.QuestId = EditorGUILayout.IntField("Can take quest id", a.QuestId);
            }
            else if (answer is ConversationAnswerAction)
            {
                var a = (ConversationAnswerAction)answer;
                a.ActionId = EditorGUILayout.IntField("Action id", a.ActionId);
            }

            if (GUILayout.Button("Delete answer"))
            {
                _answerToDelete = answer;
            }

            if (GUILayout.Button("", GUILayout.Width(20)))
            {
                _answerToTop = answer;
                _answerToTopConversation = conversation;
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DeleteChecked()
        {
            if (_npcToDelete != null)
            {
                ConversationsByNPC.Remove(_npcToDelete);
                _chosenNPC = null;
                _chosenConversation = null;
            }
            else if (_conversationToDelete != null)
            {
                _chosenNPC.Conversations.Remove(_conversationToDelete);
                _chosenConversation = null;
            }
            else if (_answerToDelete != null)
            {
                _chosenConversation.RemoveAnswer(_answerToDelete);
            }
        }

        private void OnTopChecked()
        {
            if (_answerToTop != null)
            {
                _answerToTopConversation.AnswerToTop(_answerToTop);

                _answerToTop = null;
                _answerToTopConversation = null;
            }
        }

        private void ShowSave()
        {
            if (GUILayout.Button("Save"))
            {
                var conversations = from npc in ConversationsByNPC
                                    from conversation in npc.Conversations
                                    select conversation;

                EditorSaveLoad.SaveConversations(conversations.ToList());
            }
        }
    }
}
