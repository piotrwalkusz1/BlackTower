using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

public class MeshRepository : MonoBehaviour
{
    public List<Mesh> ItemsMesh;
    public List<Material> ItemsMaterials;

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
}
