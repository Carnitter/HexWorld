using UnityEngine;

public class HexMetrics 
{
  // Radius from center to corner
  public const float hexSize = 1f;

  // Precomputed constants (performance + clarity)
  public const float sqrt3 = 1.732050807f;


  // Corner offsets for a pointy-top hex (clockwise)
  public static readonly Vector3[] corners =
  {
    new Vector3(0f, 0f, hexSize),
    new Vector3(sqrt3 * 0.5f * hexSize, 0f, 0.5f * hexSize),
    new Vector3(sqrt3 * 0.5f * hexSize, 0f, -0.5f * hexSize),
    new Vector3( 0f, 0f, -hexSize),
    new Vector3(-sqrt3 * 0.5f * hexSize, 0f, -0.5f * hexSize),
    new Vector3(-sqrt3 * 0.5f * hexSize, 0f,  0.5f * hexSize)
  };
  public static Vector3 HexToWorld(HexCoord coord)
  {
    float x = hexSize * (sqrt3 * coord.collumn + sqrt3 * 0.5f * coord.row);
    float z = hexSize * (1.5f * coord.row);

    return new Vector3(x, 0f, z);
  }

}
