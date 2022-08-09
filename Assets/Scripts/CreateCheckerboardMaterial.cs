using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCheckerboardMaterial : MonoBehaviour
{
    private Texture2D mainTexture;
    
    [SerializeField] private int mainTexWidth;
    [SerializeField] private int mainTexHeight;

    void Start()
    {
        SetMainTextureSize();
        CreatePattern();
    }

    void SetMainTextureSize()
    {
        mainTexture = new Texture2D(mainTexWidth, mainTexHeight);
    }

    void CreatePattern()
    {
        for (int x = 0; x < mainTexWidth; x++)
        {
            for (int y = 0; y < mainTexHeight; y++)
            {
                if (((x + y) % 2) == 1)
                {
                    mainTexture.SetPixel(x, y, Color.black);
                }
                else
                {
                    mainTexture.SetPixel(x, y, Color.white);
                }
            }
        }
        mainTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = mainTexture;
        mainTexture.wrapMode = TextureWrapMode.Clamp;
        mainTexture.filterMode = FilterMode.Point;
    }
}
