using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OwnPlayerEquipment : PlayerEquipement
{
    private Equipment _equipment;

    public OwnPlayerEquipment()
    {
        _equipment = new Equipment();
    }
}
