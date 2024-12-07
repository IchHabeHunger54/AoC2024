namespace AoC2024;

public class Day3() : Day(3)
{
    protected override long TaskOne(List<string> lines)
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
                    if (line.Length - i >= 4 && line.Substring(i, 4).Equals("mul("))
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

    protected override long TaskTwo(List<string> lines)
    {
        string line = lines.Aggregate("", (current, s) => current + s);
        int sum = 0;
        Stage stage = Stage.Opening;
        (string a, string b) currentPair = ("", "");
        bool enabled = true;
        for (int i = 0; i < line.Length; i++)
        {
            switch (line.Length - i)
            {
                case >= 4 when line.Substring(i, 4).Equals("do()"):
                    enabled = true;
                    break;
                case >= 7 when line.Substring(i, 7).Equals("don't()"):
                    enabled = false;
                    currentPair = ("", "");
                    stage = Stage.Opening;
                    break;
                default:
                {
                    if (enabled)
                    {
                        switch (stage)
                        {
                            case Stage.Opening:
                                if (line.Length - i >= 4 && line.Substring(i, 4).Equals("mul("))
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
                    break;
                }
            }
        }
        return sum;
    }

    private enum Stage
    {
        Opening,
        Comma,
        Closing
    }
}