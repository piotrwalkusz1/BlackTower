using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMesh : MonoBehaviour
{
    public List<SkinnedMeshRenderer> _item;

    public SkinnedMeshRenderer GetItemMeshByIdBodyPart(int idBodyPart)
    {
        return _item[idBodyPart];
    }
}
