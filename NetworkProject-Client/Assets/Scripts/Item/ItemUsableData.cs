using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using NetworkProject.Connection.ToServer;

[Serializable]
public class ItemUsableData : ItemData
{
    public List<IUseAction> Actions { get; set; }

    public ItemUsableData()
    {
        Actions = new List<IUseAction>();
    }

    public ItemUsableData(int idItem, int idTexture, int idPrefabOnScene, IUseAction[] actions)
        : base(idItem, idTexture, idPrefabOnScene)
    {
        Actions = new List<IUseAction>(actions);
    }

    public override ItemDataPackage ToPackage()
    {
        return new UsableItemPackage(IdItem, PackageConverter.UseActionToPackage(Actions.ToArray()).ToArray());
    }

    public override void UseItem(UseItemInfo info)
    {
        var request = new UseItemToServer(info.Slot);

        Client.SendRequestAsMessage(request);
    }
}
