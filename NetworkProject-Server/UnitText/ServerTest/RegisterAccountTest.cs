using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityEngine;

namespace UnitText.ServerTest
{
    [TestClass]
    public class RegisterAccountTest
    {
        [TestMethod]
        public void AddCharacterTest()
        {
            RegisterCharacter character = new RegisterCharacter();
            character.Name = "Krzysio";
            character.EndPosition = Vector3.zero;

            RegisterAccount account = new RegisterAccount();
            account.Login = "a";
            account.Password = "b";

            account.AddCharacter(character);

            Assert.AreEqual("Krzysio", account.Characters[0].Name);
        }
    }
}
