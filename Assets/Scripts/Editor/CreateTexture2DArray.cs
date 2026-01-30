using UnityEditor;
using UnityEngine;

public static class CreateBaseTerrainArray
{
  [MenuItem("Assets/Create/Terrain/Create Base Terrain Array")]
  public static void CreateBaseArray()
  {
    Texture2D[] textures = new Texture2D[]
    {
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Base/Grass.png"),
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Base/Plains.png"),
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Base/Tundra.png"),
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Base/Snow.png"),
    };

    CreateArray(textures, "Assets/Textures/Arrays/BaseTerrainArray.asset", false);
  }

  [MenuItem("Assets/Create/Terrain/Create Overlay Terrain Array")]
  public static void CreateOverlayArray()
  {
    Texture2D[] textures = new Texture2D[]
    {
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Overlay/Forest.png"),
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Overlay/Hills.png"),
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Overlay/Jungle.png"),
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Overlay/Rough.png"),
      AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Textures/Overlay/Mountain.png"),
    };

    CreateArray(textures, "Assets/Textures/Arrays/OverlayTerrainArray.asset", true);
  }

  private static void CreateArray(Texture2D[] textures, string path, bool AlphaOn)
  {
    int width = textures[0].width;
    int height = textures[0].height;

    Texture2DArray array = new Texture2DArray(
      width,
      height,
      textures.Length,
      AlphaOn ? TextureFormat.RGBA32 : TextureFormat.RGB24,
      true
    );

    array.wrapMode = TextureWrapMode.Repeat;
    array.filterMode = FilterMode.Bilinear;

    for (int i = 0; i < textures.Length; i++)
    {
      Graphics.CopyTexture(textures[i], 0, 0, array, i, 0);
    }

    AssetDatabase.CreateAsset(array, path);
    AssetDatabase.SaveAssets();

    Selection.activeObject = array;
  }
}