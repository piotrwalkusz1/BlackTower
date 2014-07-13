using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Standard;

public class CharacterWindow : GUIObject
{
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

    public GUIText _hp;
    public GUIText _hpAnswer;
    public GUIText _mp;
    public GUIText _mpAnswer;
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
    public GUIText _movementSpeed;
    public GUIText _movementSpeedAnswer;

    private OwnPlayerStats _stats;
    private List<ItemInCharacterWindow> _equipedItems;

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
        _hp.text = Languages.GetPhrase("hp");
        _hpAnswer.text = _stats.HP.ToString() + "/" + _stats.MaxHP.ToString();
        _mp.text = Languages.GetPhrase("mp");
        _power.text = Languages.GetPhrase("power");
        _defense.text = Languages.GetPhrase("defense");
        _defenseAnswer.text = _stats.Defense.ToString();
        _cooldownReduction.text = Languages.GetPhrase("cooldownReduction");
        _regenerationHP.text = Languages.GetPhrase("hpRegeneration");
        _regenerationHPAnswer.text = _stats.HPRegeneration.ToString();
        _regenerationMP.text = Languages.GetPhrase("mpRegeneration");
        //_regenerationMPAnswer.text = _stats.RegenerationMP.ToString();
        _attackSpeed.text = Languages.GetPhrase("attackSpeed");
        _attackSpeedAnswer.text = _stats.AttackSpeed.ToString();
        _movementSpeed.text = Languages.GetPhrase("movementSpeed");
        _movementSpeedAnswer.text = _stats.MovementSpeed.ToString();
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
}
