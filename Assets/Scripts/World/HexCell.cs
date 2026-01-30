using System;

[Serializable]
public class HexCell
{
  public HexCoord coord;

  public BaseTerrain baseTerrain;
  public SurfaceType surfaceType;

  public const int IMPASSABLE = int.MaxValue;

  public HexCell(HexCoord coord)
  {
    this.coord = coord;

    // sensible defaults 
    this.baseTerrain = BaseTerrain.Flat;
    this.surfaceType = SurfaceType.Plain;
  }

  public int GetMovementCost()
  {
    // Base terrain cost (10 = flat baseline)
    int cost = 10;

    switch (this.baseTerrain)
    {
      case BaseTerrain.Flat:
        cost += 0;
        break;
      case BaseTerrain.Hill:
        cost += 10; 
        break;

      case BaseTerrain.Mountain:
      case BaseTerrain.Sea:
        return IMPASSABLE;

    }

    if(this.surfaceType.HasFlag(SurfaceType.Snow))
    {
      // boundary border north south might change later
      return IMPASSABLE;
    }

    if (this.surfaceType.HasFlag(SurfaceType.Jungle))
    {
      cost += 10;
    }
    if (this.surfaceType.HasFlag(SurfaceType.Forest))
    {
      cost += 5;
    }
    if (this.surfaceType.HasFlag(SurfaceType.Rough))
    {
      cost += 5;
    }
    
    return cost;
  }

}
