using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public static class AccountsRepository
{
    private static IAccountsRepository _accountRepository;

    static AccountsRepository()
    {
        _accountRepository = Standard.IoC.GetImplementationAccountsRepository();
    }

    public static OnlineAccount LoginAccount(string login, string password, IConnectionMember address)
    {
        var registerAccount = GetAccountByLogin(login);

        if (registerAccount.CheckPassword(password))
        {
            return LoginAccount(registerAccount.IdAccount, address);
        }
        else
        {
            MonoBehaviour.print("Złe hasło.");

            return null;
        }
    }

    public static OnlineAccount LoginAccount(int idAccount, IConnectionMember address)
    {
        return _accountRepository.LoginAccount(idAccount, address);
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
        return _accountRepository.GetAccountById(id);
    }

    public static RegisterAccount GetAccountByLogin(string login)
    {
        return _accountRepository.GetAccountByLogin(login);
    }

    public static OnlineAccount GetOnlineAccountByAddress(IConnectionMember address)
    {
        return _accountRepository.GetOnlineAccountByAddress(address);
    }

    public static OnlineAccount GetOnlineAccountByLogin(string login)
    {
        return _accountRepository.GetOnlineAccountByLogin(login);
    }

    public static OnlineAccount GetOnlineAccountByIdAccount(int id)
    {
        return _accountRepository.GetOnlineAccountByIdAccount(id);
    }

    public static RegisterCharacter GetCharacterById(int idPlayer)
    {
        return _accountRepository.GetCharacterByIdCharacter(idPlayer);
    }

    public static OnlineCharacter GetOnlineCharacterByAddress(IConnectionMember address)
    {
        return _accountRepository.GetOnlineCharacterByAddress(address);
    }

    public static void UpdateRegisterCharacter(int idCharacter, GameObject player)
    {
        var character = GetCharacterById(idCharacter);

        character.Update(player);
    }
}
