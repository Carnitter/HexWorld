using UnityEngine;
using System.Collections.Generic;

public class HexGridTest : MonoBehaviour
{
  public int width = 32;
  public int height = 32;
  public int chunkSize = 16;

  public Material matGrass;
  public Material matPlains;
  public Material matTundra;
  public Material matSnow;

  public Material matForest;
  public Material matJungle;
  public Material matRough;
  public Material matHills;
  public Material matMountain;

  private HexGrid grid;
  private List<HexChunk> chunks;

  void Start()
  {
    grid = new HexGrid(width, height);
    chunks = new List<HexChunk>();

    

    Material material =
      new Material(Shader.Find("Sprites/Default"));
    material.enableInstancing = true;

    HexTerrainTestInitializer.ApplyTestPattern(grid);

    CreateChunks(material);
  }
  void CreateChunks(Material material)
  {
    int chunkCountX = Mathf.CeilToInt((float)width / chunkSize);
    int chunkCountY = Mathf.CeilToInt((float)height / chunkSize);

    Debug.Log($"Creating {chunkCountX} x {chunkCountY} chunks");

    for (int cx = 0; cx < chunkCountX; cx++)
    {
      for (int cy = 0; cy < chunkCountY; cy++)
      {
        Debug.Log($"Creating chunk {cx},{cy}");

        HexChunk chunk = new HexChunk(
         cx,
         cy,
         chunkSize,
         matPlains,   // temporary default
         null
       );

        for (int x = 0; x < chunkSize; x++)
        {
          for (int y = 0; y < chunkSize; y++)
          {
            int collumn = cx * chunkSize + x;
            int row = cy * chunkSize + y;

            HexCell cell = grid.GetCell(new HexCoord(collumn, row));
            if (cell != null)
            {
              chunk.AddCell(cell);
            }
          }
        }

        chunk.BuildMesh();
        AssignChunkMaterials(chunk, cx, cy);
        chunks.Add(chunk);
      }
    }
  }
  /*
  void AssignChunkMaterials(HexChunk chunk, int cx, int cy)
  {
    // Pick any cell in the chunk as representative (temporary)
    HexCell cell = grid.GetCell(new HexCoord(
      cx * chunkSize,
      cy * chunkSize
    ));

    if (cell == null)
      return;

    // ----- BASE -----
    if (cell.surfaceType.HasFlag(SurfaceType.Snow))
      chunk.SetBaseMaterial(matSnow);
    else if (cell.surfaceType.HasFlag(SurfaceType.Tundra))
      chunk.SetBaseMaterial(matTundra);
    else if (cell.surfaceType.HasFlag(SurfaceType.Grass))
      chunk.SetBaseMaterial(matGrass);
    else
      chunk.SetBaseMaterial(matPlains);

    // ----- OVERLAY -----
    if (cell.baseTerrain == BaseTerrain.Mountain)
      chunk.SetOverlayMaterial(matMountain);
    else if (cell.baseTerrain == BaseTerrain.Hill)
      chunk.SetOverlayMaterial(matHills);
    else if (cell.surfaceType.HasFlag(SurfaceType.Jungle))
      chunk.SetOverlayMaterial(matJungle);
    else if (cell.surfaceType.HasFlag(SurfaceType.Forest))
      chunk.SetOverlayMaterial(matForest);
    else if (cell.surfaceType.HasFlag(SurfaceType.Rough))
      chunk.SetOverlayMaterial(matRough);
    else
      chunk.SetOverlayMaterial(null);
  }*/

  void AssignChunkMaterials(HexChunk chunk, int cx, int cy)
  {
    // Alternate base material per chunk
    if ((cx + cy) % 2 == 0)
      chunk.SetBaseMaterial(matGrass);
    else
      chunk.SetBaseMaterial(matPlains);

    // Alternate overlay per chunk
    if (cx % 2 == 0)
      chunk.SetOverlayMaterial(matForest);
    else
      chunk.SetOverlayMaterial(null);
  }
}