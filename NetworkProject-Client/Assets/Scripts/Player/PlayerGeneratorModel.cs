using UnityEngine;
using System.Collections;
using NetworkProject;
using NetworkProject.BodyParts;
using NetworkProject.Items;

public class PlayerGeneratorModel : MonoBehaviour
{
    private BreedAndGender BreedAndGender
    {
        get
        {
            return GetComponent<PlayerStats>().BreedAndGender;
        }
    }

    private PlayerMesh _playerMesh;

    public void CreateModel()
    {
        GameObject playerModel = GameObject.Instantiate(Prefabs.GetPlayerModelByBreed(BreedAndGender.Breed), Vector3.zero,
            Quaternion.identity) as GameObject;

        playerModel.transform.parent = transform;
        playerModel.transform.localPosition = Vector3.zero;
        playerModel.transform.localEulerAngles = Vector3.zero;

        GetComponent<PlayerAnimation>().SetAnimator();

        _playerMesh = playerModel.GetComponentInChildren<PlayerMesh>();

        UpdateEquipedItems();
    }

    public void UpdateEquipedItems()
    {
        var eq = GetComponent<PlayerEquipment>();
        var bodyParts = eq.GetBodyParts();

        for (int i = 0; i < bodyParts.Length; i++)
        {
            UpdateEquipedItem(bodyParts[i], i);
        }
    }

    public void UpdateEquipedItem(BodyPart bodyPart, int idBodyPart)
    {
        SkinnedMeshRenderer mesh = _playerMesh.GetItemMeshByIdBodyPart(idBodyPart);

        if (bodyPart.EquipedItem == null)
        {
            mesh.sharedMesh = null;
            return;
        }

        VisualEquipableItemData item = (VisualEquipableItemData)ItemRepository.GetItemByIdItem(bodyPart.EquipedItem.IdItem);

        mesh.sharedMesh = MeshRepository.GetItemMesh(item.IdPrefabOnPlayer);
        mesh.sharedMaterial = MeshRepository.GetItemMaterial(item.IdPrefabOnPlayer);
    }
}
