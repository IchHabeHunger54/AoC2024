namespace AoC2024;

public class Day3() : Day(3)
{
    protected override int TaskOne(List<string> lines)
    {
        string line = lines.Aggregate("", (current, s) => current + s);
        int sum = 0;
        Stage stage = Stage.Opening;
        (string a, string b) currentPair = ("", "");
        for (int i = 0; i < line.Length; i++)
        {
            switch (stage)
            {
                case Stage.Opening:
                    if (line.Length - i > 3 && line[i] == 'm' && line[i + 1] == 'u' && line[i + 2] == 'l' && line[i + 3] == '(')
                    {
                        stage = Stage.Comma;
                        i += 3;
                    }
                    break;
                case Stage.Comma:
                    switch (line[i])
                    {
                        case >= '0' and <= '9':
                            currentPair.a += line[i];
                            break;
                        case ',':
                            stage = Stage.Closing;
                            break;
                        default:
                            currentPair = ("", "");
                            stage = Stage.Opening;
                            break;
                    }
                    break;
                case Stage.Closing:
                    switch (line[i])
                    {
                        case >= '0' and <= '9':
                            currentPair.b += line[i];
                            break;
                        case ')':
                            sum += int.Parse(currentPair.a) * int.Parse(currentPair.b);
                            currentPair = ("", "");
                            stage = Stage.Opening;
                            break;
                        default:
                            currentPair = ("", "");
                            stage = Stage.Opening;
                            break;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lines));
            }
        }
        return sum;
    }

    protected override int TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }

    private enum Stage
    {
        Opening,
        Comma,
        Closing
    }
}