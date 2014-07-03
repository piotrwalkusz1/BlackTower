using UnityEngine;
using System.Collections;
using NetworkProject;

[System.CLSCompliant(false)]
public class NetOwnPlayer : NetPlayer
{
	protected void Update()
    {
		if (IsMovement())
        {
            Client.SendMessagePlayerMove(transform.position);
		}

        if (IsRotation())
        {
            Client.SendMessagePlayerRotation(transform.eulerAngles.y);
        }
	}

    public void Dead(OwnDeadEventPackage deadInfo)
    {
        GetComponent<HP>()._hp = 0;

        DoWindowGUIContent content = delegate(Rect rect)
        {
            GUI.TextArea(new Rect(5, 10, rect.width - 10, rect.height - 45), "Nie żyjesz!");

            if (GUI.Button(new Rect(5, rect.height - 30, rect.width - 10, 25), "Wskrześ"))
            {
                Client.SendMessageRespawn();

                return true;
            }

            return false;
        };

        MessageGUI message = new MessageGUI("", content);

        GUIController.ShowWindow(message);
    }

    public void InitializePlayer(OwnPlayerPackage package)
    {
        IdObject = package.IdObject;
        Name = package.Name;

        var stats = GetComponent<OwnPlayerStats>();
        stats.Set(package.Stats);
    }
}
