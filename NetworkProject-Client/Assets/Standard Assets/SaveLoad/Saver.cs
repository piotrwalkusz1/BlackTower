
using System.Collections;

namespace SaveLoadSystem
{
    public class Saver
    {
        public string Path { get; set; }

        public void Save(ISavable objectToSave)
        {
            SaveLoad.Save(objectToSave, Path);
        }
    }
}

