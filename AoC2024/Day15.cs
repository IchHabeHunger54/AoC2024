using System.Text;
using AoC2024.util;

namespace AoC2024;

public class Day15() : Day(15)
{
    protected override long TaskOne(List<string> lines)
    {
        List<List<char>> warehouse = [];
        int i = 0;
        int x = -1, y = -1;
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (line.Length == 0) break;
            if (line.Contains('@'))
            {
                x = line.IndexOf('@');
                y = i;
                line = line.Replace('@', '.');
            }
            warehouse.Add(line.ToCharArray().ToList());
        }
        i++;
        List<Direction> directions = [];
        for (; i < lines.Count; i++)
        {
            directions.AddRange(lines[i].Select(c => c switch
            {
                '^' => Direction.Up,
                'v' => Direction.Down,
                '<' => Direction.Left,
                '>' => Direction.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(lines))
            }));
        }
        foreach (Direction direction in directions)
        {
            int newX = direction switch
            {
                Direction.Left => x - 1,
                Direction.Right => x + 1,
                _ => x
            };
            int newY = direction switch
            {
                Direction.Up => y - 1,
                Direction.Down => y + 1,
                _ => y
            };
            char c = warehouse[newY][newX];
            if (c == '#') continue;
            if (c == '.')
            {
                x = newX;
                y = newY;
            }
            if (c == 'O' && CanBoxMove(warehouse, newX, newY, direction))
            {
                MoveSingleBox(warehouse, newX, newY, direction);
                warehouse[newY][newX] = '.';
                x = newX;
                y = newY;
            }
        }
        long result = 0;
        for (y = 0; y < warehouse.Count; y++)
        {
            List<char> list = warehouse[y];
            for (x = 0; x < list.Count; x++)
            {
                if (list[x] != 'O') continue;
                result += 100 * y + x;
            }
        }
        return result;
    }

    protected override long TaskTwo(List<string> lines)
    {
        List<List<char>> warehouse = [];
        int i = 0;
        int x = -1, y = -1;
        for (; i < lines.Count; i++)
        {
            string line = lines[i];
            if (line.Length == 0) break;
            StringBuilder sb = new();
            foreach (char c in line)
            {
                sb.Append(c switch
                {
                    '#' => "##",
                    'O' => "[]",
                    '.' => "..",
                    '@' => "@.",
                    _ => throw new ArgumentOutOfRangeException()
                });
            }
            line = sb.ToString();
            if (line.Contains('@'))
            {
                x = line.IndexOf('@');
                y = i;
                line = line.Replace('@', '.');
            }
            warehouse.Add(line.ToCharArray().ToList());
        }
        i++;
        List<Direction> directions = [];
        for (; i < lines.Count; i++)
        {
            directions.AddRange(lines[i].Select(c => c switch
            {
                '^' => Direction.Up,
                'v' => Direction.Down,
                '<' => Direction.Left,
                '>' => Direction.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(lines))
            }));
        }
        foreach (Direction direction in directions)
        {
            int newX = direction switch
            {
                Direction.Left => x - 1,
                Direction.Right => x + 1,
                _ => x
            };
            int newY = direction switch
            {
                Direction.Up => y - 1,
                Direction.Down => y + 1,
                _ => y
            };
            char c = warehouse[newY][newX];
            if (c == '#') continue;
            if (c == '.')
            {
                x = newX;
                y = newY;
            }
            else if (c == '[' && CanBoxMove(warehouse, x, y, direction))
            {
                if (direction is Direction.Up or Direction.Down)
                {
                    MoveDoubleBox(warehouse, newX, newY, direction);
                    MoveDoubleBox(warehouse, newX + 1, newY, direction);
                }
                else
                {
                    MoveDoubleBox(warehouse, newX, newY, direction);
                }
                x = newX;
                y = newY;
            }
            else if (c == ']' && CanBoxMove(warehouse, x, y, direction))
            {
                if (direction is Direction.Up or Direction.Down)
                {
                    MoveDoubleBox(warehouse, newX - 1, newY, direction);
                    MoveDoubleBox(warehouse, newX, newY, direction);
                }
                else
                {
                    MoveDoubleBox(warehouse, newX, newY, direction);
                }
                x = newX;
                y = newY;
            }
        }
        long result = 0;
        for (y = 0; y < warehouse.Count; y++)
        {
            List<char> list = warehouse[y];
            for (x = 0; x < list.Count; x++)
            {
                if (list[x] != '[') continue;
                result += 100 * y + x;
            }
        }
        return result;
    }

    private static bool CanBoxMove(List<List<char>> warehouse, int x, int y, Direction direction)
    {
        int newX = direction switch
        {
            Direction.Left => x - 1,
            Direction.Right => x + 1,
            _ => x
        };
        int newY = direction switch
        {
            Direction.Up => y - 1,
            Direction.Down => y + 1,
            _ => y
        };
        char c = warehouse[newY][newX];
        return c switch
        {
            '.' => true,
            '#' => false,
            '[' => CanBoxMove(warehouse, newX, newY, direction) && (direction == Direction.Left || CanBoxMove(warehouse, newX + 1, newY, direction)),
            ']' => CanBoxMove(warehouse, newX, newY, direction) && (direction == Direction.Right || CanBoxMove(warehouse, newX - 1, newY, direction)),
            _ => CanBoxMove(warehouse, newX, newY, direction)
        };
    }

    private static void MoveSingleBox(List<List<char>> warehouse, int x, int y, Direction direction)
    {
        int dx = direction switch
        {
            Direction.Left => -1,
            Direction.Right => 1,
            _ => 0
        };
        int dy = direction switch
        {
            Direction.Up => -1,
            Direction.Down => 1,
            _ => 0
        };
        while (true)
        {
            int newX = x + dx;
            int newY = y + dy;
            char c = warehouse[newY][newX];
            if (c == 'O')
            {
                x = newX;
                y = newY;
                continue;
            }
            warehouse[newY][newX] = 'O';
            break;
        }
    }

    private static void MoveDoubleBox(List<List<char>> warehouse, int x, int y, Direction direction)
    {
        int dx = direction switch
        {
            Direction.Left => -1,
            Direction.Right => 1,
            _ => 0
        };
        int dy = direction switch
        {
            Direction.Up => -1,
            Direction.Down => 1,
            _ => 0
        };
        Queue<Position> positions = new();
        positions.Enqueue(new Position(x, y));
        List<Position> toMove = [new(x, y)];
        while (positions.Count > 0)
        {
            Position position = positions.Dequeue();
            int newX = position.x + dx;
            int newY = position.y + dy;
            char c = warehouse[newY][newX];
            Position pos1, pos2;
            switch (direction)
            {
                case Direction.Up or Direction.Down when c == '[':
                    pos1 = new Position(newX, newY);
                    pos2 = new Position(newX + 1, newY);
                    positions.Enqueue(pos1);
                    positions.Enqueue(pos2);
                    if (!toMove.Contains(pos1))
                    {
                        toMove.Insert(0, pos1);
                    }
                    if (!toMove.Contains(pos2))
                    {
                        toMove.Insert(1, pos2);
                    }
                    break;
                case Direction.Up or Direction.Down when c == ']':
                    pos1 = new Position(newX - 1, newY);
                    pos2 = new Position(newX, newY);
                    positions.Enqueue(pos1);
                    positions.Enqueue(pos2);
                    if (!toMove.Contains(pos1))
                    {
                        toMove.Insert(0, pos1);
                    }
                    if (!toMove.Contains(pos2))
                    {
                        toMove.Insert(1, pos2);
                    }
                    break;
                case Direction.Left or Direction.Right when c is '[' or ']':
                    positions.Enqueue(new Position(newX, newY));
                    toMove.Insert(0, new Position(newX, newY));
                    break;
            }
        }
        foreach (Position position in toMove)
        {
            int newX = position.x + dx;
            int newY = position.y + dy;
            warehouse[newY][newX] = warehouse[position.y][position.x];
            warehouse[position.y][position.x] = '.';
        }
    }
}