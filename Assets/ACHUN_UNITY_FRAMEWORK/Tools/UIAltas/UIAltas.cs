using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAltas : ScriptableObject
{
    public Texture2D mainTex;

    public List<Sprite> sprites = new List<Sprite>();


    public Sprite GetSprite(string spriteName)
    {
        return sprites.Find((Sprite s) => { return s.name == spriteName; });
    }


    public void SetSprite(ref Image im,string spriteName)
    {
        if (im == null)
            return;
        Sprite sp = GetSprite(spriteName);
        if (sp != null)
        {
            im.overrideSprite = sp;
        }
        else
        {
            Debug.LogError("");
        }
    }
}
