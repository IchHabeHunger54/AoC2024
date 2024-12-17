namespace AoC2024;

public class Day17() : Day(17)
{
    protected override long TaskOne(List<string> lines)
    {
        long a = long.Parse(lines[0].Replace("Register A: ", ""));
        long b = long.Parse(lines[1].Replace("Register B: ", ""));
        long c = long.Parse(lines[2].Replace("Register C: ", ""));
        int[] program = lines[4].Replace("Program: ", "").Split(',').Select(int.Parse).ToArray();
        int pointer = 0;
        List<long> output = [];
        while (pointer < program.Length)
        {
            int opcode = program[pointer];
            int operand = program[pointer + 1];
            long combo = operand switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => a,
                5 => b,
                6 => c,
                _ => throw new ArgumentOutOfRangeException(nameof(lines))
            };
            bool jump = false;
            switch (opcode)
            {
                case 0:
                    a = (long)(a / Math.Pow(2, combo));
                    break;
                case 1:
                    b ^= operand;
                    break;
                case 2:
                    b = combo % 8;
                    break;
                case 3:
                    if (a == 0) break;
                    pointer = operand;
                    jump = true;
                    break;
                case 4:
                    b ^= c;
                    break;
                case 5:
                    output.Add(combo % 8);
                    break;
                case 6:
                    b = (long)(a / Math.Pow(2, combo));
                    break;
                case 7:
                    c = (long)(a / Math.Pow(2, combo));
                    break;
            }
            if (!jump)
            {
                pointer += 2;
            }
        }
        Console.WriteLine("String output: " + string.Join(',', output));
        return 0;
    }

    protected override long TaskTwo(List<string> lines)
    {
        throw new NotImplementedException();
    }
}