using UnityEngine;
using System.Collections;
using NetworkProject;

public class PlayerGeneratorModel : MonoBehaviour
{
    private Breed Breed
    {
        get
        {
            return GetComponent<PlayerStats>().Breed;
        }
    }

    private PlayerMesh _playerMesh;

    public void CreateModel()
    {
        GameObject playerModel = Prefabs.GetPlayerModel(Breed);

        playerModel = GameObject.Instantiate(playerModel, Vector3.zero, Quaternion.identity) as GameObject;

        playerModel.transform.parent = transform;
        playerModel.transform.localPosition = Vector3.zero;
        playerModel.transform.localEulerAngles = Vector3.zero;

        GetComponent<PlayerAnimation>().animator = playerModel.GetComponentInChildren<Animator>();

        _playerMesh = playerModel.GetComponentInChildren<PlayerMesh>();

        UpdateEquipedItems();
    }

    public void UpdateEquipedItems()
    {
        var eq = (IEquippedItems)GetComponent(typeof(IEquippedItems));

        UpdateWeapon(eq.Weapon);
        UpdateArmor(eq.Armor);
    }

    public void UpdateWeapon(Item weapon)
    {
        if (weapon == null)
        {
            _playerMesh._weapon.sharedMesh = null;
            return;
        }

        ItemData weaponData = ItemBase.GetWeapon(weapon.IdItem);

        _playerMesh._weapon.sharedMesh = MeshRepository.GetItemMeshByBreed(Breed, weaponData._idPrefabOnPlayer);
        _playerMesh._weapon.material = MeshRepository.ItemMaterials[weaponData._idPrefabOnPlayer];
    }

    public void UpdateArmor(Item armor)
    {
        if (armor == null)
        {
            _playerMesh._armor.sharedMesh = MeshRepository.GetCorpMeshByBreed(Breed);
            _playerMesh._armor.sharedMaterials = new Material[] { MeshRepository.GetCorpMaterialByBreed(Breed) };
            return;
        }

        ItemData armorData = ItemBase.GetArmor(armor.IdItem);

        _playerMesh._armor.sharedMesh = MeshRepository.GetItemMeshByBreed(Breed, armorData._idPrefabOnPlayer);
        _playerMesh._armor.sharedMaterials = new Material[] {
            MeshRepository.GetCorpMaterialByBreed(Breed),
            MeshRepository.ItemMaterials[armorData._idPrefabOnPlayer]
        };
    }
}
