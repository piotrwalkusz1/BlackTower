using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public class OnlineAccount
{
    public int IdAccount { get; private set; }
    public IConnectionMember Address { get; private set; }
    public OnlineCharacter OnlineCharacter { get; private set; }

    public OnlineAccount(int idAccount, IConnectionMember address)
    {
        IdAccount = idAccount;
        Address = address;
    }

    public OnlineCharacter LoginCharacter(int characterSlotInAccount)
    {
        var onlineCharacter = new OnlineCharacter(this, characterSlotInAccount);

        OnlineCharacter = onlineCharacter;

        return onlineCharacter;
    }

    public bool IsLoggedCharacter()
    {
        return OnlineCharacter != null;
    }

    public void LogoutCharacter()
    {
        OnlineCharacter = null;
    }
}
