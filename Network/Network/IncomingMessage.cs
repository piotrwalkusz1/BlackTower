using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using UnityEngine;
using NetworkProject.Packages.Interfaces;

namespace NetworkProject
{
    public class IncomingMessage : BytesStream
    {
        public IConnectionMember Sender { get; set; }

        private int _index = 0;

        public IncomingMessage(IncomingMessage message)
        {
            Sender = message.Sender;
            _index = message.GetIndex();
            _data = new List<byte>(message.GetBytes());
        }

        public IncomingMessage(byte[] data)
        {
            _data = new List<byte>(data);
        }

        public IncomingMessage(byte[] data, IConnectionMember sender)
        {
            _data = new List<byte>(data);
            Sender = sender;
        }

        public void SetIndex(int newIndex)
        {
            _index = newIndex;
        }

        public T Read<T>()
            where T : INetworkPackage, new()
        {
            T networkPackage = new T();
            networkPackage.Set(this);
            return networkPackage;
        }

        public int ReadInt()
        {
            int info = BitConverter.ToInt32(_data.ToArray(), _index);
            _index += 4;
            return info;
        }

        public float ReadFloat()
        {
            float info = BitConverter.ToSingle(_data.ToArray(), _index);
            _index += 4;
            return info;
        }

        public char ReadChar()
        {
            char info = BitConverter.ToChar(_data.ToArray(), _index);
            _index += 2;
            return info;
        }

        public byte[] ReadAllBytes()
        {
            int numberRemainingBytes = _data.Count - _index;
            return ReadBytes(numberRemainingBytes);
        }

        public byte[] ReadBytes(int number)
        {
            byte[] info = new byte[number];
            for (int i = 0; i < number; i++)
            {
                info[i] = _data[i + _index];
            }
            _index += number;
            return info;
        }

        public Vector3 ReadVector3()
        {
            float x = ReadFloat();
            float y = ReadFloat();
            float z = ReadFloat();
            return new Vector3(x, y, z);
        }

        public string ReadString()
        {
            char length = ReadChar();
            byte[] info = ReadBytes(length);
            string text = System.Text.Encoding.UTF8.GetString(info);
            return text;
        }

        public DateTime ReadDateTime()
        {
            long ticks = ReadLong();

            //DateTime defaultTime = OutgoingMessage.DEFAULT_DATA_TIME;

            //return defaultTime.AddSeconds(totalSecounds);

            return new DateTime(ticks);
        }

        public long ReadLong()
        {
            long info = BitConverter.ToInt64(_data.ToArray(), _index);
            _index += 8;
            return info;
        }

        public int GetIndex()
        {
            return _index;
        }
    }
}
