namespace AdventOfCode2025;

static class LINQExtensions
{
  public static ulong Sum(this IEnumerable<ulong> range)
  {
    ulong ret = 0;
    foreach (var value in range)
      ret += value;
    return ret;
  }
}

public class Day5 : IAoC
{
  public int Day => 5;

  List<(ulong Min, ulong Max)> freshRanges = new List<(ulong, ulong)>();
  List<ulong> ingredients = new List<ulong>();

  public void Input(IEnumerable<string> lines)
  {
    bool ranges = true;
    foreach (var line in lines)
    {
      if (string.IsNullOrWhiteSpace(line))
      {
        ranges = false;
      }
      else if (ranges)
      {
        var parts = line.Split('-').Select(part => ulong.Parse(part));
        freshRanges.Add((parts.First(), parts.Last()));
      }
      else
      {
        ingredients.Add(ulong.Parse(line));
      }
    }
  }

  public void Part1()
  {
    int fresh = 0;
    foreach (var ingred in ingredients)
    {
      if (freshRanges.Any(range => Contains(range, ingred)))
        fresh++;
    }
    Console.WriteLine($"{fresh} ingredients are fresh");
  }
  public void Part2()
  {
    var sorted = freshRanges.OrderBy(r => r.Item1);
    var withoutOverlap = new Stack<(ulong Min, ulong Max)>();
    withoutOverlap.Push(sorted.First());

    foreach (var range in sorted)
    {
      var last = withoutOverlap.Peek();
      if (range.Min > last.Max)
      {
        withoutOverlap.Push(range);
      }
      else if (range.Min <= last.Max && range.Max > last.Max)
      {
        withoutOverlap.Pop();
        withoutOverlap.Push((last.Min, range.Max));
      }
    }

    ulong total = withoutOverlap.Select(r => r.Max - r.Min + 1).Sum();
    Console.WriteLine($"{total} ingredients are fresh");
  }

  bool Contains((ulong, ulong) range, ulong value)
  {
    return range.Item1 <= value &&  value <= range.Item2;
  }
}
