using UnityEngine;

public static class HexMeshBuilder 
{
    public static Mesh CreateHexMesh()
  {
    Mesh mesh = new Mesh();
    mesh.name = "HexMesh";

    Vector3[] vertices = new Vector3[7];
    int[] triangles = new int[18];

    // Center vertex
    vertices[0] = Vector3.zero;

    // Corner vertex 
    for (int i = 0; i < 6; i++)
    {
      vertices[i+1] = HexMetrics.corners[i];
    }

    // Triangles (fan from center)
    for (int i = 0;i < 6; i++)
    {
      int triIndex = i * 3;
      triangles[triIndex] = 0;
      triangles[triIndex + 1] = i + 1;
      triangles[triIndex + 2] = (i==5) ? 1 : i + 2;
    }

    mesh.vertices = vertices;
    mesh.triangles = triangles;

    mesh.RecalculateNormals();
    mesh.RecalculateBounds();

    return mesh;

  }
}
