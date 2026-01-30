using UnityEngine;

public static class HexTerrainTestInitializer
{
  public static void ApplyTestPattern(HexGrid grid)
  {
    int x = 0;
    int y = 0;

    // ----- ROW 0 : FLAT -----
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Plain);
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Grass);
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Grass | SurfaceType.Forest);
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Grass | SurfaceType.Rough);
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Grass | SurfaceType.Forest | SurfaceType.Rough);
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Desert);
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Snow);

    // ----- ROW 1 : HILL -----
    x = 0; y++;
    Set(grid, x++, y, BaseTerrain.Hill, SurfaceType.Plain);
    Set(grid, x++, y, BaseTerrain.Hill, SurfaceType.Grass);
    Set(grid, x++, y, BaseTerrain.Hill, SurfaceType.Forest);
    Set(grid, x++, y, BaseTerrain.Hill, SurfaceType.Forest | SurfaceType.Rough);
    Set(grid, x++, y, BaseTerrain.Hill, SurfaceType.Rough);
    Set(grid, x++, y, BaseTerrain.Hill, SurfaceType.Tundra);
    Set(grid, x++, y, BaseTerrain.Hill, SurfaceType.Tundra | SurfaceType.Forest);

    // ----- ROW 2 : MOUNTAIN -----
    x = 0; y++;
    Set(grid, x++, y, BaseTerrain.Mountain, SurfaceType.Grass);   // should be stripped
    Set(grid, x++, y, BaseTerrain.Mountain, SurfaceType.Forest);  // should be stripped
    Set(grid, x++, y, BaseTerrain.Mountain, SurfaceType.Rough);   // should be stripped
    Set(grid, x++, y, BaseTerrain.Mountain, SurfaceType.Snow);    // should remain snow-only

    // ----- ROW 3 : INVALID COMBINATIONS -----
    x = 0; y++;
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Grass | SurfaceType.Tundra); // grass removed
    Set(grid, x++, y, BaseTerrain.Flat, SurfaceType.Forest | SurfaceType.Jungle); // forest removed
  }

  private static void Set(
    HexGrid grid,
    int x,
    int y,
    BaseTerrain baseTerrain,
    SurfaceType surface)
  {
    HexCell cell = grid.GetCell(new HexCoord(x, y));
    if (cell == null)
      return;

    cell.baseTerrain = baseTerrain;
    cell.surfaceType = surface;

    HexTerrainValidator.Validate(cell);
  }
}