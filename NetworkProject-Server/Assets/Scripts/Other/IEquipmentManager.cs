using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Connection;
using NetworkProject.Items;

public interface IEquipmentManager
{
    Equipment GetEquipment();

    void SendUpdateSlot(int slot, IConnectionMember address);
}
