namespace AdventOfCode2025;

public class Day7 : IAoC
{
  public int Day => 7;

  Dictionary<ulong, bool> Splitters = new Dictionary<ulong, bool>();
  (ulong X, ulong Y) Size = (0ul, 0ul);
  (ulong X, ulong Y) Start = (0ul, 0ul);

  public void Input(IEnumerable<string> lines)
  {
    foreach (var line in lines)
    {
      Size.X = (ulong)line.Length;
      Size.Y++;

      for (int x = 0; x < line.Length; ++x)
      {
        if (line[x] == 'S')
          Start = ((ulong)x, Size.Y);
        else if (line[x] == '^')
          Splitters[Size.Y * Size.X + (ulong)x] = true;
      }
    }
  }

  public void Part1()
  {
    List<ulong> beams = new List<ulong>();
    beams.Add(Start.Y * Size.X + Start.X);

    int splits = 0;
    while (beams.Any())
    {
      List<ulong> newBeams = new List<ulong>();
      foreach(var beam in beams)
      {
        var newPos = beam + Size.X;
        if (Splitters.ContainsKey(newPos))
        {
          splits++;
          if (!newBeams.Contains(newPos - 1))
            newBeams.Add(newPos - 1);
          if (!newBeams.Contains(newPos + 1))
            newBeams.Add(newPos + 1);
        }
        else if (newPos <= Size.Y * Size.X && !newBeams.Contains(newPos))
          newBeams.Add(newPos);
      }
      beams = newBeams;
    }

    Console.WriteLine($"Beam split {splits} times");
  }
  public void Part2()
  {
    ulong[] beams = new ulong[Size.X];
    beams[Start.X] = 1;

    for (ulong y = 0; y < Size.Y; ++y)
    {
      ulong[] newBeams = new ulong[Size.X];
      for (ulong x = 0; x < Size.X; ++x)
      {
        if (beams[x] == 0)
          continue;

        if (Splitters.ContainsKey(y * Size.X + x))
        {
          newBeams[x - 1] += beams[x];
          newBeams[x + 1] += beams[x];
        }
        else
          newBeams[x] += beams[x];
      }
      beams = newBeams;
    }

    Console.WriteLine($"Tachyon has {beams.Sum()} timelines");
  }
}
