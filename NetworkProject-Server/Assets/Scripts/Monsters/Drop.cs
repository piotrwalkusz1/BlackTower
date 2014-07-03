using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.CLSCompliant(false)]
public class Drop : MonoBehaviour
{
    public List<ItemDrop> _itemsDrop = new List<ItemDrop>(); 

    void OnDead()
    {
        DropItems();
    }

    public void AddItemToDrop(ItemDrop item)
    {
        _itemsDrop.Add(item);
    }

    public void DropItems()
    {
        foreach (ItemDrop item in _itemsDrop)
        {
            if (DrawLots(item._chance))
            {
                Vector3? position = CalculatePosition();

                if (position == null)
                {
                    print("Nie znaleziono pozycji dla itemu");
                }
                else
                {
                    SceneBuilder.CreateItem(item._idItem, position.Value);
                }
            }
        }
    }

    private bool DrawLots(float chance)
    {
        return (chance >= GenerateRandomNumber());
    }

    private float GenerateRandomNumber()
    {
        return UnityEngine.Random.Range(0f, 100f);
    }

    private Vector3? CalculatePosition()
    {
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        RaycastHit hitInfo;

        float x = UnityEngine.Random.Range(-Standard.Settings.maxDropDistance, Standard.Settings.maxDropDistance);
        float z = UnityEngine.Random.Range(-Standard.Settings.maxDropDistance, Standard.Settings.maxDropDistance);

        Vector3 position = transform.position + new Vector3(x, 0, z);

        if (Physics.Raycast(position, Vector3.down, out hitInfo, Mathf.Infinity, layerMask))
        {
            Vector3 offset = transform.position - hitInfo.point;

            if (offset.sqrMagnitude > 4 * Standard.Settings.maxDropDistance * Standard.Settings.maxDropDistance)
            {
                return CalculatePosition2();
            }
            else
            {
                return hitInfo.point + Vector3.up * Standard.Settings.distanceBetweenDroppedItemAndGround;
            }
        }
        else
        {
            return CalculatePosition2();
        }
    }

    private Vector3? CalculatePosition2()
    {
        LayerMask layerMask = LayerMask.GetMask("Terrain");
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity, layerMask))
        {
            return hitInfo.point + Vector3.up * Standard.Settings.distanceBetweenDroppedItemAndGround;
        }
        else
        {
            return null;
        }
    }
}
