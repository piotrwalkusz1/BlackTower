using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.Connection;

public abstract class Stats : MonoBehaviour, IStats
{
    public sealed override void Set(IStats stats)
    {
        Copier.CopyProperties(stats, this);
    }
}
