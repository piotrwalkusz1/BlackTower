using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using UnityEngine;
using NetworkProject.Packages.Interfaces;

namespace NetworkProject
{
    public class OutgoingMessage : BytesStream
    {
        IConnectionMember Address { get; private set; }

        private List<byte> _data = new List<byte>();

        public OutgoingMessage(MessageToClientType messageType, IConnectionMember address)
        {
            Write((int)messageType);
            Address = address;
        }

        public OutgoingMessage(MessageToServerType messageType, IConnectionMember address)
        {
            Write((int)messageType);
            Address = address;
        }
    }
}
