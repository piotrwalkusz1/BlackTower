using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Connection;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class ErrorMessageToClient : INetworkRequestToClient
    {
        public ErrorCode ErrorCode { get; set; }

        public ErrorMessageToClient(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
