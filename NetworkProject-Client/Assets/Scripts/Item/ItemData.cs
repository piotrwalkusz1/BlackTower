using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Standard;
using NetworkProject.Items;

[Serializable]
public class ItemData
{
    public int IdItem { get; set; }
    public int IdTexture { get; set; }
    public int IdPrefabOnScene { get; set; }

    public ItemData()
    {

    }

    public ItemData(int idItem, int idTexture, int idPrefabOnScene)
    {
        IdItem = idItem;
        IdTexture = idTexture;
        IdPrefabOnScene = idPrefabOnScene;
    }

    public virtual ItemDataPackage ToPackage()
    {
        return new ItemDataPackage(IdItem);
    }

    public virtual string GetDescription()
    {
        return Languages.GetItemName(IdItem);
    }

    public virtual void UseItem(UseItemInfo info)
    {
        // empty
    }
}
