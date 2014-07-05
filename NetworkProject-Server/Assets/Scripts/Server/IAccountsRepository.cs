using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public interface IAccountsRepository 
{
    void RegisterAccount(RegisterAccount registerAccount);
    void RegisterCharacter(int idAccount, RegisterCharacter registerCharacter);
    OnlineAccount LoginAccount(int idAccount, IConnectionMember address);
    OnlineCharacter LoginCharacter(OnlineAccount account, int characterSlotInAccount);
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
    OnlineCharacter GetOnlineCharacterByIdNet(int netPlayerObjectId);
    OnlineCharacter GetOnlineCharacterByAddress(IConnectionMember address);
}
