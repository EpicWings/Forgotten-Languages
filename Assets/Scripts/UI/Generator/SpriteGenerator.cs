using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprites;
    [SerializeField] private Sprite[] raritySprites;
    
    private readonly string savePathSprite = "Assets/Data/Sprites";
    private readonly string savePathScriptable = "Assets/Data/Objects";

    private void Start()
    {
        Merge();
        ApplyChanges();
    }


    private void Merge()
    {
        Resources.UnloadUnusedAssets();
        var texture = new Texture2D(32, 32);

        for (int i = 0; i < texture.width; i++)
            for (int j = 0; j < texture.height; j++)
                texture.SetPixel(i, j, Color.clear);

        int itemRandomIndex = Random.Range(0, itemSprites.Length);
        int rarityRandomIndex = Random.Range(0, raritySprites.Length);

        var itemSprite = itemSprites[itemRandomIndex];
        var raritySprite = raritySprites[rarityRandomIndex];

        for (int i = 0; i < raritySprite.texture.width; i++)
            for (int j = 0; j < raritySprite.texture.height; j++)
            {
                var color = raritySprite.texture.GetPixel(i, j);
                if (color.a > 0)
                    texture.SetPixel(i, j, color);
            }

        for (int i = 0; i < itemSprite.texture.width; i++)
            for (int j = 0; j < itemSprite.texture.height; j++)
            {
                var color = itemSprite.texture.GetPixel(i, j);
                if (color.a > 0)
                    texture.SetPixel(i, j, color);
            }

        texture.Apply();
        var finalSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 32);
        finalSprite.name = itemSprite.name + "_" + raritySprite.name;

        var bytes = finalSprite.texture.EncodeToPNG();
        File.WriteAllBytes(savePathSprite + "/" + finalSprite.name + ".png", bytes);
    }

    private void ApplyChanges()
    {
        foreach (var file in Directory.GetFiles(savePathSprite, "*.png").Where(file => !file.EndsWith(".meta")))
        {
            var importer = AssetImporter.GetAtPath(file) as TextureImporter;
            importer.textureType = TextureImporterType.Sprite;
            importer.spritePixelsPerUnit = 32;
            importer.filterMode = FilterMode.Point;
            importer.SaveAndReimport();
        }
        
    }

    private void CreateScriptableObject()
    {
        ItemScriptableObj itemScriptableObj;
        itemScriptableObj = ScriptableObject.CreateInstance<ItemScriptableObj>();
        itemScriptableObj.Name = "Test";

        AssetDatabase.CreateAsset(itemScriptableObj, savePathScriptable + "/" + itemScriptableObj.Name + ".asset");
    }

    public void RemoveSprite()
    {
        //...
    }

}
