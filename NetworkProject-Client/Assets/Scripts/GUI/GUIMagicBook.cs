using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUIMagicBook : GUIObject, IClosable
{
    public int _topMargin;
    public int _leftMargin;
    public int _borderSize;
    public int _imageSize;
    public int _marginBetweenSpells;
    public int _xCount;
    public int _yCount;

    private Vector3 _lastMousePosition;
    private List<GUISpell> _spells;
    private SpellCaster _spellCaster;

    void Awake()
    {
        _lastMousePosition = Input.mousePosition;

        _spells = new List<GUISpell>();

        for (int y = 0; y < _yCount; y++)
        {
            for (int x = 0; x < _xCount; x++)
            {
                GameObject go = Instantiate(Prefabs.GUISpell, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                Vector2 position = GetPixelPositionToSpell(x, y);
                go.guiTexture.pixelInset = new Rect(position.x, position.y, _imageSize, _imageSize);
                go.transform.parent = transform;
                go.transform.localPosition = new Vector3(0, 0, transform.localPosition.z + 0.5f);

                GUISpell spell = go.GetComponent<GUISpell>();
                spell._magicBook = this;

                _spells.Add(spell);
            }
        }
    }

    void LateUpdate()
    {
        _lastMousePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        Vector3 offset = Input.mousePosition - _lastMousePosition;

        transform.position += new Vector3(offset.x / Screen.width, offset.y / Screen.height);

        _lastMousePosition = Input.mousePosition;
    }

    public void Refresh()
    {
        SetSpellCaster(Client.GetNetOwnPlayer().GetComponent<SpellCaster>());

        var spells = _spellCaster.Spells;
        int count = spells.Count;

        for (int i = 0; i < _spells.Count; i++)
        {
            if (i < count)
            {
                _spells[i].ChangeTexture(spells[i]);
            }
            else
            {
                _spells[i].ChangeTexture(null);
            }
        }
    }

    public Vector2 GetPixelPositionToSpell(GUISpell spell)
    {
        int index = _spells.IndexOf(spell);

        Vector2 position = new Vector2(index % _xCount, (int)(index / _yCount));

        return GetPixelPositionToSpell((int)position.x, (int)position.y);
    }

    public Spell GetSpell(GUISpell spell)
    {
        int slot = _spells.IndexOf(spell);

        return _spellCaster.GetSpellBySlot(slot);
    }

    public void SetSpellCaster(SpellCaster spellCaster)
    {
        _spellCaster = spellCaster;
    }

    public void Close()
    {
        GUIController.HideMagicBook();
    }

    private Vector2 GetPixelPositionToSpell(int x, int y)
    {
        int posX = _leftMargin + (2 * x + 1) * _borderSize + (_imageSize + _marginBetweenSpells) * x ;
        int posY = (int)guiTexture.pixelInset.height - _topMargin - (2 * y + 1) * _borderSize - _imageSize * (y + 1) - y * _marginBetweenSpells;

        return new Vector2(posX, posY);
    }
}
