using UnityEngine;
using System.Collections;

[System.CLSCompliant(false)]
public class ItemDrop
{
    public int _idItem;
    public float _chance;

    public ItemDrop(int idItem, float chance)
    {
        _idItem = idItem;
        _chance = chance;
    }
}
