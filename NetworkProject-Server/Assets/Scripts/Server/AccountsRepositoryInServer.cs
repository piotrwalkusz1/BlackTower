using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public class AccountsRepositoryInServer : IAccountsRepository
{
    private List<RegisterAccount> _registerAccounts = new List<RegisterAccount>();
    private List<OnlineAccount> _onlineAccounts = new List<OnlineAccount>();

    private int _nextIdAccount = 0;
    private int _nextIdCharacter = 0;

    public AccountsRepositoryInServer()
    {
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
        _registerAccounts.Add(registerAccount);
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

    public OnlineAccount LoginAccount(int idAccount, IConnectionMember address)
    {
        OnlineAccount onlineAccount = new OnlineAccount(idAccount, address);
        _onlineAccounts.Add(onlineAccount);
        return onlineAccount;
    }

    public OnlineCharacter LoginCharacter(OnlineAccount account, int characterSlotInAccount)
    {
        if (account.IsLoggedCharacter())
        {
            throw new System.Exception("Na tym koncie jest już zalogowana postać!");
        }

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

    public RegisterAccount GetAccountByLogin(string login)
    {
        return _registerAccounts.Find(x => x.Login == login);
    }

    public RegisterAccount GetAccountById(int id)
    {
        return _registerAccounts.Find(x => x.IdAccount == id);
    }

    public OnlineAccount GetOnlineAccountByAddress(IConnectionMember address)
    {
        foreach (OnlineAccount account in _onlineAccounts)
        {
            if (account.Address.Equals(address))
            {
                return account;
            }
        }

        return null;
    }

    public OnlineAccount GetOnlineAccountByLogin(string login)
    {
        RegisterAccount registerAccount = GetAccountByLogin(login);

        OnlineAccount onlineAccount = GetOnlineAccountByIdAccount(registerAccount.IdAccount);

        return onlineAccount;
    }

    public OnlineAccount GetOnlineAccountByIdAccount(int id)
    {
        return _onlineAccounts.Find(x => x.IdAccount == id);
    }

    public OnlineAccount GetOnlineAccountByIdCharacter(int id)
    {
        RegisterCharacter character = GetCharacterByIdCharacter(id);

        if (character == null)
        {
            return null;
        }
        else
        {
            return GetOnlineAccountByIdAccount(character.MyAccount.IdAccount);
        }
    }

    public RegisterCharacter GetCharacterByIdCharacter(int idCharacter)
    {
        foreach (RegisterAccount account in _registerAccounts)
        {
            foreach (RegisterCharacter character in account.Characters)
            {
                if (character.IdCharacter == idCharacter)
                {
                    return character;
                }
            }
        }

        return null;
    }

    public RegisterCharacter GetCharacterByName(string name)
    {
        foreach (RegisterAccount account in _registerAccounts)
        {
            foreach (RegisterCharacter character in account.Characters)
            {
                if (character.Name == name)
                {
                    return character;
                }
            }
        }

        return null;
    }

    public OnlineCharacter GetOnlineCharacterByIdNet(int idNet)
    {
        foreach (OnlineAccount account in _onlineAccounts)
        {
            if (account.IsLoggedCharacter() && account.OnlineCharacter.Instantiate.GetComponent<NetPlayer>().IdNet == idNet)
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
}
