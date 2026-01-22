using System.Collections.Generic;

public class HexGrid
{
  public int width;
  public int height;

  private Dictionary<HexCoord, HexCell> cells;

  public HexGrid(int width, int height)
  {
    this.width = width;
    this.height = height;

    this.cells = CreateCells();

  }

  private Dictionary<HexCoord, HexCell> CreateCells()
  {
    Dictionary<HexCoord, HexCell> newCells = new Dictionary<HexCoord, HexCell>();

    for (int collumn = 0; collumn < width; collumn++)
    {
      for (int row = 0; row < height; row++)
      {
        HexCoord coord = new HexCoord(collumn, row);
        newCells.Add(coord, new HexCell(coord));
      }
    }

    return newCells;
  }

  public HexCell GetCell(HexCoord coord) 
  { 
    this.cells.TryGetValue(coord, out HexCell cell);
    return cell;
  }

  public IEnumerable<HexCell> GetAllCells() 
  { 
    return this.cells.Values;
  }
}
