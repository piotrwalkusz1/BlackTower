using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using UnityEngine;
using NetworkProject.Connection;

namespace NetworkProject
{
    public class IncomingMessage
    {
        IConnectionMember Sender { get; set; }
        INetworkPackage Message { get; set; }

        public IncomingMessage(INetworkPackage message, IConnectionMember address)
        {
            Message = message;
            Sender = address;
        }
    }
}
