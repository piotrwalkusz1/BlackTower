using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject.Items;

public class Drop : MonoBehaviour
{
    private List<ItemDrop> _itemsDrop = new List<ItemDrop>(); 

    void OnDead()
    {
        DropItems();
    }

    public void AddItemToDrop(ItemDrop item)
    {
        _itemsDrop.Add(item);
    }

    public void AddItemToDrop(ItemDrop[] items)
    {
        _itemsDrop.AddRange(items);
    }

    public void DropItems()
    {
        foreach (ItemDrop drop in _itemsDrop)
        {
            if (DrawLots(drop.Chances))
            {
                Vector3? position = CalculatePosition();

                if (position == null)
                {
                    print("Nie znaleziono pozycji dla itemu");
                }
                else
                {
                    SceneBuilder.CreateItem(drop.Item.IdItem, position.Value);
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

        Vector3 position = transform.position + new Vector3(x, 0.5f, z);

        if (Physics.Raycast(position, Vector3.down, out hitInfo, Mathf.Infinity, layerMask))
        {
            Vector3 offset = transform.position - hitInfo.point;

            return hitInfo.point + Vector3.up * Standard.Settings.distanceBetweenDroppedItemAndGround;
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

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hitInfo, Mathf.Infinity, layerMask))
        {
            return hitInfo.point + Vector3.up * Standard.Settings.distanceBetweenDroppedItemAndGround;
        }
        else
        {
            return null;
        }
    }
}
