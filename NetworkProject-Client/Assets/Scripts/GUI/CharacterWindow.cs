using UnityEngine;
using System.Collections;
using Standard;

[System.CLSCompliant(false)]
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
    public PlayerStats _stats;
    public PlayerEquipement PlayerEquipement
    {
        get
        {
            return _stats.GetComponent<PlayerEquipement>();
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

    public ItemInCharacterWindow _head;
    public ItemInCharacterWindow _chest;
    public ItemInCharacterWindow _shoes;
    public ItemInCharacterWindow _rightHand;
    public ItemInCharacterWindow _leftHand;
    public ItemInCharacterWindow _Addition1;
    public ItemInCharacterWindow _Addition2;

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

    public void Refresh()
    {
        _hp.text = Languages.GetSentence("hp");
        _hpAnswer.text = _stats.HP.ToString() + "/" + _stats.MaxHP.ToString();
        _mp.text = Languages.GetSentence("mp");
        _power.text = Languages.GetSentence("power");
        _defense.text = Languages.GetSentence("defense");
        _defenseAnswer.text = _stats.Defense.ToString();
        _cooldownReduction.text = Languages.GetSentence("cooldownReduction");
        _regenerationHP.text = Languages.GetSentence("hpRegeneration");
        _regenerationHPAnswer.text = _stats.RegenerationHP.ToString();
        _regenerationMP.text = Languages.GetSentence("mpRegeneration");
        _regenerationMPAnswer.text = _stats.RegenerationMP.ToString();
        _attackSpeed.text = Languages.GetSentence("attackSpeed");
        _attackSpeedAnswer.text = _stats.AttackSpeed.ToString();
        _movementSpeed.text = Languages.GetSentence("movementSpeed");
        _movementSpeedAnswer.text = _stats.MovementSpeed.ToString();
    }
}
