using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Items;

public static class AccountRepository
{
    public static readonly Vector3 StartedPlayerPosition = new Vector3(2.034565f, 392.084f, -0.06116676f);

    private static IAccountRepository _accountRepository;

    static AccountRepository()
    {
        _accountRepository = Standard.IoC.GetAccountRepository();
    }

    public static void OnGUI()
    {
        _accountRepository.OnGUI();
    }

    public static void Set(IAccountRepository repository)
    {
        _accountRepository = repository;
    }

    public static RegisterAccount RegisterAccount(string login, string password)
    {
        if (login.Length > 20)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.LoginIsTooLong);
        }
        else if (login.Length < 5)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.LoginIsTooShort);
        }
        else if (!Regex.IsMatch(login, @"^[a-z0-9]*$", RegexOptions.IgnoreCase))
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.LoginContainsNotAllowedCharacters);
        }
        else if (password.Length > 20)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.PasswordIsTooLong);
        }
        else if (password.Length < 5)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.PasswordIsTooShort);
        }
        else if (!Regex.IsMatch(password, @"^[a-z0-9]*$", RegexOptions.IgnoreCase))
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.PasswordContainsNotAllowedCharacters);
        }
        else if (GetAccountByLogin(login) != null)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.LoginAlreadyExist);
        }

        RegisterAccount account = new RegisterAccount(login, password);

        _accountRepository.RegisterAccount(account);

        return account;
    }

    public static RegisterCharacter RegisterCharacter(RegisterAccount account, string name, BreedAndGender breedAndGender)
    {
        if (name.Length > 20)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.CharacterNameIsTooLong);
        }
        else if (name.Length < 5)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.CharacterNameIsTooShort);
        }
        else if (GetCharacterByName(name) != null)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.CharacterNameAlreadyExist);
        }
        else if (!Regex.IsMatch(name, @"^[a-z0-9]*$", RegexOptions.IgnoreCase))
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.CharacterNameContainsNotAllowedCharacters);
        }

        var character = new RegisterCharacter(name, breedAndGender);

        _accountRepository.RegisterCharacter(account, character);

        character.EndPosition = StartedPlayerPosition;

        return character;
    }

    public static OnlineAccount LoginAccount(string login, string password, IConnectionMember address)
    {
        var registerAccount = GetAccountByLogin(login);

        if (registerAccount != null && registerAccount.CheckPassword(password))
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
        if (GetOnlineAccountByIdAccount(account.IdAccount) == null)
        {
            return _accountRepository.LoginAccount(account, address);
        }
        else
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.AccountAlreadyLogin);
        }       
    }

    public static OnlineCharacter LoginCharacter(OnlineAccount account, int characterSlotInAccount)
    {
        if (account.OnlineCharacter != null)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.CharacterAlreadyLogin);
        }
        else if (characterSlotInAccount >= account.GetCharacters().Length)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.CharacterSlotIsEmpty);
        }

        return _accountRepository.LoginCharacter(account, characterSlotInAccount);  
    }

    public static void LogoutAccount(OnlineAccount account)
    {
        _accountRepository.LogoutAccount(account);
    }

    public static void LogoutAccount(string login)
    {
        OnlineAccount onlineAccount = GetOnlineAccountByLogin(login);

        if (onlineAccount == null)
        {
            throw new AccountRepositoryException(AccountRepositoryExceptionCode.AcconuntAlreadyLogout);
        }
        else
        {
            LogoutAccount(onlineAccount);
        }
    }

    public static void LogoutAndDeleteCharacter(OnlineCharacter character)
    {
        _accountRepository.LogoutAndDeleteCharacter(character);
    }

    public static RegisterAccount GetAccountById(int id)
    {
        return _accountRepository.GetAccounts().FirstOrDefault(x => x.IdAccount == id);
    }

    public static RegisterAccount GetAccountByLogin(string login)
    {
        return _accountRepository.GetAccounts().FirstOrDefault(x => x.Login == login);
    }

    public static OnlineAccount GetOnlineAccountByAddress(IConnectionMember address)
    {
        return _accountRepository.GetOnlineAccounts().FirstOrDefault(x => x.Address.Equals(address));
    }

    public static OnlineAccount GetOnlineAccountByLogin(string login)
    {
        RegisterAccount account = GetAccountByLogin(login);

        return _accountRepository.GetOnlineAccounts().FirstOrDefault(x => x.IdAccount == account.IdAccount);
    }

    public static OnlineAccount GetOnlineAccountByIdAccount(int id)
    {
        return _accountRepository.GetOnlineAccounts().FirstOrDefault(x => x.IdAccount == id);
    }

    public static RegisterCharacter GetCharacterByName(string name)
    {
        foreach (var account in _accountRepository.GetAccounts())
        {
            foreach (var character in account.Characters)
            {
                if (character.Name == name)
                {
                    return character;
                }
            }
        }

        return null;
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
