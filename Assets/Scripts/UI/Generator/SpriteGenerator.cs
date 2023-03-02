using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprites;
    [SerializeField] private Sprite[] raritySprites;

    private readonly string savePathSprite = "Assets/Data/Sprites";
    private readonly string savePathScriptable = "Assets/Data/Objects";
    
    private void Start()
    {
        MergePNGs();
        FilterAndPixelPreset();
       
    }

    private void MergePNGs()
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

        byte[] bytes = finalSprite.texture.EncodeToPNG();
        File.WriteAllBytes(savePathSprite + "/" + finalSprite.name + ".png", bytes);
        AssetDatabase.Refresh();

        CreateScriptableObject(AssetDatabase.LoadAssetAtPath<Sprite>(savePathSprite + "/" + finalSprite.name + ".png"));
    }


    private void FilterAndPixelPreset()
    {
        foreach(var sprite in Directory.GetFiles(savePathSprite).Where(item => !item.EndsWith(".meta")))
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(sprite) as TextureImporter;
            if(textureImporter != null)
            {
                textureImporter.filterMode = FilterMode.Point;
                textureImporter.spritePixelsPerUnit = 32;
                textureImporter.SaveAndReimport();
            }
        }
    }

    private void CreateScriptableObject(Sprite sprite)
    {
        ItemScriptableObj itemScriptableObj;
        itemScriptableObj = ScriptableObject.CreateInstance<ItemScriptableObj>();
        itemScriptableObj.Name = sprite.name;
        itemScriptableObj.Sprite = sprite;

        AssetDatabase.CreateAsset(itemScriptableObj, savePathScriptable + "/" + itemScriptableObj.Name + ".asset");
    }

    private void RemoveSprite(string spriteAssetPath)
    {
        AssetDatabase.DeleteAsset(spriteAssetPath);

        // Remove the texture asset (if any)
        var texturePath = spriteAssetPath.Replace(".sprite", ".png");
        AssetDatabase.DeleteAsset(texturePath);

        // Remove the meta file (if any)
        var metaPath = spriteAssetPath + ".meta";
        if (File.Exists(metaPath))
        {
            File.Delete(metaPath);
        }

        // Refresh the Asset Database to reflect the changes
        AssetDatabase.Refresh();
    }
    
    private void RemoveScriptableObject(string scriptableObjPath)
    {
        AssetDatabase.DeleteAsset(scriptableObjPath);
    }

}
