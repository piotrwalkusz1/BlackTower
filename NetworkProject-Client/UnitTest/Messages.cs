using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;

namespace UnitTest
{
    [TestClass]
    public class Messages
    {
        [TestMethod]
        public void ToBytesAndSetTest_String()
        {
            OutgoingMessage om = new OutgoingMessage();
            om.Write("abc");

            IncomingMessage im = new IncomingMessage(om.Data.ToArray());

            Assert.AreEqual("abc", im.ReadString());
        }

        [TestMethod]
        public void WriteAndReadTwoStrings()
        {
            OutgoingMessage om = new OutgoingMessage();
            om.Write("abc");
            om.Write("def");

            IncomingMessage im = new IncomingMessage(om.Data.ToArray());
            string a = im.ReadString();
            string b = im.ReadString();

            Assert.AreEqual("abc", a);
            Assert.AreEqual("def", b);
        }

        [TestMethod]
        public void WriteAndReadDateTime()
        {
            OutgoingMessage om = new OutgoingMessage();
            DateTime time = new DateTime(2014, 2, 16, 23, 59, 57);
            om.Write(time);

            IncomingMessage im = new IncomingMessage(om.Data.ToArray());
            DateTime reTime = im.ReadDateTime();

            Assert.AreEqual(time.Year, reTime.Year);
            Assert.AreEqual(time.Month, reTime.Month);
            Assert.AreEqual(time.Day, reTime.Day);
            Assert.AreEqual(time.Hour, reTime.Hour);
            Assert.AreEqual(time.Minute, reTime.Minute);
            Assert.AreEqual(time.Second, reTime.Second);
        }
    }
}
