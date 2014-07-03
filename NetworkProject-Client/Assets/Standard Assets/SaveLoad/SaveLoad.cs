using UnityEngine;
using System.Collections;
using System.IO;

namespace SaveLoadSystem
{
    static public class SaveLoad
    {
        static public void Save(ISavable objectToSave, string path)
        {
            string content = objectToSave.ToSavableText();
            Write(content, path);
        }

        static public T Load<T>(string path)
            where T : ISavable, new()
        {
            string content = Read(path);
            T loadedObject = new T();
            loadedObject.SetByLoadedText(content);
            return loadedObject;
        }

        static public string Read(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        static public void Write(string content, string path, bool append = false)
        {
            using (StreamWriter writer = new StreamWriter(path, append))
            {
                writer.Write(content);
                writer.Flush();
            }
        }
    }
}

