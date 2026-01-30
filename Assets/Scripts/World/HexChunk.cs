using System.Collections.Generic;
using UnityEngine;

public class HexChunk
{
  public readonly int chunkX;
  public readonly int chunkY;
  private readonly int chunkSize;

  private List<HexCell> cells;

  private GameObject gameObject;

  private Mesh mesh;

  private MeshFilter baseMeshFilter;
  private MeshRenderer baseMeshRenderer;

  private MeshFilter overlayMeshFilter;
  private MeshRenderer overlayMeshRenderer;

  public HexChunk(
    int chunkX,
    int chunkY,
    int chunkSize,
    Material baseMaterial,
    Material overlayMaterial)
  {
    this.chunkX = chunkX;
    this.chunkY = chunkY;
    this.chunkSize = chunkSize;

    cells = new List<HexCell>();

    gameObject = new GameObject($"HexChunk ({chunkX},{chunkY})");

    // Shared mesh
    mesh = new Mesh();
    mesh.name = "HexChunkMesh";

    // ---------- BASE ----------
    GameObject baseGO = new GameObject("BaseMesh");
    baseGO.transform.SetParent(gameObject.transform, false);

    baseMeshFilter = baseGO.AddComponent<MeshFilter>();
    baseMeshRenderer = baseGO.AddComponent<MeshRenderer>();

    baseMeshFilter.mesh = mesh;
    baseMeshRenderer.material = baseMaterial;

    // ---------- OVERLAY ----------
    GameObject overlayGO = new GameObject("OverlayMesh");
    overlayGO.transform.SetParent(gameObject.transform, false);

    overlayMeshFilter = overlayGO.AddComponent<MeshFilter>();
    overlayMeshRenderer = overlayGO.AddComponent<MeshRenderer>();

    overlayMeshFilter.mesh = mesh;
    overlayMeshRenderer.material = overlayMaterial;

    // Ensure overlay renders on top
    overlayMeshRenderer.sortingOrder = 1;
  }

  public void AddCell(HexCell cell)
  {
    cells.Add(cell);
  }

  public void BuildMesh()
  {
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    int vertexIndex = 0;

    foreach (HexCell cell in cells)
    {
      HexCoord localCoord = new HexCoord(
        cell.coord.collumn - chunkX * chunkSize,
        cell.coord.row - chunkY * chunkSize
      );

      Vector3 center = HexMetrics.HexToWorld(localCoord);

      for (int i = 0; i < 6; i++)
      {
        Vector3 vCenter = center;
        Vector3 vA = center + HexMetrics.corners[i];
        Vector3 vB = center + HexMetrics.corners[(i + 1) % 6];

        vertices.Add(vCenter);
        vertices.Add(vA);
        vertices.Add(vB);

        // ----- UVs -----
        Vector2 uvCenter = new Vector2(0.5f, 0.5f);

        float angleA = i * Mathf.PI * 2f / 6f + Mathf.PI / 2f;
        float angleB = (i + 1) * Mathf.PI * 2f / 6f + Mathf.PI / 2f;

        Vector2 uvA = new Vector2(
          0.5f + Mathf.Cos(angleA) * 0.5f,
          0.5f + Mathf.Sin(angleA) * 0.5f
          );

        Vector2 uvB = new Vector2(
          0.5f + Mathf.Cos(angleB) * 0.5f,
          0.5f + Mathf.Sin(angleB) * 0.5f
          );

        uvs.Add(uvCenter);
        uvs.Add(uvA);
        uvs.Add(uvB);


        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);

        vertexIndex += 3;
      }
    }

    mesh.Clear();
    mesh.vertices = vertices.ToArray();
    mesh.triangles = triangles.ToArray();
    mesh.uv = uvs.ToArray();

    mesh.RecalculateNormals();
    mesh.RecalculateBounds();
  }

  // ---------- MATERIAL ASSIGNMENT ----------

  public void SetBaseMaterial(Material material)
  {
    baseMeshRenderer.material = material;
  }

  public void SetOverlayMaterial(Material material)
  {
    if (material == null)
    {
      overlayMeshRenderer.enabled = false;
    }
    else
    {
      overlayMeshRenderer.enabled = true;
      overlayMeshRenderer.material = material;
    }
  }
}