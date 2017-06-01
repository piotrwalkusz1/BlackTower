using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{	
	public virtual int HP { get; set; }
    public virtual int MaxHP { get; set; }
    public float HPRegeneration { get; set; }

    public bool IsDead()
    {
        return HP <= 0;
    }

    public virtual void Dead()
    {
        HP = 0;
    }

    public bool IsAlive()
    {
        return !IsDead();
    }
}
