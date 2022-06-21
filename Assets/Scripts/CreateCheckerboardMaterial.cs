using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCheckerboardMaterial : MonoBehaviour
{
    [SerializeField] private int mainTexWidth;
    [SerializeField] private int mainTexHeight;
    private Texture2D mainTexture;

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
        for (int i = 0; i < mainTexWidth; i++)
        {
            for (int j = 0; j < mainTexHeight; j++)
            {
                if (((i + j) % 2) == 1)
                {
                    mainTexture.SetPixel(i, j, Color.black);
                }
                else
                {
                    mainTexture.SetPixel(i, j, Color.white);
                }
            }
        }
        mainTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = mainTexture;
        mainTexture.wrapMode = TextureWrapMode.Clamp;
        mainTexture.filterMode = FilterMode.Point;
    }
}
