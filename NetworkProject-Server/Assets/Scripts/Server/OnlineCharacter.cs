using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public class OnlineCharacter
{
    public OnlineCharacter(OnlineAccount myAccount, int characterSlot)
    {
        MyAccount = myAccount;
        CharacterSlot = characterSlot;
    }

    public int CharacterSlot { get; private set; }
    public OnlineAccount MyAccount { get; private set; }
    public IConnectionMember Address
    {
        get
        {
            return MyAccount.Address;
        }
    }
    public GameObject GameObject { get; private set; }

    public void CreatePlayerInstantiate()
    {
        GameObject = SceneBuilder.CreatePlayer()
    }
}
