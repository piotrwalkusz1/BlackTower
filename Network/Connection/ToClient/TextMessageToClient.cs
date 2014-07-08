using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class TextMessageToClient : INetworkRequest
    {
        public string Text { get; set;  }

        public TextMessageToClient(string text)
        {
            Text = text;
        }
    }
}
