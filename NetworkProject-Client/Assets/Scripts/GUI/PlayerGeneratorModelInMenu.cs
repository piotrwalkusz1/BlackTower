using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using NetworkProject;
using NetworkProject.Items;
using NetworkProject.BodyParts;

public class PlayerGeneratorModelInMenu : PlayerGeneratorModel
{
    public override BreedAndGender BreedAndGender
    {
        get { return _breedAndGender; }
    }

    private BreedAndGender _breedAndGender;

    public override void CreateModel()
    {
        GameObject playerModel = GameObject.Instantiate(Prefabs.GetPlayerModelByBreed(BreedAndGender.Breed), Vector3.zero,
            Quaternion.identity) as GameObject;

        playerModel.transform.parent = transform;
        playerModel.transform.localPosition = Vector3.zero;
        playerModel.transform.localEulerAngles = Vector3.zero;

        _playerModel = playerModel;
        _playerMesh = playerModel.GetComponentInChildren<PlayerMesh>();
    }

    public void SetBreedAndGender(BreedAndGender breedAndGender)
    {
        _breedAndGender = breedAndGender;
    }

    public void UpdateEquipedItems(PlayerEquipedItemsPackage items)
    {
        var bodyParts = PackageConverter.PackageToBodyPart(items.BodyParts.ToArray());

        for (int i = 0; i < bodyParts.Count; i++)
        {
            UpdateEquipedItem(bodyParts[i], i);
        }
    }
}
