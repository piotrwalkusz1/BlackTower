using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public static class AccountRepository
{
    private static IAccountRepository _accountRepository;

    static AccountRepository()
    {
        _accountRepository = Standard.IoC.GetAccountRepository();
    }

    public static void Set(IAccountRepository repository)
    {
        _accountRepository = repository;
    }

    public static OnlineAccount LoginAccount(string login, string password, IConnectionMember address)
    {
        var registerAccount = GetAccountByLogin(login);

        if (registerAccount.CheckPassword(password))
        {
            return LoginAccount(registerAccount, address);
        }
        else
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.WrongLoginOrPassword);
        }
    }

    public static OnlineAccount LoginAccount(RegisterAccount account, IConnectionMember address)
    {
        return _accountRepository.LoginAccount(account, address);
    }

    public static OnlineCharacter LoginCharacter(OnlineAccount account, int characterSlotInAccount)
    {
        return _accountRepository.LoginCharacter(account, characterSlotInAccount);
    }

    public static void LogoutAccount(OnlineAccount account)
    {
        _accountRepository.LogoutAccount(account);
    }

    public static void LogoutAndDeleteCharacter(OnlineCharacter character)
    {
        _accountRepository.LogoutAndDeleteCharacter(character);
    }

    public static RegisterAccount GetAccountById(int id)
    {
        return _accountRepository.GetAccounts().First(x => x.IdAccount == id);
    }

    public static RegisterAccount GetAccountByLogin(string login)
    {
        return _accountRepository.GetAccounts().First(x => x.Login == login);
    }

    public static OnlineAccount GetOnlineAccountByAddress(IConnectionMember address)
    {
        return _accountRepository.GetOnlineAccounts().First(x => x.Address.Equals(address));
    }

    public static OnlineAccount GetOnlineAccountByLogin(string login)
    {
        RegisterAccount account = GetAccountByLogin(login);

        return _accountRepository.GetOnlineAccounts().First(x => x.IdAccount == account.IdAccount);
    }

    public static OnlineAccount GetOnlineAccountByIdAccount(int id)
    {
        return _accountRepository.GetOnlineAccounts().First(x => x.IdAccount == id);
    }

    public static RegisterCharacter GetCharacterById(int id)
    {
        foreach (var account in _accountRepository.GetAccounts())
        {
            foreach (var character in account.Characters)
            {
                if (character.IdCharacter == id)
                {
                    return character;
                }
            }
        }

        return null;
    }

    public static OnlineCharacter GetOnlineCharacterByAddress(IConnectionMember address)
    {
        OnlineAccount account = GetOnlineAccountByAddress(address);

        return account.OnlineCharacter;
    }

    public static void UpdateRegisterCharacter(int idCharacter, GameObject player)
    {
        var character = GetCharacterById(idCharacter);

        character.Update(player);
    }

    public static NetPlayer FindNetPlayerByAddress(IConnectionMember address)
    {
        OnlineCharacter onlineCharacter = AccountRepository.GetOnlineCharacterByAddress(address);

        return onlineCharacter.Instantiate.GetComponent<NetPlayer>();
    }

    public static NetPlayer FindAliveNetPlayerByAddress(IConnectionMember address)
    {
        var player = FindNetPlayerByAddress(address);

        if (player.GetComponent<PlayerHealthSystem>().IsDead())
        {
            throw new Exception("Gracz jest martwy!");
        }

        return player;
    }

    public static NetPlayer FindDeadNetPlayerByAddress(IConnectionMember address)
    {
        var player = FindNetPlayerByAddress(address);

        if (!player.GetComponent<PlayerHealthSystem>().IsDead())
        {
            throw new Exception("Gracz jest żywy!");
        }

        return player;
    }
}
