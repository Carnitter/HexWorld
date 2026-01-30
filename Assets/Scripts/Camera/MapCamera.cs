using UnityEngine;

public class MapCamera : MonoBehaviour
{
  public int mapWidth = 32;
  public int mapHeight = 32;

  public float height = 25f;
  public float tiltAngle = 60f;

  void Start()
  {
    PositionCamera();
  }

  void PositionCamera()
  {
    // Center of the map in hex coordinates
    HexCoord centerCoord = new HexCoord(
      mapWidth / 2,
      mapHeight / 2
    );

    Vector3 centerWorldPos = HexMetrics.HexToWorld(centerCoord);

    // Position camera above and slightly offset
    transform.position = centerWorldPos + new Vector3(0f, height, -height * 0.6f);

    // Look at the center of the map
    transform.rotation = Quaternion.Euler(tiltAngle, 0f, 0f);
  }
}
