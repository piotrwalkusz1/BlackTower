using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public GUITexture _hpBar;
    public int _maxWidth;

    protected PlayerHealth _health;

    void Update()
    {
        if (_health == null)
        {
            if (Client.GetNetOwnPlayer() != null)
            {
                _health = Client.GetNetOwnPlayer().GetComponent<PlayerHealth>();

                UpdateBar();
            }           
        }
        else
        {
            UpdateBar();
        }
    }

    void UpdateBar()
    {
        _hpBar.pixelInset = new Rect(_hpBar.pixelInset.x, _hpBar.pixelInset.y, (int)(_health.HP * _maxWidth / _health.MaxHP), _hpBar.pixelInset.height);
    }
}
