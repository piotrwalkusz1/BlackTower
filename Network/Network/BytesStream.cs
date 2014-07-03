using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Packages.Interfaces;


namespace NetworkProject
{
    public class BytesStream
    {
        protected List<byte> _data = new List<byte>();

        #region Constructors

        public BytesStream()
        {

        }

        public BytesStream(INetworkPackage networkPackage)
        {
            Write(networkPackage);
        }

        public BytesStream(int intMessage)
        {
            Write(intMessage);
        }

        public BytesStream(float floatMessage)
        {
            Write(floatMessage);
        }

        public BytesStream(char charMessage)
        {
            Write(charMessage);
        }

        public BytesStream(byte[] bytesMessage)
        {
            Write(bytesMessage);
        }

        public BytesStream(Vector3 vectorMessage)
        {
            Write(vectorMessage);
        }

        public BytesStream(string textMessage)
        {
            Write(textMessage);
        }

        public BytesStream(DateTime date)
        {
            Write(date);
        }

        public BytesStream(long longMessage)
        {
            Write(longMessage);
        }

        #endregion

        #region Write

        public void Write(INetworkPackage networkPackage)
        {
            Write(networkPackage.ToBytes());
        }

        public void Write(int intMessage)
        {
            byte[] bytes = BitConverter.GetBytes(intMessage);
            _data.AddRange(bytes);
        }

        public void Write(float floatMessage)
        {
            byte[] bytes = BitConverter.GetBytes(floatMessage);
            _data.AddRange(bytes);
        }

        public void Write(char charMessage)
        {
            byte[] bytes = BitConverter.GetBytes(charMessage);
            _data.AddRange(bytes);
        }

        public void Write(byte[] bytesMessage)
        {
            _data.AddRange(bytesMessage);
        }

        public void Write(Vector3 vectorMessage)
        {
            Write(vectorMessage.x);
            Write(vectorMessage.y);
            Write(vectorMessage.z);
        }

        public void Write(string textMessage)
        {
            char length = (char)textMessage.Length;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(textMessage);
            Write(length);
            Write(bytes);
        }

        public void Write(DateTime date)
        {
            Write(date.Ticks);
        }

        public void Write(long longMessage)
        {
            byte[] bytes = BitConverter.GetBytes(longMessage);
            _data.AddRange(bytes);
        }

        #endregion

        public byte[] GetBytes()
        {
            return _data.ToArray();
        }
    }
}
