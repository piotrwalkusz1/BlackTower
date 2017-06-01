using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

public class OnlineAccount
{
    public int IdAccount { get; private set; }
    public IConnectionMember Address { get; private set; }
    public OnlineCharacter OnlineCharacter { get; private set; }
    public RegisterAccount AccountData
    {
        get
        {
            return AccountRepository.GetAccountById(IdAccount);
        }
    }

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

    public RegisterCharacter[] GetCharacters()
    {
        return AccountData.Characters.ToArray();
    }

    public void UpdateRegisterCharacter()
    {
        if (OnlineCharacter != null)
        {
            OnlineCharacter.UpdateRegisterCharacter();
        }
    }
}
