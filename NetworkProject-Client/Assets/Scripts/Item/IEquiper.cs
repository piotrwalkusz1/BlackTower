using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using NetworkProject.BodyParts;

public interface IEquiper
{
    void Equip(Item item, int bodyPart);
}
