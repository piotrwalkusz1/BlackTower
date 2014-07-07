using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

[System.CLSCompliant(false)]
public class MonsterStats : MonoBehaviour, IStats
{
    public virtual int HP { get; set; }

    public virtual int MaxHP { get; set; }

    public virtual float MovementSpeed { get; set; }

    public virtual float AttackSpeed
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public List<int> Damages
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public int Defense
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    #region Unnecessary

    public int Lvl
    {
        get { throw new System.NotImplementedException(); }
    }

    public float RegenerationHP
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public Breed Breed
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    } 

    #endregion
}
