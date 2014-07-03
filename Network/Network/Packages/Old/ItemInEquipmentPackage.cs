using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class ItemInEquipmentPackage : INetworkPackage
    {
        public int IdItem { get; set; }

        public ItemInEquipmentPackage()
        {

        }

        public ItemInEquipmentPackage(int i)
        {
            IdItem = i;
        }

        public void Set(IncomingMessage message)
        {
            IdItem = message.ReadInt();
        }

        public byte[] ToBytes()
        {
            var data = new OutgoingMessage();

            data.Write(IdItem);

            return data.Data.ToArray();
        }
    }
}
