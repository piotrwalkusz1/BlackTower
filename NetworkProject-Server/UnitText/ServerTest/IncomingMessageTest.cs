using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;

namespace UnitTest.ServerTest
{
    [TestClass]
    public class IncomingMessageTest
    {
        [TestMethod]
        public void ReadInt()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0, 0xaa, 0x18 };
            var message = new IncomingMessage(data, null);

            int i1 = message.ReadInt();
            int i2 = message.ReadInt();

            byte[][] assert =
            {
                new byte[] { 0x11, 0xff, 0x12, 0x47 },
                new byte[] { 0xaf, 0xf0, 0xaa, 0x18 }
            };

            Assert.AreEqual(BitConverter.ToInt32(assert[0], 0), i1);
            Assert.AreEqual(BitConverter.ToInt32(assert[1], 0), i2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadInt_Exception()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0, 0xaa, 0x18, 0xda };
            var message = new IncomingMessage(data, null);

            int i1 = message.ReadInt();
            int i2 = message.ReadInt();
            int i3 = message.ReadInt();
        }

        [TestMethod]
        public void ReadFloat()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0, 0xaa, 0x18 };
            var message = new IncomingMessage(data, null);

            float f1 = message.ReadFloat();
            float f2 = message.ReadFloat();

            byte[][] assert =
            {
                new byte[] { 0x11, 0xff, 0x12, 0x47 },
                new byte[] { 0xaf, 0xf0, 0xaa, 0x18 }
            };

            Assert.AreEqual(BitConverter.ToSingle(assert[0], 0), f1);
            Assert.AreEqual(BitConverter.ToSingle(assert[1], 0), f2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadFloat_Exception()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0, 0xaa };
            var message = new IncomingMessage(data, null);

            float f1 = message.ReadFloat();
            float f2 = message.ReadFloat();

        }

        [TestMethod]
        public void ReadChar()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0 };
            var message = new IncomingMessage(data, null);

            char c1 = message.ReadChar();
            char c2 = message.ReadChar();
            char c3 = message.ReadChar();

            byte[][] assert =
            {
                new byte[] { 0x11, 0xff },
                new byte[] { 0x12, 0x47 },
                new byte[] { 0xaf, 0xf0 }
            };

            Assert.AreEqual(BitConverter.ToChar(assert[0], 0), c1);
            Assert.AreEqual(BitConverter.ToChar(assert[1], 0), c2);
            Assert.AreEqual(BitConverter.ToChar(assert[2], 0), c3);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReadChar_Exception()
        {
            byte[] data = { 0x11, 0xff, 0x12 };
            var message = new IncomingMessage(data, null);

            char c1 = message.ReadChar();
            char c2 = message.ReadChar();
        }

        [TestMethod]
        public void ReadAllBytes()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0 };
            var message = new IncomingMessage(data, null);

            byte[] b1 = message.ReadBytes(3);
            byte[] b2 = message.ReadAllBytes();

            Assert.AreEqual(3, b2.Length);
            Assert.AreEqual(data[0], b1[0]);
            Assert.AreEqual(data[1], b1[1]);
            Assert.AreEqual(data[2], b1[2]);
            Assert.AreEqual(data[3], b2[0]);
            Assert.AreEqual(data[4], b2[1]);
            Assert.AreEqual(data[5], b2[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ReadAllBytes_Exception()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0 };
            var message = new IncomingMessage(data, null);

            message.ReadAllBytes();
            message.ReadBytes(1);
        }

        [TestMethod]
        public void ReadBytes()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0 };
            var message = new IncomingMessage(data, null);

            byte[] b1 = message.ReadBytes(2);
            byte[] b2 = message.ReadBytes(4);

            Assert.AreEqual(2, b1.Length);
            Assert.AreEqual(4, b2.Length);
            Assert.AreEqual(data[0], b1[0]);
            Assert.AreEqual(data[1], b1[1]);
            Assert.AreEqual(data[2], b2[0]);
            Assert.AreEqual(data[3], b2[1]);
            Assert.AreEqual(data[4], b2[2]);
            Assert.AreEqual(data[5], b2[3]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ReadBytes_Exception()
        {
            byte[] data = { 0x11, 0xff, 0x12, 0x47, 0xaf, 0xf0 };
            var message = new IncomingMessage(data, null);

            byte[] b1 = message.ReadBytes(2);
            byte[] b2 = message.ReadBytes(4);
            byte[] b3 = message.ReadBytes(1);
        }
    }
}
