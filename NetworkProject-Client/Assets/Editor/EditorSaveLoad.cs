using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.Spells;
using NetworkProject.Monsters;
using NetworkProject.Buffs;
using UnityEngine;

namespace EditorExtension
{
    public static class EditorSaveLoad
    {
        public static readonly string ITEMS_CLIENT = Application.dataPath + "/Resources/items.txt";
        public static readonly string MONSTERS_CLIENT = Application.dataPath + "/Resources/monsters.txt";
        public static readonly string SPELLS_CLIENT = Application.dataPath + "/Resources/spells.txt";
        public static readonly string BUFFS_CLIENT = Application.dataPath + "/Resources/buffs.txt";
        public static readonly string CONVERSATIONS_CLIENT = Application.dataPath + "/Resources/conversations.txt";
        public static readonly string QUESTS_CLIENT = Application.dataPath + "/Resources/quests.txt";

        private static readonly string ITEMS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/items.txt";
        private static readonly string MONSTERS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/monsters.txt";
        private static readonly string SPELLS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/spells.txt";
        private static readonly string BUFFS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/buffs.txt";
        private static readonly string QUESTS_SERVER = "C:/Users/Piotr/Source/Repos/NetworkProject/NetworkProject-Server/Assets/Resources/quests.txt";

        public static void SaveMonsters(List<MonsterMultiData> monsters)
        {
            var monstersInClient = from monster in monsters
                                   select monster.MonsterClientVersion;

            Save(monstersInClient.ToList().ConvertAll<MonsterData>(x => (MonsterData)x).ToArray(), MONSTERS_CLIENT);

            var monstersInServer = from monster in monsters
                                   select monster.MonsterServerVersion;

            Save(monstersInServer.ToList().ConvertAll<MonsterData>(x => (MonsterData)x).ToArray(), MONSTERS_SERVER);
        }

        public static void SaveItems(List<ItemData> items)
        {
            Save(items.ToArray(), ITEMS_CLIENT);

            Save(PackageConverter.ItemDataToPackage(items.ToArray()).ToArray(), ITEMS_SERVER);
        }

        public static void SaveSpells(List<SpellData> spells)
        {
            Save(spells.ToArray(), SPELLS_CLIENT);

            Save(PackageConverter.SpellDataToPackage(spells.ToArray()).ToArray(), SPELLS_SERVER);
        }

        public static void SaveBuffs(List<BuffData> buffs)
        {
            Save(buffs.ToArray(), BUFFS_CLIENT);

            Save(PackageConverter.BuffDataToPackage(buffs.ToArray()).ToArray(), BUFFS_SERVER);
        }

        public static void SaveConversations(List<Conversation> conversations)
        {
            Save(conversations.ToArray(), CONVERSATIONS_CLIENT);
        }

        public static List<MonsterMultiData> LoadMonsters()
        {
            try
            {
                var monstersInClient = (MonsterData[])Load(MONSTERS_CLIENT);

                var monstersInServer = (MonsterData[])Load(MONSTERS_SERVER);

                var monsters = new List<MonsterMultiData>();

                for (int i = 0; i < monstersInClient.Length; i++)
                {
                    monsters.Add(new MonsterMultiData((VisualMonsterData)monstersInClient[i], (MonsterFullData)monstersInServer[i]));
                }

                return monsters;
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);

                return new List<MonsterMultiData>();
            }
        }

        public static List<ItemData> LoadItems()
        {
            try
            {
                return new List<ItemData>((ItemData[])Load(ITEMS_CLIENT));
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);

                return new List<ItemData>();
            }
        }

        public static List<SpellData> LoadSpells()
        {
            try
            {
                return new List<SpellData>((SpellData[])Load(SPELLS_CLIENT));
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);

                return new List<SpellData>();
            }
        }

        public static List<BuffData> LoadBuffs()
        {
            try
            {
                return new List<BuffData>((BuffData[])Load(BUFFS_CLIENT));
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);

                return new List<BuffData>();
            }
        }

        public static List<Conversation> LoadConversation()
        {
            try
            {
                return new List<Conversation>((Conversation[])Load(CONVERSATIONS_CLIENT));
            }
            catch (Exception ex)
            {
                MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);

                return new List<Conversation>();
            }
        }

        public static void Save(object obj, string path)
        {
            var serializer = new BinaryFormatter();

            var bufferStream = new MemoryStream();

            serializer.Serialize(bufferStream, obj);

            using (var stream = new BinaryWriter(File.OpenWrite(path)))
            {
                stream.Write(bufferStream.ToArray());
            }
        }

        public static object Load(string path)
        {
            var serializer = new BinaryFormatter();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream);
            }
        }
    }
}
