public static class HexTerrainValidator
{
  public static void Validate(HexCell cell)
  {
    // Mountains are always bare
    if (cell.baseTerrain == BaseTerrain.Mountain)
    {
      cell.surfaceType = SurfaceType.Plain;
      return;
    }

    // Forest and Jungle are mutually exclusive
    if (cell.surfaceType.HasFlag(SurfaceType.Forest) &&
        cell.surfaceType.HasFlag(SurfaceType.Jungle))
    {
      // Prefer Jungle if both were set
      cell.surfaceType &= ~SurfaceType.Forest;
    }

    // Tundra cannot have grass
    if (cell.surfaceType.HasFlag(SurfaceType.Tundra))
    {
      cell.surfaceType &= ~SurfaceType.Grass;
    }

    // Snow overrides everything (impassable & visual)
    if (cell.surfaceType.HasFlag(SurfaceType.Snow))
    {
      cell.surfaceType = SurfaceType.Snow;
    }
  }
}