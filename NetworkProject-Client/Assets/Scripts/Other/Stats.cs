using UnityEngine;
using System.Collections;
using NetworkProject;

public abstract class Stats : MonoBehaviour
{
    public abstract void Set(IncomingMessage message);
}
