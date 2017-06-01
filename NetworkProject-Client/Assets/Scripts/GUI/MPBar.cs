using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MPBar : MonoBehaviour
{
    public GUITexture _mpBar;
    public int _maxWidth;

    private SpellCaster _caster;

    void Update()
    {
        if (_caster == null)
        {
            if (Client.GetNetOwnPlayer() != null)
            {
                _caster = Client.GetNetOwnPlayer().GetComponent<SpellCaster>();

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
        _mpBar.pixelInset = new Rect(_mpBar.pixelInset.x, _mpBar.pixelInset.y, (int)(_caster.Mana * _maxWidth / _caster.MaxMana), _mpBar.pixelInset.height);
    }
}
