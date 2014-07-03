using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public class ListPackage<T> : INetworkPackage where T : INetworkPackage, new()
    {
        public List<T> List { get; set; }

        public ListPackage()
        {
            List = new List<T>();
        }

        public void Set(IncomingMessage message)
        {
            int length = message.ReadInt();

            for (int i = 0; i < length; i++)
            {
                List.Add(message.Read<T>());
            }
        }

        public byte[] ToBytes()
        {
            OutgoingMessage data = new OutgoingMessage();

            data.Write(List.Count);

            foreach (T item in List)
            {
                data.Write(item);
            }

            return data.Data.ToArray();
        }

        public T this[int index]
        {
            get
            {
                return List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        public void Add(T item)
        {
            List.Add(item);
        }

        public int Count
        {
            get
            {
                return List.Count;
            }
        }
    }  
}
