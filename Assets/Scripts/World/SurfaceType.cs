using System;

[Flags]
public enum SurfaceType
{
  Plain   = 0,
  Grass   = 1 << 0,
  Forest  = 1 << 1,
  Jungle  = 1 << 2,
  Tundra  = 1 << 3,
  Desert  = 1 << 4,
  Rough   = 1 << 5,
  Snow    = 1 << 6,
}