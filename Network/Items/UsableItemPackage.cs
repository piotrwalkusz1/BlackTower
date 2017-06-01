using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Items
{
    [Serializable]
    public class UsableItemPackage : ItemDataPackage
    {
        public List<IUseActionPackage> Actions { get; set; }

        public UsableItemPackage(int idItem, IUseActionPackage[] actions)
            : base(idItem)
        {
            Actions = new List<IUseActionPackage>(actions);
        }
    }
}
