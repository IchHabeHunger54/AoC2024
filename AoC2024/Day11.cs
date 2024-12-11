namespace AoC2024;

public class Day11() : Day(11)
{
    protected override long TaskOne(List<string> lines)
    {
        List<long> numbers = lines[0].Split(' ').Select(long.Parse).ToList();
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < numbers.Count; j++)
            {
                long number = numbers[j];
                if (number == 0)
                {
                    numbers[j] = 1;
                }
                else if (number.ToString().Length % 2 == 0)
                {
                    string s = number.ToString();
                    numbers[j] = long.Parse(s[..(s.Length / 2)]);
                    numbers.Insert(j + 1, long.Parse(s[(s.Length / 2)..]));
                    j++;
                }
                else
                {
                    numbers[j] *= 2024;
                }
            }
        }
        return numbers.Count;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}