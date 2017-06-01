using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;

public interface IEquipment
{
    int Gold { get; set; }
    void SetSlot(Item item, int idSlot);
    Item GetSlot(int idSlot);
}
