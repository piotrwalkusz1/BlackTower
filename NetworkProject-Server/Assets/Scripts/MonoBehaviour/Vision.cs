using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetworkProject;
using NetworkProject.Connection;

[System.CLSCompliant(false)]
public class Vision : MonoBehaviour
{
    public List<IConnectionMember> Observers { get; private set; }

    private List<NetObject> _seenNetObjects = new List<NetObject>();

    private int _map;

    void Awake()
    {
        Observers = new List<IConnectionMember>();
    }

    void Start()
    {
        _map = Standard.Settings.GetMap(transform.position);
    }

    public void UpdateInApplicationController()
    {
        NetObject[] netObjectsInRange = FindNetObjectsInRange();
        NetObject[] netObjectsVisible = SelectNetObjectsVisible(netObjectsInRange);
        NetObject[] netObjectsMustUpdate = SelectNetObjectsMustUpdate(netObjectsVisible);
        SendToObserversUpdate(netObjectsMustUpdate);

        NetObject[] netObjectsDisappeared = SelectNetObjectsDisappeared(netObjectsVisible);
        SendToObserversObjectsDisappeared(netObjectsDisappeared);

        NetObject[] netObjectsAppeared = SelectNetObjectsAppeared(netObjectsVisible);
        SendToObserversObjectsAppeared(netObjectsAppeared);
        
        _seenNetObjects = netObjectsVisible.ToList();
    }

    public void AddObserver(IConnectionMember observer)
    {
        Observers.Add(observer);
    }

    public void DeleteObserver(IConnectionMember observer)
    {
        Observers.Remove(observer);
    }

    private NetObject[] FindNetObjectsInRange()
    {
        var netObjects = GameObject.FindObjectsOfType(typeof(NetObject)) as IEnumerable<NetObject>;
        var netObjectsInRange = from obj in netObjects
                                where obj.GetMap() == _map
                                where IsInRange(obj.transform.position)                          
                                select obj;
        return netObjectsInRange.ToArray();
    }

    private NetObject[] SelectNetObjectsAppeared(NetObject[] currentSeenNetObjects)
    {
        var netObjectsAppeared = new List<NetObject>();
        bool isFound = false;

        foreach (NetObject currentSeenNetObject in currentSeenNetObjects)
        {
            isFound = false;

            foreach (NetObject netObject in _seenNetObjects)
            {
                if (netObject == currentSeenNetObject)
                {
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                netObjectsAppeared.Add(currentSeenNetObject);
            }
        }

        return netObjectsAppeared.ToArray();
    }

    private NetObject[] SelectNetObjectsDisappeared(NetObject[] currentSeenNetObjects)
    {
        var netObjectsDisappeared = new List<NetObject>();
        bool isFound = false;

        foreach (NetObject netObject in _seenNetObjects)
        {
            isFound = false;

            foreach (NetObject currentSeenNetObject in currentSeenNetObjects)
            {
                if (netObject == currentSeenNetObject)
                {
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                netObjectsDisappeared.Add(netObject);
            }
        }

        return netObjectsDisappeared.ToArray();
    }

    private NetObject[] SelectNetObjectsVisible(NetObject[] netObjects)
    {
        var netObjectsVisible = from netObject in netObjects
                  where netObject.IsVisible(this)
                  select netObject;

        return netObjectsVisible.ToArray();
    }

    private NetObject[] SelectNetObjectsMustUpdate(NetObject[] netObjects)
    {
        var netObjectsMusUpdate = from netObject in netObjects
                                  where netObject.IsMustUpdate()
                                  select netObject;

        return netObjectsMusUpdate.ToArray();
    }

    private void SendToObserversObjectsAppeared(NetObject[] netObjects)
    {
        foreach (IConnectionMember observer in Observers)
        {
            foreach (NetObject netObject in netObjects)
            {
                netObject.SendMessageAppeared(observer);
            }    
        }
    }

    private void SendToObserversObjectsDisappeared(NetObject[] netObjects)
    {
        foreach (IConnectionMember observer in Observers)
        {
            foreach (NetObject netObject in netObjects)
            {
                netObject.SendMessageDisappeared(observer);
            }
        }
    }

    private void SendToObserversUpdate(NetObject[] netObjects)
    {
        foreach (IConnectionMember observer in Observers)
        {
            foreach (NetObject netObject in netObjects)
            {
                netObject.SendMessageUpdate(observer);
            }
        }
    }

    private bool IsInRange(Vector3 position)
    {
        Vector3 offset = position - transform.position;
        return offset.sqrMagnitude < Standard.Settings.visionRange * Standard.Settings.visionRange;
    }
}
