using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject.Buffs;

public interface IBuffable
{
    void AddChild(GameObject child, Vector3 localPosition, Quaternion localRotation);

    void SetVisibleModel(bool visible);

    GameObject GetGameObject();
} 
