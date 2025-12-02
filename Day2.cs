namespace AdventOfCode2025;

public class Day2 : IAoC
{
  public int Day => 2;

  List<(ulong, ulong)> ranges = new List<(ulong, ulong)>();

  public void Input(IEnumerable<string> lines)
  {
    foreach (var line in lines)
    {
      var toAdd = line.Split(',');
      foreach (var range in toAdd.Where(s => !string.IsNullOrWhiteSpace(s)))
      {
        var parts = range.Split('-');
        var from = parts[0];
        var to = parts[1];

        ranges.Add((ulong.Parse(from), ulong.Parse(to)));
      }
    }
  }

  public void Part1()
  {
    ulong mistakes = 0;

    foreach (var range in ranges)
    {
      for (ulong i = range.Item1; i <= range.Item2; ++i)
      {
        var numStr = i.ToString().AsSpan();
        if (numStr.Length % 2 != 0)
          continue;

        var halfLen = numStr.Length / 2;
        var halfSlice = numStr.Slice(0, halfLen);
        if (MemoryExtensions.Equals(halfSlice, numStr.Slice(halfLen), StringComparison.Ordinal))
          mistakes += i;
      }
    }

    Console.WriteLine($"Total mistakes in ID ranges: {mistakes}");
  }
  public void Part2()
  {
    ulong mistakes = 0;

    foreach (var range in ranges)
    {
      for (ulong i = range.Item1; i <= range.Item2; ++i)
      {
        if (i < 10)
          continue;

        var numStr = i.ToString().AsSpan();
        for (int j = numStr.Length / 2; j > 0; --j)
        {
          if (numStr.Length % j != 0)
            continue;

          var slice = numStr.Slice(0, j);
          bool valid = true;
          for (int k = 0; k < numStr.Length; k += j)
            if (!MemoryExtensions.Equals(slice, numStr.Slice(k, j), StringComparison.Ordinal))
            {
              valid = false;
              break;
            }

          if (valid)
          {
            mistakes += i;
            break;
          }
        }
      }
    }

    Console.WriteLine($"Total mistakes in ID ranges: {mistakes}");
  }
}
