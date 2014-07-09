using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Items;
using NetworkProject.BodyParts;

public interface IEquiper
{
    void Equipe(Item item, int bodyPart);
}
