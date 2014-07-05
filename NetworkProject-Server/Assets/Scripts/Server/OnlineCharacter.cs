using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public class OnlineCharacter
{
    public OnlineAccount MyAccount { get; private set; }
    public int CharacterSlotInAccount { get; private set; }
    public GameObject Instantiate { get; private set; }

    public int IdCharacter 
    {
        get
        {
            return CharacterData.IdCharacter;
        }
    }
    public RegisterCharacter CharacterData
    {
        get
        {
            RegisterAccount account = AccountsRepository.GetAccountById(MyAccount.IdAccount);
            return account.Characters[CharacterSlotInAccount];
        }
    }
    public IConnectionMember Address
    {
        get
        {
            return MyAccount.Address;
        }
    }  

    public OnlineCharacter(OnlineAccount myAccount, int characterSlot)
    {
        MyAccount = myAccount;
        CharacterSlotInAccount = characterSlot;
    }

    public void CreatePlayerInstantiate()
    {
        Instantiate = SceneBuilder.CreatePlayer(CharacterData, Address);
    }

    public void UpdateRegisterCharacter()
    {
        CharacterData.Update(Instantiate);
    }

    public void LogoutAndDeleteCharacter()
    {
        SceneBuilder.DeletePlayer(this);

        MyAccount.LogoutCharacter();
    }
}
