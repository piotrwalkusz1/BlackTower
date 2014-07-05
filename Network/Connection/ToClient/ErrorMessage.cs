using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkProject.Connection;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class ErrorMessage : INetworkRequest
    {
        public ErrorCode ErrorCode { get; set; }

        public ErrorMessage(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
