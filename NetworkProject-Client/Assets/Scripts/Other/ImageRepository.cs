using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject.Items;

public class ImageRepository : MonoBehaviour
{	
	public List<Texture2D> _images;

    public Texture2D _equipmentWindow;
    public Texture2D _characterWindow;

    public static Texture2D EquipmentWindow
    {
        get { return _repository._equipmentWindow; }
        set { _repository._equipmentWindow = value; }
    }
    public static Texture2D CharacterWindow
    {
        get { return _repository._characterWindow; }
        set { _repository._characterWindow = value; }
    }

    

    private static ImageRepository _repository;

    void Awake()
    {
        if (_repository == null)
        {
            _repository = this;
        }
        else
        {
            MonoBehaviour.print("Image repository has already been assigned. Check whether you haven't two ImageRepository on scene.");
        }
    }

    static public Texture2D GetImageByIdImage(int idImage)
    {
        return _repository._images[idImage];
    }

    public static Texture2D GetImageByItem(Item item)
    {
        VisualItemData itemData = (VisualItemData)ItemRepository.GetItemByIdItem(item.IdItem);

        return GetImageByIdImage(itemData.IdTexture);
    }
}
