using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class OnlineCharacter
{
    public OnlineCharacter(int id)
    {
        IdCharacter = id;
    }

    public int IdCharacter { get; private set; }
    public OnlineAccount MyAccount { get; set; }
    public IConnectionMember Address
    {
        get
        {
            return MyAccount.Address;
        }
    }
    public GameObject GameObject { get; set; }
    public NetPlayer NetPlayerObject
    {
        get
        {
            return GameObject.GetComponent<NetPlayer>();
        }
    }
    public PlayerMovementSystem PlayerMovement
    {
        get
        {
            return GameObject.GetComponent<PlayerMovementSystem>();
        }
    }
    public Vision Vision
    {
        get
        {
            return GameObject.GetComponent<Vision>();
        }
    }
    public PlayerEquipment Equipment
    {
        get
        {
            return GameObject.GetComponent<PlayerEquipment>();
        }
    }
    public PlayerCombat PlayerCombat
    {
        get
        {
            return GameObject.GetComponent<PlayerCombat>();
        }
    }

    public void SendUpdateMessageToOwner()
    {
        GameObject.GetComponent<PlayerHealthSystem>().SendHpUpdatingToOwner();
        GameObject.GetComponent<PlayerEquipment>().SendUpdateAllSlots();
        GameObject.GetComponent<SpellCaster>().SendUpdateSpells();
        GameObject.GetComponent<PlayerExperience>().SendUpdate();
    }
}
