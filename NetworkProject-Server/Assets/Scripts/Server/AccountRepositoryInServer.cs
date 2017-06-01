using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NetworkProject;
using NetworkProject.Connection;
using NetworkProject.Spells;
using NetworkProject.Items;

public class AccountRepositoryInServer : IAccountRepository
{
    private List<RegisterAccount> _registerAccounts = new List<RegisterAccount>();
    private List<OnlineAccount> _onlineAccounts = new List<OnlineAccount>();

    private int _nextIdAccount = 0;
    private int _nextIdCharacter = 0;

    private readonly string _pathToAccounts = Application.dataPath + "/accounts.txt";

    public AccountRepositoryInServer()
    {
        Load();
    }

    public void OnGUI()
    {
        if(GUILayout.Button("Save"))
        {
            Save();
        }
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
        try
        {
            character.UpdateRegisterCharacter();

            SceneBuilder.DeletePlayer(character);
        }
        catch(Exception ex)
        {
            MonoBehaviour.print(ex.Message + '\n' + ex.StackTrace);
        }
        finally
        {
            character.MyAccount.LogoutCharacter();
        }
    }

    public RegisterAccount[] GetAccounts()
    {
        return _registerAccounts.ToArray();
    }

    public OnlineAccount[] GetOnlineAccounts()
    {
        return _onlineAccounts.ToArray();
    }

    private void Save()
    {
        _onlineAccounts.ForEach(x => x.UpdateRegisterCharacter());

        var formatter = new BinaryFormatter();

        var package = new AccountRepositoryToSave(_registerAccounts.ToArray(), _nextIdAccount, _nextIdCharacter);

        using (var stream = new FileStream(_pathToAccounts, FileMode.Create))
        {
            formatter.Serialize(stream, package);
        }
    }

    private void Load()
    {
        var formatter = new BinaryFormatter();

        try
        {
            using (var stream = new FileStream(_pathToAccounts, FileMode.Open))
            {
                var package = (AccountRepositoryToSave)formatter.Deserialize(stream);

                _registerAccounts = new List<RegisterAccount>(package.RegisterAccounts);
                _nextIdAccount = package.NextIdAccount;
                _nextIdCharacter = package.NextIdCharacter;
            }
        }
        catch
        {
            MonoBehaviour.print("Błąd z odczytem kont lub plik nie istnieje");
        }
        
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
