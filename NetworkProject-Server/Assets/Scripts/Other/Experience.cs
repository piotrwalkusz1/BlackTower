using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour
{
    public virtual int Lvl { get; protected set; }
    public virtual int Exp { get; protected set; }
    public virtual int MaxExp { get; protected set; }

    public void AddExp(int exp)
    {
        Exp += exp;
    }

    private void CheckIsNewLvl()
    {
        if (Exp >= MaxExp)
        {
            NewLvl();
        }

        CheckIsNewLvl();
    }

    private void NewLvl()
    {
        Exp -= MaxExp;
        Lvl++;
    }
}
