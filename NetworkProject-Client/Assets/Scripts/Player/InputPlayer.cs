using UnityEngine;
using System.Collections;
using InputsSystem;

[System.CLSCompliant(false)]
public class InputPlayer : MonoBehaviour 
{
	private CharacterMotor movement;
	
	private float rateSpeed;
	private float directionRotate;

	void Start ()
    {
		movement = GetComponent<CharacterMotor>();
	}
	
	void Update ()
    {
        Vector3 direction = new Vector3(Inputs.GetAxis("Horizontal"), 0f, Inputs.GetAxis("Vertical"));

        movement.inputMoveDirection = transform.TransformDirection(direction);

        if (Inputs.DownButton("Jump"))
        {
            movement.inputJump = true;

            Client.SendMessagePlayerJump(transform.position, Vector3.zero);
        }
        if (Inputs.UpButton("Jump"))
        {
            movement.inputJump = false;
        }
        if (Inputs.DownButton("PickItem"))
        {
            float distance;

            NetItem item = FindNearestItem(out distance);

            if (item != null && distance <= NetworkProject.Settings.pickItemRange)
            {
                Client.SendMessagePickItem(item.IdObject);
            }
        }
        if (Inputs.DownButton("Equipment"))
        {
            if (GUIController.IsEquipmentActive())
            {
                GUIController.HideEquipment();
            }
            else
            {
                GUIController.ShowEquipment();
            }
        }
        if (Inputs.DownButton("Character"))
        {
            if (GUIController.IsCharacterGUIActive())
            {
                GUIController.HideCharacterGUI();
            }
            else
            {
                GUIController.ShowCharacterGUI();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spell spell = GetComponent<SpellCaster>().GetSpellById(0);

            GetComponent<SpellCaster>().CastSpell(spell);
        }
	}

    NetItem FindNearestItem(out float distance)
    {
        NetItem[] items = GameObject.FindObjectsOfType<NetItem>() as NetItem[];

        float sqrDistance;
        Vector3 offset;
        float smallestSqrDistance = Mathf.Infinity;
        NetItem nearestItem = null;

        foreach (NetItem item in items)
        {
            offset = transform.position - item.transform.position;

            sqrDistance = offset.sqrMagnitude;

            if (sqrDistance < smallestSqrDistance)
            {
                nearestItem = item;
                smallestSqrDistance = sqrDistance;
            }
        }

        distance = Mathf.Sqrt(smallestSqrDistance);

        return nearestItem;
    }
}
