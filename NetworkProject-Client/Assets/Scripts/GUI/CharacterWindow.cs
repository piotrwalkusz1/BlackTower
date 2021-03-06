﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Standard;

public class CharacterWindow : GUIObject, IClosable
{
    public List<ItemInCharacterWindow> _equipedItems;

    public Texture Texture
    {
        get
        {
            return GetComponent<GUITexture>().texture;
        }
        set
        {
            GetComponent<GUITexture>().texture = value;
        }
    }  
    public OwnPlayerEquipment PlayerEquipement
    {
        get
        {
            return _stats.GetComponent<OwnPlayerEquipment>();
        }
    }


    public GUIText _lvl;
    public GUIText _lvlAnswer;
    public GUIText _exp;
    public GUIText _expAnswer;
    public GUIText _power;
    public GUIText _powerAnswer;
    public GUIText _defense;
    public GUIText _defenseAnswer;
    public GUIText _cooldownReduction;
    public GUIText _cooldownReductionAnswer;
    public GUIText _regenerationHP;
    public GUIText _regenerationHPAnswer;
    public GUIText _regenerationMP;
    public GUIText _regenerationMPAnswer;
    public GUIText _attackSpeed;
    public GUIText _attackSpeedAnswer;
    public GUIText _damage;
    public GUIText _damageAnswer;
    public GUIText _movementSpeed;
    public GUIText _movementSpeedAnswer;

    private OwnPlayerStats _stats; 

    private Vector3 _lastMousePosition;

    void Awake()
    {
        _lastMousePosition = Input.mousePosition;
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

    public void SetStats(OwnPlayerStats stats)
    {
        _stats = stats;
    }

    public void Refresh()
    {
        try
        {
            SetStats(Client.GetNetOwnPlayer().GetComponent<OwnPlayerStats>());

            _lvl.text = Languages.GetPhrase("lvl");
            _lvlAnswer.text = _stats.Lvl.ToString();
            _exp.text = Languages.GetPhrase("exp");
            _expAnswer.text = _stats.Exp.ToString() + "/" + _stats.MaxExp.ToString();
            _power.text = Languages.GetPhrase("power");
            _defense.text = Languages.GetPhrase("defense");
            _defenseAnswer.text = _stats.Defense.ToString();
            _cooldownReduction.text = Languages.GetPhrase("cooldown reduction");
            _regenerationHP.text = Languages.GetPhrase("hp regeneration");
            _regenerationHPAnswer.text = _stats.HPRegeneration.ToString();
            _regenerationMP.text = Languages.GetPhrase("mp regeneration");
            _regenerationMPAnswer.text = _stats.ManaRegeneration.ToString();
            _attackSpeed.text = Languages.GetPhrase("attack speed");
            _attackSpeedAnswer.text = _stats.AttackSpeed.ToString();
            _damage.text = Languages.GetPhrase("damage");
            _damageAnswer.text = _stats.MinDmg.ToString() + " - " + _stats.MaxDmg.ToString();
            _movementSpeed.text = Languages.GetPhrase("movement speed");
            _movementSpeedAnswer.text = _stats.MovementSpeed.ToString();

            _equipedItems.ForEach(x => x.RefreshTexture());
        }
        catch
        {
            MonoBehaviour.print("Wystąił błąd prz odświeżaniu character window");
        }
    }

    public int GetSlotToItem(ItemInCharacterWindow item)
    {
        for (int i = 0; i < _equipedItems.Count; i++)
        {
            if (_equipedItems[i] == item)
            {
                return i;
            }   
        }

        return -1;
    }

    public OwnPlayerStats GetPlayerStats()
    {
        return _stats;
    }

    public OwnPlayerEquipment GetEquipment()
    {
        return _stats.GetComponent<OwnPlayerEquipment>();
    }

    public void Close()
    {
        GUIController.HideCharacterGUI();
    }
}
