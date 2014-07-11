using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{	
	public int _hp;
	public int _maxHp;
    public float _hpRegeneration;

    public void Awake()
    {
        _maxHp = 1;
        _hp = 1;
    }

    public bool IsDead()
    {
        return _hp <= 0;
    }

    public virtual void Dead()
    {
        _hp = 0;
    }
}
