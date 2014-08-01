using UnityEngine;
using System.Collections;
using NetworkProject.Connection.ToClient;

public class NetObject : MonoBehaviour
{
	public int IdNet { get; set; }
    public bool IsModelVisible
    {
        get
        {
            return _isModelVisible;
        }
        set
        {
            _isModelVisible = value;

            PlayerGeneratorModel playerGeneratorModel = GetComponent<PlayerGeneratorModel>();
            if (playerGeneratorModel != null)
            {
                if (_isModelVisible)
                {
                    playerGeneratorModel.ShowModel();
                }
                else
                {
                    playerGeneratorModel.HideModel();
                }
            }
        }
    }

    private bool _isModelVisible;

    public void Respawn(RespawnToClient respawnInfo)
    {
        transform.position = respawnInfo.Position;
    }
}
