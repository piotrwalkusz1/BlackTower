using UnityEngine;
using System.Collections;
using InputsSystem;
using NetworkProject.Connection.ToServer;

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

            var request = new PlayerJumpToServer();

            Client.SendRequestAsMessage(request);
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
                var request = new PickItemToServer(item.IdNet);

                Client.SendRequestAsMessage(request);
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<SpellCaster>().TryCastSpellFromSpellBook(0);
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
