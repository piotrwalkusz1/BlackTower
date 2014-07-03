using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.CLSCompliant(false)]
public class Tex
{
    public int _id;
    public Texture2D _image;

    public Tex(int id, byte[] image)
    {
        this._id = id;
        _image = new Texture2D(1, 1);
        _image.LoadImage(image);
    }

    public Tex(int id, Texture2D image)
    {
        _id = id;
        _image = image;
    }
}

[System.CLSCompliant(false)]
public static class ImagesBase
{		
	static public List<Tex> _images;

    static ImagesBase()
    {
        _images = new List<Tex>();
    }

    static public Texture2D GetImage(int idImage)
    {
        foreach (Tex im in _images)
        {
            if (im._id == idImage)
            {
                return im._image;
            }
        }

        return null;
    }

    static public void AddImage(int idImage, Texture2D image)
    {
        Tex im = new Tex(idImage, image);

        _images.Add(im);
    }
	
	static public void AddImage( int idImage, byte[] image ) 
    {	
		Tex im = new Tex( idImage, image );

		_images.Add( im );
	}
}
