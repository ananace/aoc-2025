namespace AdventOfCode2025;

using Point = (long X, long Y);

public class Day9 : IAoC
{
  public int Day => 9;

  List<Point> Tiles = new List<Point>();

  public void Input(IEnumerable<string> lines)
  {
    foreach (var line in lines)
    {
      var parts = line.Split(',');
      Tiles.Add((long.Parse(parts[0]), long.Parse(parts[1])));
    }
  }

  long maxArea = 0;
  long maxAreaInside = 0;

  public void PreCalc()
  {
    for (int i = 0; i < Tiles.Count; ++i)
    {
      for (int j = i + 1; j < Tiles.Count; ++j)
      {
        var area = CalcArea(Tiles[i], Tiles[j]);
        if (area > maxArea)
          maxArea = area;

        if (area <= maxAreaInside)
          continue;

        if (IsInside(Tiles[i], Tiles[j]))
          maxAreaInside = area;
      }
    }
  }

  public void Part1()
  {
    Console.WriteLine($"Max area is {maxArea}");
  }
  public void Part2()
  {
    Console.WriteLine($"Max area is {maxAreaInside}");
  }

  long CalcArea(Point a, Point b) => (Math.Abs(a.X - b.X) + 1) * (Math.Abs(a.Y - b.Y) + 1);

  bool IsInside(Point a, Point b) 
  {
    long xmin = Math.Min(a.X, b.X), 
         ymin = Math.Min(a.Y, b.Y),
         xmax = Math.Max(a.X, b.X),
         ymax = Math.Max(a.Y, b.Y);

    var midx = (xmin + xmax) / 2;
    var midy = (ymin + ymax) / 2;

    bool inside = false;
    foreach (var (pointA, pointB) in Tiles.Zip(Tiles.Skip(1)))
    {
      if (pointA.X == midx && pointA.Y == midy)
      {
        inside = true;
        break;
      }

      if ((pointA.Y > midy) != (pointB.Y > midy))
      {
        var slope = (midx - pointA.X) * (pointB.Y - pointA.Y) - (pointB.X - pointA.X) * (midy - pointA.Y);
        if (slope == 0)
        {
          inside = true;
          break;
        }
        else if ((slope < 0) != (pointB.Y < pointA.Y))
          inside = !inside;
      }
    }

    if (!inside)
      return false;
    
    foreach (var (pointA, pointB) in Tiles.Zip(Tiles.Skip(1)))
    {
      if (pointA.X == pointB.X)
      {
        if (pointA.X <= xmin || pointA.X >= xmax)
          continue;

        var wallMinY = Math.Min(pointA.Y, pointB.Y);
        var wallMaxY = Math.Max(pointA.Y, pointB.Y);

        if (Math.Max(ymin, wallMinY) < Math.Min(ymax, wallMaxY))
          return false;
      }
      else if (pointA.Y == pointB.Y)
      {
        if (pointA.Y <= ymin || pointA.Y >= ymax)
          continue;

        var wallMinX = Math.Min(pointA.X, pointB.X);
        var wallMaxX = Math.Max(pointA.X, pointB.X);

        if (Math.Max(xmin, wallMinX) < Math.Min(xmax, wallMaxX))
          return false;
      }
    }

    return true;
  }
}
