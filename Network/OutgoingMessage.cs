using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using UnityEngine;
using NetworkProject.Connection;

namespace NetworkProject
{
    public class OutgoingMessage
    {
        IConnectionMember Address { get; set; }
        INetworkPackage Message { get; set; }

        public OutgoingMessage(INetworkPackage message, IConnectionMember address)
        {
            Message = message;
            Address = address;
        }
    }
}
