namespace AdventOfCode2025;

public class Day3 : IAoC
{
  public int Day => 3;

  List<int[]> batteries = new List<int[]>();

  public void Input(IEnumerable<string> lines)
  {
    batteries.AddRange(lines.Select(line => line.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()));
  }

  public void Part1()
  {
    int total = 0;
    foreach (var bank in batteries)
    {
      int highest = 0;
      for (int i = 0; i < bank.Length - 1; ++i)
      {
        int jolt = bank[i] * 10;
        for (int j = i + 1; j < bank.Length; ++j)
        {
          if (jolt + bank[j] > highest)
            highest = jolt + bank[j];
        }
      }

      total += highest;
    }

    Console.WriteLine($"Total jolts: {total}");
  }
  public void Part2()
  {
    ulong total = 0;
    foreach (var bank in batteries)
    {
      ulong highest = findMaxJoltage(bank, 12);

      total += highest;
    }

    Console.WriteLine($"Total jolts: {total}");
  }

  ulong findMaxJoltage(int[] bank, int count)
  {
    ulong result = 0;
    int idx = 0;
    for (int toFill = count - 1; toFill >= 0; --toFill)
    {
      int best = bank[idx];
      for (int otherIdx = idx + 1; otherIdx < bank.Length - toFill; ++otherIdx)
      {
        int batt = bank[otherIdx];
        if (batt > best)
        {
          best = batt;
          idx = otherIdx;
        }
        if (best == 9)
          break;
      }
      result += (ulong)best * (ulong)Math.Pow(10, toFill);
      idx++;
    }
    return result;
  }
}
