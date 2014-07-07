using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public interface IAccountRepository 
{
    void RegisterAccount(RegisterAccount registerAccount);
    void RegisterCharacter(RegisterAccount acount, RegisterCharacter registerCharacter);
    OnlineAccount LoginAccount(RegisterAccount account, IConnectionMember address);
    OnlineCharacter LoginCharacter(OnlineAccount account, int characterSlotInAccount);
    void LogoutAccount(OnlineAccount account);
    void LogoutAndDeleteCharacter(OnlineCharacter character);
    RegisterAccount[] GetAccounts();
    OnlineAccount[] GetOnlineAccounts();
}
