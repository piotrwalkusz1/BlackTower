using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class BuffRepository
{
    private static List<BuffData> _buffs;

    public static void LoadAndSetFromResources(string resourceName)
    {
        var textAsset = Resources.Load(resourceName, typeof(TextAsset)) as TextAsset;

        var stream = new MemoryStream(textAsset.bytes);

        var serializer = new BinaryFormatter();

        var buffs = (BuffData[])serializer.Deserialize(stream);

        _buffs = new List<BuffData>(buffs);
    }

    public static BuffData GetBuff(int idBuff)
    {
        return _buffs.First(x => x.IdBuff == idBuff);
    }
}
