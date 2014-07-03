using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

public class MeshRepository : MonoBehaviour
{
    public List<Mesh> _charactersCorps;
    public List<Material> _characterCorpMaterial;

    public List<Mesh> _itemsOnTroll;
    public List<Material> _itemMaterials;

    public static List<Mesh> ItemsOnTroll
    {
        get
        {
            return _repository._itemsOnTroll;
        }
        set
        {
            _repository._itemsOnTroll = value;
        }
    }
    public static List<Material> ItemMaterials
    {
        get
        {
            return _repository._itemMaterials;
        }
        set
        {
            _repository._itemMaterials = value;
        }
    }

    private static MeshRepository _repository;

    public void Awake()
    {
        _repository = this;
    }

    public static Mesh GetItemMeshByBreed(Breed breed, int idPrefab)
    {
        switch (breed)
        {
            case Breed.Troll:
                return ItemsOnTroll[idPrefab];
            default:
                throw new System.Exception("Nie ma takiej rasy.");
        }
    }

    public static Mesh GetCorpMeshByBreed(Breed breed)
    {
        return _repository._charactersCorps[(int)breed];
    }

    public static Material GetCorpMaterialByBreed(Breed breed)
    {
        return _repository._characterCorpMaterial[(int)breed];
    }
}
