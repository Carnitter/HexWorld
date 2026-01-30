using UnityEngine;

public static class HexVisuals
{
  // ---------- BASE COLORS ----------

  public static Color GetBaseColor(HexCell cell)
  {
    if (cell.surfaceType.HasFlag(SurfaceType.Snow))
      return Color.white;

    if (cell.surfaceType.HasFlag(SurfaceType.Grass))
      return new Color(0.55f, 0.85f, 0.45f); // light green

    if (cell.surfaceType.HasFlag(SurfaceType.Tundra))
      return new Color(0.55f, 0.45f, 0.35f); // brown

    if (cell.surfaceType.HasFlag(SurfaceType.Desert))
      return new Color(0.85f, 0.8f, 0.55f); // sand

    // Plain default
    return new Color(0.75f, 0.7f, 0.4f); // hay yellow
  }

  public static Color GetBaseTerrainColor(HexCell cell, Color baseColor)
  {
    switch (cell.baseTerrain)
    {
      case BaseTerrain.Hill:
        return baseColor * 0.7f; // darker

      case BaseTerrain.Mountain:
        return new Color(0.35f, 0.35f, 0.35f); // dark grey

      default:
        return baseColor;
    }
  }

  // ---------- OVERLAY COLORS ----------

  public static bool HasForest(HexCell cell)
    => cell.surfaceType.HasFlag(SurfaceType.Forest);

  public static bool HasJungle(HexCell cell)
    => cell.surfaceType.HasFlag(SurfaceType.Jungle);

  public static bool HasRough(HexCell cell)
    => cell.surfaceType.HasFlag(SurfaceType.Rough);

  public static Color GetForestColor()
    => new Color(0.0f, 0.6f, 0.0f); // green

  public static Color GetJungleColor()
    => new Color(0.0f, 0.35f, 0.0f); // dark green

  public static Color GetRoughColor()
    => new Color(0.5f, 0.5f, 0.5f); // grey
}