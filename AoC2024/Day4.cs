namespace AoC2024;

public class Day4() : Day(4)
{
    protected override long TaskOne(List<string> lines)
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

    protected override long TaskTwo(List<string> lines)
    {
        int result = 0;
        for (int i = 1; i < lines.Count - 1; i++)
        {
            string line = lines[i];
            for (int j = 1; j < line.Length - 1; j++)
            {
                if (line[j] != 'A') continue;
                // M.M
                // .A.
                // S.S
                if (lines[i - 1][j - 1] == 'M' && lines[i - 1][j + 1] == 'M' && lines[i + 1][j - 1] == 'S' && lines[i + 1][j + 1] == 'S')
                {
                    result++;
                }
                // S.M
                // .A.
                // S.M
                if (lines[i - 1][j - 1] == 'S' && lines[i - 1][j + 1] == 'M' && lines[i + 1][j - 1] == 'S' && lines[i + 1][j + 1] == 'M')
                {
                    result++;
                }
                // S.S
                // .A.
                // M.M
                if (lines[i - 1][j - 1] == 'S' && lines[i - 1][j + 1] == 'S' && lines[i + 1][j - 1] == 'M' && lines[i + 1][j + 1] == 'M')
                {
                    result++;
                }
                // M.S
                // .A.
                // M.S
                if (lines[i - 1][j - 1] == 'M' && lines[i - 1][j + 1] == 'S' && lines[i + 1][j - 1] == 'M' && lines[i + 1][j + 1] == 'S')
                {
                    result++;
                }
            }
        }
        return result;
    }
}