using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public class OnlineAccount
{
    public int IdAccount { get; private set; }
    public IConnectionMember Address { get; set; }
    public OnlineCharacter OnlineCharacter { get; private set; }

    public OnlineAccount(RegisterAccount account)
    {
        IdAccount = account.IdAccount;
    }

    public OnlineCharacter CreateOnlineCharacter(int characterSlot)
    {
        var onlineCharacter = new OnlineCharacter(characterSlot);

        netPlayer.OnlineCharacter = onlineCharacter;
        onlineCharacter.GameObject = netPlayer.gameObject;

        SetOnlineCharacter(onlineCharacter);

        return onlineCharacter;
    }

    public bool IsLoggedCharacter()
    {
        return OnlineCharacter != null;
    }

    public void SetOnlineCharacter(OnlineCharacter character)
    {
        character.MyAccount = this;
        OnlineCharacter = character;
    }

    public void DeleteOnlineCharacter()
    {
        OnlineCharacter = null;
    }
}
