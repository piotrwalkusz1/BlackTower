using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetworkProject;

[System.CLSCompliant(false)]
public class AccountsRepositoryInServer : IAccountsRepository
{
    private List<RegisterAccount> _accountsData;
    private List<OnlineAccount> _onlineAccounts;

    private int _nextIdAccount = 0;
    private int _nextIdCharacter = 0;

    public AccountsRepositoryInServer()
    {
        _accountsData = new List<RegisterAccount>();
        _onlineAccounts = new List<OnlineAccount>();

        RegisterAccount account = new RegisterAccount();
        account.Login = "Login";
        account.Password = "Password";
        RegisterAccount(account);

        RegisterCharacter character = new RegisterCharacter();
        character.Name = "Neon";
        character.EndPosition = new Vector3(0, 400, 0);
        character.Breed = Breed.Troll;
        character.AddSpell(new Spell(SpellRepository.GetSpellById(0)));
        character.Lvl = 1;
        character.Exp = 0;
        RegisterCharacter(account.IdAccount, character);

        RegisterAccount account2 = new RegisterAccount();
        account2.Login = "Login1";
        account2.Password = "Password";
        RegisterAccount(account2);

        RegisterCharacter character2 = new RegisterCharacter();
        character2.Name = "Spejkon";
        character2.EndPosition = new Vector3(3, 400, 3);
        character2.Breed = Breed.Troll;
        RegisterCharacter(account2.IdAccount, character2);  
    }

    public void RegisterAccount(RegisterAccount registerAccount)
    {
        registerAccount.IdAccount = _nextIdAccount;
        _nextIdAccount ++;
        _accountsData.Add(registerAccount);
    }

    public void RegisterCharacter(int idAccount, RegisterCharacter registerCharacter)
    {
        registerCharacter.IdCharacter = _nextIdCharacter;
        _nextIdCharacter ++;

        RegisterAccount account = GetAccountById(idAccount);

        account.AddCharacter(registerCharacter);
    }

    public bool IsAccountLogged(string login)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByLogin(login);
        return onlineAccount != null;
    }

    public bool IsAccountLogged(IConnectionMember address)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByAddress(address);
        return onlineAccount != null;
    }

    public bool IsAccountLogged(RegisterAccount account)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByLogin(account.Login);
        return onlineAccount != null;
    }

    public bool IsCharacterLogged(string login)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByLogin(login);

        if (onlineAccount == null)
        {
            return false;
        }
        else
        {
            return onlineAccount.OnlineCharacter != null;
        }
    }

    public bool IsCharacterLogged(IConnectionMember address)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByAddress(address);

        if (onlineAccount == null)
        {
            return false;
        }
        else
        {
            return onlineAccount.OnlineCharacter != null;
        }
    }

    public bool IsCharacterLogged(RegisterAccount account)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByLogin(account.Login);

        if (onlineAccount == null)
        {
            return false;
        }
        else
        {
            return onlineAccount.OnlineCharacter != null;
        }
    }

    public OnlineAccount LoginAccount(RegisterAccount account, IConnectionMember address)
    {
        OnlineAccount onlineAccount = new OnlineAccount(account);
        onlineAccount.Address = address;
        _onlineAccounts.Add(onlineAccount);
        return onlineAccount;
    }

    public OnlineCharacter LoginAndCreateCharacter(RegisterCharacter character)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByIdCharacter(character.IdCharacter);

        if (onlineAccount.Address == null)
        {
            throw new System.SystemException("Null address");
        }

        GameObject playerInstantiate = SceneBuilder.CreatePlayer(character, onlineAccount.Address);

        OnlineCharacter onlineCharacter = onlineAccount.CreateOnlineCharacter(playerInstantiate.GetComponent<NetPlayer>(), character.IdCharacter);

        onlineCharacter.SendUpdateMessageToOwner();
        
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
        SceneBuilder.DeletePlayer(character);

        UpdateRegisterCharacter(character.IdCharacter, character.NetPlayerObject);

        character.MyAccount.DeleteOnlineCharacter();
    }

    public RegisterAccount GetAccountByLogin(string login)
    {
        return _accountsData.Find(x => x.Login == login);
    }

    public RegisterAccount GetAccountById(int id)
    {
        return _accountsData.Find(x => x.IdAccount == id);
    }

    public OnlineAccount GetOnlineAccountByAddress(IConnectionMember address)
    {
        var foundOnlineAccounts = from onlineAccount in _onlineAccounts
                                  where onlineAccount.Address.Equals(address)
                                  select onlineAccount;
        return foundOnlineAccounts.SingleOrDefault();
    }

    public OnlineAccount GetOnlineAccountByLogin(string login)
    {
        RegisterAccount registerAccount = GetAccountByLogin(login);

        OnlineAccount onlineAccount = GetOnlineAccountByIdAccount(registerAccount.IdAccount);

        return onlineAccount;
    }

    public OnlineAccount GetOnlineAccountByIdAccount(int id)
    {
        foreach (OnlineAccount account in _onlineAccounts)
        {
            if (account.IdAccount == id)
            {
                return account;
            }
        }
        return null;
    }

    public OnlineAccount GetOnlineAccountByIdCharacter(int id)
    {
        var characters = from account in _accountsData
                         from character in account.Characters
                         where character.IdCharacter == id
                         select character;
        RegisterCharacter registerCharacter = characters.SingleOrDefault();
        if (registerCharacter == null)
        {
            return null;
        }
        else
        {
            return GetOnlineAccountByIdAccount(registerCharacter.MyAccount.IdAccount);
        }
    }

    public RegisterCharacter GetCharacterByIdCharacter(int idCharacter)
    {
        var characters = from account in _accountsData
                         from character in account.Characters
                         select character;
        var foundCharacters = from character in characters
                              where character.IdCharacter == idCharacter
                              select character;
        return foundCharacters.SingleOrDefault();
    }

    public RegisterCharacter GetCharacterByName(string name)
    {
        var characters = from account in _accountsData
                         from character in account.Characters
                         select character;
        var foundCharacters = from character in characters
                              where character.Name == name
                              select character;
        return foundCharacters.SingleOrDefault();
    }

    public OnlineCharacter GetOnlineCharacterByNetId(int netPlayerObjectId)
    {
        foreach (OnlineAccount account in _onlineAccounts)
        {
            if (account.IsLoggedCharacter() && account.OnlineCharacter.NetPlayerObject.IdObject == netPlayerObjectId)
            {
                return account.OnlineCharacter;
            }
        }
        return null;
    }

    public OnlineCharacter GetOnlineCharacterByName(string name)
    {
        foreach (OnlineAccount account in _onlineAccounts)
        {
            if (account.IsLoggedCharacter() && account.OnlineCharacter.NetPlayerObject.Name == name)
            {
                return account.OnlineCharacter;
            }
        }
        return null;
    }

    public OnlineCharacter GetOnlineCharacterByAddress(IConnectionMember address)
    {
        foreach (OnlineAccount account in _onlineAccounts)
        {
            if (account.IsLoggedCharacter() && account.Address.Equals(address))
            {
                return account.OnlineCharacter;
            }
        }
        return null;
    }

    public void UpdateRegisterCharacter(int idCharater, NetPlayer player)
    {
        RegisterCharacter charater = GetCharacterByIdCharacter(idCharater);

        charater.Update(player);
    }
}
