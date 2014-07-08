using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;
using NetworkProject.BodyParts;

namespace UnitTest
{
    [TestClass]
    public class IoCTest
    {
        [TestMethod]
        public void GetBodyPart_NotReturnNull()
        {
            BodyPart bodyPart = IoC.GetBodyPart(0);

            Assert.IsNotNull(bodyPart);
        }

        [TestMethod]
        public void GetBodyPart_ReturnAdequateBodyPart()
        {
            BodyPart bodyPart = IoC.GetBodyPart(0);

            Assert.IsTrue(bodyPart is Head);
        }

        [TestMethod]
        public void GetBodyPart_ReturnedSameBodyPartsAreOtherInstantiates()
        {
            BodyPart bodyPart1 = IoC.GetBodyPart(0);
            BodyPart bodyPart2 = IoC.GetBodyPart(0);

            Assert.AreNotSame(bodyPart1, bodyPart2);
        }

        [TestMethod]
        public void GetBodyParts_ReturnLengthGreaterThan0()
        {
            BodyPart[] bodyParts = IoC.GetBodyParts();

            Assert.IsTrue(bodyParts.Length > 0);
        }

        [TestMethod]
        public void GetBodyPartAndGetBodyPartsReturnSameTypes()
        {
            BodyPart[] bodyParts = IoC.GetBodyParts();

            for (int i = 0; i < bodyParts.Length; i++)
            {
                BodyPart bodyPart = IoC.GetBodyPart(i);

                Assert.AreEqual(bodyPart.GetType(), bodyParts[i].GetType());
            }
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetBodyPart_NotReturnMoreThanGetBodyParts()
        {
            BodyPart[] bodyParts = IoC.GetBodyParts();

            IoC.GetBodyPart(bodyParts.Length);
        }
    }
}
