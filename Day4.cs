namespace AdventOfCode2025;

public class Day4 : IAoC
{
  public int Day => 4;

  bool[] rolls = new bool[0];
  (int, int) size = (0, 0);

  public void Input(IEnumerable<string> lines)
  {
    List<bool> reading = new List<bool>();
    foreach (var line in lines)
    {
      size.Item1 = line.Length;
      size.Item2++;

      reading.AddRange(line.ToCharArray().Select(c => c == '@'));
    }

    rolls = reading.ToArray();
  }

  public void Part1()
  {
    int movable = 0;

    for (int x = 0; x < size.Item1; ++x)
    {
      for (int y = 0; y < size.Item2; ++y)
      {
        if (!rolls[x + y * size.Item1])
          continue;

        var adj = CountAdjacent(x, y);
        if (adj >= 4)
          continue;

        movable++;
      }
    }

    Console.WriteLine($"Found {movable} movable rolls.");
  }
  public void Part2()
  {
    int moved = 0, movedRound;

    var rollsCopy = rolls;
    var rollsMut = rolls.ToArray();

    do
    {
      movedRound = 0;
      for (int x = 0; x < size.Item1; ++x)
      {
        for (int y = 0; y < size.Item2; ++y)
        {
          if (!rollsCopy[x + y * size.Item1])
            continue;

          var adj = CountAdjacent(x, y, rollsCopy);
          if (adj >= 4)
            continue;

          movedRound++;
          rollsMut[x + y * size.Item1] = false;
        }
      }

      moved += movedRound;
      rollsCopy = rollsMut.ToArray();
    } while (movedRound > 0);

    Console.WriteLine($"Moved {moved} movable rolls.");
  }

  int CountAdjacent(int x, int y, bool[]? array = null)
  {
    if (array == null)
      array = rolls;

    int found = 0;
    for (int xdiff = -1; xdiff <= 1; ++xdiff)
    {
      for (int ydiff = -1; ydiff <= 1; ++ydiff)
      {
        if (xdiff == 0 && ydiff == 0)
          continue;

        if (x + xdiff < 0 || x + xdiff >= size.Item1)
          continue;
        if (y + ydiff < 0 || y + ydiff >= size.Item2)
          continue;

        if (array[x + xdiff + (y + ydiff) * size.Item1])
          found++;
      }
    }
    return found;
  }
}
