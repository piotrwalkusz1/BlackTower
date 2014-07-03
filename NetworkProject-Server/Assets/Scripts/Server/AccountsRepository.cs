using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public static class AccountsRepository
{
    private static IAccountsRepository _accountRepository;

    static AccountsRepository()
    {
        _accountRepository = IoC.GetImplementationAccountsRepository();
    }

    public static bool IsAccountLogged(string login)
    {
        return _accountRepository.IsAccountLogged(login);
    }

    public static bool IsAccountLogged(IConnectionMember address)
    {
        return _accountRepository.IsAccountLogged(address);
    }

    public static bool IsAccountLogged(RegisterAccount account)
    {
        return _accountRepository.IsAccountLogged(account);
    }

    public static bool IsCharacterLogged(string login)
    {
        return _accountRepository.IsCharacterLogged(login);
    }

    public static bool IsCharacterLogged(IConnectionMember address)
    {
        return _accountRepository.IsCharacterLogged(address);
    }

    public static bool IsCharacterLogged(RegisterAccount account)
    {
        return _accountRepository.IsCharacterLogged(account);
    }

    public static OnlineAccount LoginAccount(RegisterAccount account, IConnectionMember address)
    {
        return _accountRepository.LoginAccount(account, address);
    }

    public static OnlineCharacter LoginAndCreateCharacter(RegisterCharacter character)
    {
        return _accountRepository.LoginAndCreateCharacter(character);
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

    public static RegisterCharacter GetCharacterByName(string name)
    {
        return _accountRepository.GetCharacterByName(name);
    }

    public static OnlineCharacter GetOnlineCharacterByAddress(IConnectionMember address)
    {
        return _accountRepository.GetOnlineCharacterByAddress(address);
    }

    public static void UpdateRegisterCharacter(int idCharacter, NetPlayer player)
    {
        _accountRepository.UpdateRegisterCharacter(idCharacter, player);
    }
}
