using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NetworkProject.Items;

public class ImageRepository : MonoBehaviour
{	
	public List<Texture2D> _images;    

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
        if (idImage == -1)
        {
            return null;
        }
        else
        {
            return _repository._images[idImage];
        }       
    }

    public static Texture2D GetImageBySpell(Spell spell)
    {
        return GetImageByIdImage(spell.SpellData.IdImage);
    }

    public static Texture2D GetImageByItem(Item item)
    {
        return GetImageByIdImage(item.ItemData.IdTexture);
    }
}
