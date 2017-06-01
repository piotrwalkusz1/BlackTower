using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkProject;
using NetworkProject.Connection;
using UnityEngine;

namespace UnitTest.FakeToServerTest
{
    public class FakeAccountRepository : IAccountRepository
    {
        private List<RegisterAccount> _registerAccounts = new List<RegisterAccount>();
        private List<OnlineAccount> _onlineAccounts = new List<OnlineAccount>();

        private int _nextIdAccount = 0;
        private int _nextIdCharacter = 0;

        public FakeAccountRepository()
        {
            RegisterAccount account = new RegisterAccount("Login", "Password");
            RegisterAccount(account);

            RegisterCharacter character = new RegisterCharacter();
            character.Name = "Neon";
            character.EndPosition = new Vector3(0, 400, 0);
            character.BreedAndGender = new BreedAndGender(0, true);
            character.Lvl = 1;
            character.Exp = 0;
            RegisterCharacter(account, character);

            RegisterAccount account2 = new RegisterAccount("Login1", "Password");
            RegisterAccount(account2);

            RegisterCharacter character2 = new RegisterCharacter();
            character2.Name = "Spejkon";
            character2.EndPosition = new Vector3(3, 400, 3);
            character2.BreedAndGender = new BreedAndGender(0, true);
            RegisterCharacter(account2, character2);  
        }

        public void RegisterAccount(RegisterAccount registerAccount)
        {
            registerAccount.IdAccount = NextAccountId();

            _registerAccounts.Add(registerAccount);
        }

        public void RegisterCharacter(RegisterAccount account, RegisterCharacter character)
        {
            character.IdCharacter = NextCharacterId();

            account.AddCharacter(character);
        }

        public OnlineAccount LoginAccount(RegisterAccount account, IConnectionMember address)
        {
            OnlineAccount onlineAccount = new OnlineAccount(account.IdAccount, address);
            _onlineAccounts.Add(onlineAccount);
            return onlineAccount;
        }

        public OnlineCharacter LoginCharacter(OnlineAccount account, int characterSlotInAccount)
        {
            OnlineCharacter onlineCharacter = account.LoginCharacter(characterSlotInAccount);
        
            return onlineCharacter;
        }

        public void LogoutAccount(OnlineAccount account)
        {
            if (account.IsLoggedCharacter())
            {
                LogoutAndDeleteCharacter(account.OnlineCharacter);
            }

            _onlineAccounts.Remove(account);
        }

        public void LogoutAndDeleteCharacter(OnlineCharacter character)
        {
            character.UpdateRegisterCharacter();

            SceneBuilder.DeletePlayer(character);

            character.MyAccount.LogoutCharacter();
        }

        public RegisterAccount[] GetAccounts()
        {
            return _registerAccounts.ToArray();
        }

        public OnlineAccount[] GetOnlineAccounts()
        {
            return _onlineAccounts.ToArray();
        }

        private int NextAccountId()
        {
            return _nextIdAccount++;
        }

        private int NextCharacterId()
        {
            return _nextIdCharacter++;
        }
    }
}
