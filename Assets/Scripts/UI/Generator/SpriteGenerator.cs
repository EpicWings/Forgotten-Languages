using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D itemTexture;
    [SerializeField] private Texture2D rarityTexture;
    [SerializeField] private string path = "Assets/Data/Sprites";

    private void Start()
    {
        GenerateSprites();
    }
    private Sprite GenerateSprite(Texture2D itemTexture, Texture2D rarityTexture)
    {
        Sprite sprite = null;
        var texture = new Texture2D(itemTexture.width, itemTexture.height);
        var pixels = itemTexture.GetPixels();
        var rarityPixels = rarityTexture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (rarityPixels[i].a > 0)
            {
                pixels[i] = rarityPixels[i];
            }
        }
        texture.SetPixels(pixels);
        texture.Apply();
        var bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path + "/item.png", bytes);
        return sprite;
    }

    private void GenerateSprites()
    {
        var sprite = GenerateSprite(itemTexture, rarityTexture);
    }
}
