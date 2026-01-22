using System;

[Serializable]
public class HexCoord : IEquatable<HexCoord>
{
  public int collumn; // axial collumn
  public int row;     // axial row


  public HexCoord(int collumn, int row)
  {
    this.collumn = collumn;
    this.row = row;
  }

  // Axial directions (pointy-top)
  public  static readonly HexCoord[] Directions =
   {
        new HexCoord( 1,  0),
        new HexCoord( 1, -1),
        new HexCoord( 0, -1),
        new HexCoord(-1,  0),
        new HexCoord(-1,  1),
        new HexCoord( 0,  1)
    };

  public HexCoord Neighbor(int directionNr)
  {
    var direction = Directions[directionNr];
    return new HexCoord(this.collumn + direction.collumn, this.row + direction.row);
  }

  public bool Equals(HexCoord other)
    => this.collumn == other.collumn && this.row == other.row;

  public override bool Equals(object obj)
    => obj is HexCoord other && Equals(other);

  public override int GetHashCode()
    => HashCode.Combine(this.collumn, this.row);

  public override string ToString()
    => $"({this.collumn}, {this.row})";

}
