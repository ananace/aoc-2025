namespace AdventOfCode2025;

using Point = (ulong X, ulong Y, ulong Z);

public class Day8 : IAoC
{
  public int Day => 8;

  List<Point> boxes = new List<Point>();

  public void Input(IEnumerable<string> lines)
  {
    foreach (var line in lines)
    {
      var parts = line.Split(',');
      boxes.Add((ulong.Parse(parts[0]), ulong.Parse(parts[1]), ulong.Parse(parts[2])));
    }
  }

  List<List<int>> circuits = new List<List<int>>();
  ulong firstThree = 0;
  ulong lastDist = 0;

  public void PreCalc()
  {
    var pairs = new List<(float Dist, int A, int B)>();
    for (int i = 0; i < boxes.Count; ++i)
    {
      for (int j = i + 1; j < boxes.Count; ++j)
      {
        float dist = Distance(boxes[i], boxes[j]);
        pairs.Add((dist, i, j));
      }
    }

    var count = boxes.Count == 20 ? 10 : 1000;

    pairs = pairs.OrderBy(pair => pair.Dist).ToList();
    for (int i = 0; i < pairs.Count; ++i)
    {
      var pair = pairs[i];
      var c_a = circuits.Find(c => c.Contains(pair.A));
      var c_b = circuits.Find(c => c.Contains(pair.B));

      if (c_a == null && c_b == null)
      {
        var circ = new List<int>();
        circ.Add(pair.A);
        circ.Add(pair.B);
        circuits.Add(circ);
      }
      else if (c_a != null && c_b == null)
      {
        c_a.Add(pair.B);
      }
      else if (c_a == null && c_b != null)
      {
        c_b.Add(pair.A);
      }
      else if (c_a != null && c_b != null)
      {
        if (c_a != c_b)
        {
          c_a.AddRange(c_b);
          circuits.Remove(c_b);
        }
      }

      if (i + 1 == count)
        firstThree = circuits.Select(c => (ulong)c.Count).Order().Reverse().Take(3).Aggregate(1ul, (t, v) => t *= v);

      if (c_a != c_b)
        lastDist = boxes[pair.A].X * boxes[pair.B].X;
    }
  }

  public void Part1()
  {
    Console.WriteLine($"First 10 is {firstThree}");
  }
  public void Part2()
  {
    Console.WriteLine($"Longest is {lastDist}");
  }

  float Distance(Point a, Point b)
  {
    return (float)((a.X - b.X) * (a.X - b.X)) + (float)((a.Y - b.Y) * (a.Y - b.Y)) + (float)((a.Z - b.Z) * (a.Z - b.Z));
  }
}
