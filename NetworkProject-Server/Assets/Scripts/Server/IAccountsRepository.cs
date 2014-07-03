using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public interface IAccountsRepository 
{
    void RegisterAccount(RegisterAccount registerAccount);
    void RegisterCharacter(int idAccount, RegisterCharacter registerCharacter);
    bool IsAccountLogged(string login);
    bool IsAccountLogged(IConnectionMember address);
    bool IsAccountLogged(RegisterAccount account);
    bool IsCharacterLogged(string login);
    bool IsCharacterLogged(IConnectionMember address);
    bool IsCharacterLogged(RegisterAccount account);
    OnlineAccount LoginAccount(RegisterAccount account, IConnectionMember address);
    OnlineCharacter LoginAndCreateCharacter(RegisterCharacter character);
    void LogoutAccount(OnlineAccount account);
    void LogoutAndDeleteCharacter(OnlineCharacter character);
    RegisterAccount GetAccountByLogin(string login);
    RegisterAccount GetAccountById(int id);
    OnlineAccount GetOnlineAccountByAddress(IConnectionMember address);
    OnlineAccount GetOnlineAccountByLogin(string login);
    OnlineAccount GetOnlineAccountByIdAccount(int id);
    OnlineAccount GetOnlineAccountByIdCharacter(int id);
    RegisterCharacter GetCharacterByIdCharacter(int idPlayer);
    RegisterCharacter GetCharacterByName(string name);
    OnlineCharacter GetOnlineCharacterByNetId(int netPlayerObjectId);
    OnlineCharacter GetOnlineCharacterByName(string name);
    OnlineCharacter GetOnlineCharacterByAddress(IConnectionMember address);
    void UpdateRegisterCharacter(int idCharater, NetPlayer player);
}
