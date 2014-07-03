using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;
using UnityEngine;

namespace UnitTest.ServerTest
{
    [TestClass]
    public class OutgoingMessageTest
    {
        [TestMethod]
        public void Write_Int()
        {
            OutgoingMessage message = new OutgoingMessage();

            int i = 2402421;
            message.Write(i);

            byte[] actual = message.Data.ToArray();
            byte[] expected = BitConverter.GetBytes(i);

            Assert.AreEqual(4, actual.Length);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Write_Float()
        {
            OutgoingMessage message = new OutgoingMessage();

            float i = -2402421.2145f;
            message.Write(i);

            byte[] actual = message.Data.ToArray();
            byte[] expected = BitConverter.GetBytes(i);

            Assert.AreEqual(4, actual.Length);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Write_Char()
        {
            OutgoingMessage message = new OutgoingMessage();

            char i = 'f';
            message.Write(i);

            byte[] actual = message.Data.ToArray();
            byte[] expected = BitConverter.GetBytes(i);

            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        [TestMethod]
        public void Write_Bytes()
        {
            OutgoingMessage message = new OutgoingMessage();

            byte[] i = { 0xff, 0xaa, 0x21 };
            message.Write(i);

            byte[] actual = message.Data.ToArray();

            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(i[0], actual[0]);
            Assert.AreEqual(i[1], actual[1]);
            Assert.AreEqual(i[2], actual[2]);
        }

        [TestMethod]
        public void Write_IntAndChar()
        {
            OutgoingMessage message = new OutgoingMessage();

            int i = -4629;
            char c = 'f';
            message.Write(i);
            message.Write(c);

            byte[] actual = message.Data.ToArray();
            byte[] expected1 = BitConverter.GetBytes(i);
            byte[] expected2 = BitConverter.GetBytes(c);

            Assert.AreEqual(6, actual.Length);
            Assert.AreEqual(expected1[0], actual[0]);
            Assert.AreEqual(expected1[1], actual[1]);
            Assert.AreEqual(expected1[2], actual[2]);
            Assert.AreEqual(expected1[3], actual[3]);
            Assert.AreEqual(expected2[0], actual[4]);
            Assert.AreEqual(expected2[1], actual[5]);
        }

        [TestMethod]
        public void WriteAndReadVector3()
        {
            OutgoingMessage message = new OutgoingMessage();
            message.Write(new Vector3(1, 2, 3));
            byte[] bytes = message.Data.ToArray();

            IncomingMessage m = new IncomingMessage(bytes);
            Vector3 readVector3 = m.ReadVector3();

            Assert.AreEqual(1, readVector3.x);
            Assert.AreEqual(2, readVector3.y);
            Assert.AreEqual(3, readVector3.z);
        }
    }
}
