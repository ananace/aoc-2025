namespace AdventOfCode2025;

public class Day1 : IAoC
{
  public int Day => 1;

  List<int> changes = new List<int>();

  public void Input(IEnumerable<string> lines)
  {
    foreach (var line in lines)
    {
      int mult = line[0] == 'R' ? 1 : -1;
      changes.Add(int.Parse(line[1..]) * mult);
    }

    Console.WriteLine($"Read {changes.Count} instructions");
  }

  public void Part1()
  {
    int zero = 0;

    int at = 50;
    foreach (var change in changes)
    {
      at += change;
      if (at % 100 == 0)
        zero++;
    }

    Console.WriteLine($"Hit 0 a total of {zero} times");
  }
  public void Part2()
  {
    int zero = 0;

    int at = 50 + 1000;
    foreach (var change in changes)
    {
      if (at % 100 == 0)
        zero += Math.Abs(change / 100);
      else
      {
        zero += Math.Abs((at + change) / 100 - at / 100);
        if (change < 0 && (at + change) % 100 == 0)
          zero++;
      }
      at += change;
    }

    Console.WriteLine($"Hit 0 a total of {zero} times");
  }
}
