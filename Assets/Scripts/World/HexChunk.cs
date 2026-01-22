using System.Collections.Generic;
using UnityEngine;

public class HexChunk 
{
  public readonly int chunkX;
  public readonly int chunkY;

  private List<HexCell> cells;

  private GameObject gameObject;
  private Mesh mesh;
  private MeshFilter meshFilter;
  private MeshRenderer meshRenderer;

  public HexChunk(int chunkX, int chunkY, Material material)
  {
    this.chunkX = chunkX;
    this.chunkY = chunkY;

    cells = new List<HexCell>();

    gameObject = new GameObject($"HexChunk ({chunkX},{chunkY})");

    mesh = new Mesh();
    mesh.name = "HexChunkMesh";

    meshFilter = gameObject.AddComponent<MeshFilter>();
    meshRenderer = gameObject.AddComponent<MeshRenderer>();

    meshFilter.mesh = mesh;
    meshRenderer.material = material;
  }

  public void AddCell(HexCell cell)
  {
    cells.Add( cell );
  }

  public void BuildMesh()
  {
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    int vertexIndex = 0;
    
    foreach (HexCell cell in cells)
    {
      Vector3 center = HexMetrics.HexToWorld(cell.coord);

      // Center vertex
      vertices.Add( center );

      // corner vertices
      for (int i = 0; i < 6; i++)
      {
        vertices.Add(center + HexMetrics.corners[i]);
      }

      // Triangles (fan)
      for (int i = 0; i < 6; i++)
      {
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + i + 1);
        triangles.Add(vertexIndex + (i == 5 ? 1: i + 2));
      }

      vertexIndex+= 7;
    }

    mesh.Clear();
    mesh.vertices = vertices.ToArray();
    mesh.triangles = triangles.ToArray();
    mesh.RecalculateNormals();
    mesh.RecalculateBounds();
    
  }

}
