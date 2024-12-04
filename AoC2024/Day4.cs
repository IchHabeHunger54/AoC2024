namespace AoC2024;

public class Day4() : Day(4)
{
    protected override int TaskOne(List<string> lines)
    {
        int result = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] != 'X') continue;
                // horizontal, forwards
                if (line.Length - j >= 4 && lines[i][j + 1] == 'M' && lines[i][j + 2] == 'A' && lines[i][j + 3] == 'S')
                {
                    result++;
                }
                // horizontal, backwards
                if (j >= 3 && lines[i][j - 1] == 'M' && lines[i][j - 2] == 'A' && lines[i][j - 3] == 'S')
                {
                    result++;
                }
                // vertical, forwards
                if (lines.Count - i >= 4 && lines[i + 1][j] == 'M' && lines[i + 2][j] == 'A' && lines[i + 3][j] == 'S')
                {
                    result++;
                }
                // vertical, backwards
                if (i >= 3 && lines[i - 1][j] == 'M' && lines[i - 2][j] == 'A' && lines[i - 3][j] == 'S')
                {
                    result++;
                }
                // left diagonal, forwards
                if (line.Length - j >= 4 && lines.Count - i >= 4 && lines[i + 1][j + 1] == 'M' && lines[i + 2][j + 2] == 'A' && lines[i + 3][j + 3] == 'S')
                {
                    result++;
                }
                // left diagonal, backwards
                if (j >= 3 && i >= 3 && lines[i - 1][j - 1] == 'M' && lines[i - 2][j - 2] == 'A' && lines[i - 3][j - 3] == 'S')
                {
                    result++;
                }
                // right diagonal, forwards
                if (j >= 3 && lines.Count - i >= 4 && lines[i + 1][j - 1] == 'M' && lines[i + 2][j - 2] == 'A' && lines[i + 3][j - 3] == 'S')
                {
                    result++;
                }
                // right diagonal, backwards
                if (line.Length - j >= 4 && i >= 3 && lines[i - 1][j + 1] == 'M' && lines[i - 2][j + 2] == 'A' && lines[i - 3][j + 3] == 'S')
                {
                    result++;
                }
            }
        }
        return result;
    }

    protected override int TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}