using UnityEngine;
using System.Collections;
using InputsSystem;
using NetworkProject.Connection.ToServer;

public class InputPlayer : MonoBehaviour 
{
    public MouseLook _playerAxis;

	private CharacterMotor movement;
	
	private float rateSpeed;
	private float directionRotate;

    private bool _lookRotationByKey;
    private bool _lookRotationByGUI;

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
        if (Inputs.DownButton("Quests"))
        {
            if (GUIController.IsQuestManagerActive())
            {
                GUIController.HideQuestManager();
            }
            else
            {
                GUIController.ShowQuestManager();
            }
        }
        if (Input.GetKeyDown(InputsK.magicBook))
        {
            if (GUIController.IsMagicBookActive())
            {
                GUIController.HideMagicBook();
            }
            else
            {
                GUIController.ShowMagicBook();
            }
        }
        if (Inputs.DownButton("Mouse"))
        {
            SetLockRotationByKey(true);
            SetCombatAssent(false);
        }
        if (Inputs.UpButton("Mouse"))
        {
            SetLockRotationByKey(false);
            SetCombatAssent(true);
        }
        if (Input.GetKeyDown(InputsK.menu))
        {
            GUIController.ShowWindow(new MessageGUI("Menu",
                delegate(Rect position)
                {
                    if (GUILayout.Button("Wyloguj"))
                    {
                        ClientController.Logout();
                        return true;
                    }
                    if (GUILayout.Button("Wyjdź z gry"))
                    {
                        ClientController.Logout();
                        ApplicationControler.Quit();
                        return true;
                    }
                    if (GUILayout.Button("Wróć"))
                    {
                        return true;
                    }

                    return false;
                }));
        }
	}

    public void SetLockRotationByGUI(bool isLock)
    {
        _lookRotationByGUI = isLock;

        RefreshLockRotation();
    }

    private void SetLockRotationByKey(bool isLock)
    {
        _lookRotationByKey = isLock;

        RefreshLockRotation();
    }

    private void SetCombatAssent(bool isAssent)
    {
        GetComponent<OwnPlayerCombat>().IsMouseLockAssent = isAssent;
    }

    private void RefreshLockRotation()
    {
        if (_lookRotationByKey || _lookRotationByGUI)
        {
            LockRotation();
        }
        else
        {
            UnlockRotation();
        }
    }

    private void LockRotation()
    {
        GetComponent<MouseLook>().enabled = false;
        _playerAxis.enabled = false;
    }

    private void UnlockRotation()
    {
        GetComponent<MouseLook>().enabled = true;
        _playerAxis.enabled = true;
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
