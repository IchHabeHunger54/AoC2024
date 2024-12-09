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
            start++;
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
        }
        return result.Select((x, i) => x * i).Sum();
    }

    protected override long TaskTwo(List<string> lines)
    {
        List<int> line = lines[0].Select(c => c - '0').ToList();
        List<Block> list = [];
        for (int i = 0; i < line.Count; i++)
        {
            int? value = i % 2 == 0 ? i / 2 : null;
            if (line[i] > 0)
            {
                list.Add(new Block(line[i], value));
            }
        }
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Block block = list[i];
            if (block.Value == null) continue;
            for (int j = 0; j < i; j++)
            {
                Block newBlock = list[j];
                if (newBlock.Value != null || newBlock.Size < block.Size) continue;
                list.Insert(j, block);
                int newSize = newBlock.Size - block.Size;
                if (newSize > 0)
                {
                    list[j + 1] = new Block(newSize, null);
                    i++;
                }
                else
                {
                    list.RemoveAt(j + 1);
                }
                list[i] = block with { Value = null };
                break;
            }
        }
        int index = 0;
        long result = 0;
        foreach (Block block in list)
        {
            for (int i = 0; i < block.Size; i++)
            {
                result += (block.Value ?? 0) * index;
                index++;
            }
        }
        return result;
    }

    private record Block(int Size, int? Value);
}