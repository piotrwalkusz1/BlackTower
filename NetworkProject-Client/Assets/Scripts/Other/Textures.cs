using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.CLSCompliant(false)]
public class Textures : MonoBehaviour
{
    public Texture2D _equipmentWindow;
    public Texture2D _characterWindow;

    public List<Texture2D> _texturesToImageBase;

    void Awake()
    {
        for (int i = 0; i < _texturesToImageBase.Count; i++)
        {
            ImagesBase.AddImage(i, _texturesToImageBase[i]);
        }

        StaticRepository.Textures = this;
    }
}
