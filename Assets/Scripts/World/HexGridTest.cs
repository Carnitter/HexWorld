using UnityEngine;
using System.Collections.Generic;

public class HexGridTest : MonoBehaviour
{
  public int width = 32;
  public int height = 32;
  public int chunkSize = 16;

  private HexGrid grid;
  private List<HexChunk> chunks;

  void Start()
  {
    grid = new HexGrid(width, height);
    chunks = new List<HexChunk>();

    Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
    material.color = Color.green;

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

        HexChunk chunk = new HexChunk(cx, cy, material);

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
        chunks.Add(chunk);
      }
    }
  }
}