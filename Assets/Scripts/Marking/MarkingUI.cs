using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkingUI : MonoBehaviour
{
    public static MarkingUI Instance;
    public Texture[] textures;
    public RawImage images;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ChangedImage(Marking.I.index);
    }

    public void ChangedImage(int val)
    {
        if(val == 0)
        {
            images.texture = textures[0];
        }
       else if (val == 1)
        {
            images.texture = textures[1];
        }
       else if (val == 2)
        {
            images.texture = textures[2];
        }
        else if (val == 3)
        {
            images.texture = textures[3];
        }
    }
}
