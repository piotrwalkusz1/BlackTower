using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject.Connection.ToClient
{
    [Serializable]
    public class TextMessage : INetworkRequest
    {
        public string Text { get; set;  }

        public TextMessage(string text)
        {
            Text = text;
        }
    }
}
