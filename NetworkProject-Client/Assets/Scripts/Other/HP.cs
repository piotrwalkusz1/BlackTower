using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class HP : MonoBehaviour
{	
	public int _hp;
	public int _maxHp;

    public void Awake()
    {
        _maxHp = 1;
        _hp = 1;
    }

    public bool IsDead()
    {
        return _hp <= 0;
    }
}
