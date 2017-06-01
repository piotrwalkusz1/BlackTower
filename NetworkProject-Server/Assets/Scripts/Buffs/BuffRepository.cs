using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NetworkProject.Buffs;
using UnityEngine;

class BuffRepository
{
    private static List<BuffData> _buffs;

    public static void LoadAndSetFromResources(string resourceName)
    {
        var textAsset = Resources.Load(resourceName, typeof(TextAsset)) as TextAsset;

        var stream = new MemoryStream(textAsset.bytes);

        var serializer = new BinaryFormatter();

        var buffs = (BuffDataPackage[])serializer.Deserialize(stream);

        _buffs = PackageConverter.PackageToBuffData(buffs);
    }

    public static BuffData GetBuff(int idBuff)
    {
        return _buffs.First(x => x.IdBuff == idBuff);
    }
}
