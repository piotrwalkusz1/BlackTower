
using System.Collections;

namespace SaveLoadSystem
{
    public interface ISavable
    {
        string ToSavableText();
        void SetByLoadedText(string loadedText);
    }
}

