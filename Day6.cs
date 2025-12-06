namespace AdventOfCode2025;

public class Day6 : IAoC
{
  public int Day => 6;

  List<List<ulong>> operands = new List<List<ulong>>();
  List<List<ulong>> cephOperands = new List<List<ulong>>();
  List<char> operators = new List<char>();

  public void Input(IEnumerable<string> lines)
  {
    List<string> data = new List<string>();
    foreach (var line in lines)
    {
      bool num = false;
      var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      for (int i = 0; i < parts.Length; ++i)
      {
        if (operands.Count <= i)
          operands.Add(new List<ulong>());

        if (ulong.TryParse(parts[i], out ulong operand))
        {
          num = true;
          operands[i].Add(operand);
        }
        else
          operators.Add(parts[i].Trim()[0]);
      }

      if (num)
        data.Add(line);
    }

    int operIter = 0;
    for (int i = data.First().Length - 1; i >= 0; --i)
    {
      ulong? num = null;
      foreach (var line in data)
      {
        if (ulong.TryParse(line[i].ToString(), out ulong parsed))
        {
          if (num == null)
            num = parsed;
          else
            num = num * 10 + parsed;
        }
      }

      if (num != null)
      {
        if (cephOperands.Count <= operIter)
          cephOperands.Add(new List<ulong>());
        cephOperands[operIter].Add(num.Value);
      }
      else
        operIter++;
    }
  }

  public void Part1()
  {
    ulong result = 0;
    for (int i = 0; i < operators.Count; ++i)
    {
      ulong value = 0;
      if (operators[i] == '*')
        value = operands[i].Aggregate(1ul, (total, value) => total * value);
      else
        value = operands[i].Sum();

      result += value;
    }

    Console.WriteLine($"Total result is {result}");
  }
  public void Part2()
  {
    ulong result = 0;
    for (int i = 0; i < operators.Count; ++i)
    {
      ulong value = 0;
      if (operators[operators.Count - i - 1] == '*')
        value = cephOperands[i].Aggregate(1ul, (total, value) => total * value);
      else
        value = cephOperands[i].Sum();

      result += value;
    }

    Console.WriteLine($"Total result is {result}");
  }
}
