using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class OnlineAccount
{
    public int IdAccount { get; private set; }
    public IConnectionMember Address { get; set; }
    public OnlineCharacter OnlineCharacter { get; private set; }

    public OnlineCharacter CreateOnlineCharacter(NetPlayer netPlayer, int idCharacter)
    {
        var onlineCharacter = new OnlineCharacter(idCharacter);

        netPlayer.OnlineCharacter = onlineCharacter;
        onlineCharacter.GameObject = netPlayer.gameObject;

        SetOnlineCharacter(onlineCharacter);

        return onlineCharacter;
    }

    public OnlineAccount(RegisterAccount account)
    {
        IdAccount = account.IdAccount;
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
