using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour
{
    public virtual int Lvl { get; set; }
    public virtual int Exp { get; protected set; }
    public virtual int MaxExp { get; protected set; }

    public void AddExp(int exp)
    {
        Exp += exp;

        CheckIsNewLvl();
    }

    protected void CheckIsNewLvl()
    {
        if (Exp >= MaxExp)
        {
            NewLvl();
            CheckIsNewLvl();
        }
    }

    protected void NewLvl()
    {
        Exp -= MaxExp;
        Lvl++;
    }
}
