using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Buffs;
using NetworkProject.Connection.ToClient;

public class PlayerBuff : MonoBehaviour, IBuffable
{
    protected List<Buff> _buffs;

    protected void Awake()
    {
        _buffs = new List<Buff>();
    }

    private void Update()
    {
        foreach (var buff in _buffs.ToArray())
        {
            if (DateTime.UtcNow > buff.EndTime)
            {
                buff.OnEnd(this);

                _buffs.Remove(buff);
            }
        }
    }

    public void AddBuff(Buff buff)
    {
        _buffs.Add(buff);

        buff.OnCast(this);
    }

    public void AddChild(GameObject child, Vector3 localPosition, Quaternion localRotation)
    {
        child.transform.parent = transform;
        child.transform.localPosition = localPosition;
        child.transform.localRotation = localRotation;
    }

    public void SetVisibleModel(bool visible)
    {
        var netObject = GetComponent<NetObject>();

        netObject.IsModelVisible = visible;

        netObject.SendVisibleModelMessage();

        if (netObject is NetPlayer)
        {
            NetPlayer netPlayer = (NetPlayer)netObject;

            var request = new ChangeVisibilityModelToClient(netPlayer.IdNet, visible);

            Server.SendRequestAsMessage(request, netPlayer.OwnerAddress);
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ApplyToStats(IPlayerStats stats)
    {
        _buffs.ForEach(x => x.ApplyToStats(stats));
    }
}
