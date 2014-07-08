using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

public class MeshRepository : MonoBehaviour
{
    public List<Mesh> ItemsMesh;
    public List<Material> ItemsMaterials;

    public List<Mesh> TrollCorpMesh;
    public List<Material> TrollCorpMaterials;

    private static MeshRepository _repository;

    public void Awake()
    {
        _repository = this;
    }

    public static Mesh GetItemMesh(int id)
    {
        return _repository.ItemsMesh[id];
    }

    public static Material GetItemMaterial(int id)
    {
        return _repository.ItemsMaterials[id];
    }

    public static Mesh GetCorpMesh(BreedAndGender breedAndGender, int id)
    {
        switch (breedAndGender.Breed)
        {
            case 0:
                return _repository.TrollCorpMesh[id];
            default:
                throw new System.Exception("Nie ma takiej rasy.");
        }
    }

    public static Material GetCorpMaterial(BreedAndGender breedAndGender, int id)
    {
        switch (breedAndGender.Breed)
        {
            case 0:
                return _repository.TrollCorpMaterials[id];
            default:
                throw new System.Exception("Nie ma takiej rasy.");
        }
    }
}
