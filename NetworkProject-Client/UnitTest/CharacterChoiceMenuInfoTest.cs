using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkProject;

namespace UnitTest
{
    [TestClass]
    public class CharacterChoiceMenuInfoTest
    {
        [TestMethod]
        public void ToBytesAndSetTest()
        {
            var menuInfo = new CharacterChoiceMenuPackage();

            var ch1 = new CharacterInChoiceMenuPackage();
            ch1.Name = "Abc";

            var ch2 = new CharacterInChoiceMenuPackage();
            ch2.Name = "Def";

            menuInfo.AddCharacter(ch1);
            menuInfo.AddCharacter(ch2);

            byte[] bytes = menuInfo.ToBytes();
            IncomingMessage m = new IncomingMessage(bytes);

            var menuInfoWithBytes = new CharacterChoiceMenuPackage();
            menuInfoWithBytes.Set(m);

            Assert.AreEqual("Abc", menuInfoWithBytes.Characters[0].Name);
            Assert.AreEqual("Def", menuInfoWithBytes.Characters[1].Name);
        }
    }
}
