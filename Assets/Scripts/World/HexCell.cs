using System;

[Serializable]
public class HexCell
{
  public HexCoord coord;

  // Basic terrain data (@todo expand later )
  public int elevation;
  public bool isWater;

  // Movement (@todo placeholder)
  public int movementCost = 1;

  public HexCell(HexCoord coord)
  {
    this.coord = coord;
  }

}
