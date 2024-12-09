namespace AoC2024;

public class Day9() : Day(9)
{
    protected override long TaskOne(List<string> lines)
    {
        List<int> line = lines[0].Select(c => c - '0').ToList();
        List<int?> list = [];
        List<long> result = [];
        for (int i = 0; i < line.Count; i++)
        {
            int? value = i % 2 == 0 ? i / 2 : null;
            for (int j = 0; j < line[i]; j++)
            {
                list.Add(value);
            }
        }
        int start = 0;
        int end = list.Count - 1;
        while (start <= end)
        {
            int? value = list[start];
            if (value != null)
            {
                result.Add(value.Value);
            }
            else
            {
                int? toMove = list[end];
                end--;
                while (toMove == null)
                {
                    toMove = list[end];
                    end--;
                }
                result.Add(toMove.Value);
            }
            start++;
        }
        return result.Select((x, i) => x * i).Sum();
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}