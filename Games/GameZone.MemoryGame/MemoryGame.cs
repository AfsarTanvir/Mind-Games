namespace GameZone.MemoryGame;
using GameZone.Core;

public class MemoryGame : IGame
{
    private char[,] _board = null!;
    private bool[,] _revealed = null!;
    private readonly int _rows;
    private readonly int _cols;
    public string Name => "Memory Game";
    private readonly Difficulty _difficulty;

    public MemoryGame(Difficulty difficulty)
    {
        _difficulty = difficulty;
        switch (difficulty)
        {
            case Difficulty.Easy:
                _rows = 2; _cols = 2;
                break;
            case Difficulty.Medium:
                _rows = 2; _cols = 4;
                break;
            case Difficulty.Hard:
                _rows = 4; _cols = 4;
                break;
            default:
                _rows = 2; _cols = 2;
                break;
        }

        InitializeBoard();
    }

    private void InitializeBoard()
    {
        int totalCards = _rows * _cols;
        var symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        var needed = totalCards / 2;

        var selected = symbols.Take(needed).ToList();
        var allSymbols = selected.Concat(selected).OrderBy(_ => Guid.NewGuid()).ToArray();

        _board = new char[_rows, _cols];
        _revealed = new bool[_rows, _cols];

        int index = 0;
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                _board[r, c] = allSymbols[index++];
                _revealed[r, c] = false;
            }
        }
    }

    public void Start()
    {
        while (!IsGameComplete())
        {
            Console.Clear();
            PrintBoard();

            Console.Write("Select first card (row col): ");
            var first = ReadCoordinates();
            Reveal(first.Item1, first.Item2);

            Console.Clear();
            PrintBoard();

            Console.Write("Select second card (row col): ");
            var second = ReadCoordinates();
            Reveal(second.Item1, second.Item2);

            Console.Clear();
            PrintBoard();

            if (_board[first.Item1, first.Item2] == _board[second.Item1, second.Item2])
            {
                Console.WriteLine("🎉 It's a match!");
            }
            else
            {
                Console.WriteLine("❌ Not a match.");
                _revealed[first.Item1, first.Item2] = false;
                _revealed[second.Item1, second.Item2] = false;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        Console.Clear();
        PrintBoard();
        Console.WriteLine("🏆 You matched all the pairs!");
    }

    private (int, int) ReadCoordinates()
    {
        while (true)
        {
            var input = Console.ReadLine()?.Split(' ');
            if (input?.Length != 2) continue;

            if (int.TryParse(input[0], out var r) && int.TryParse(input[1], out var c))
            {
                r -= 1; c -= 1;
                if (r >= 0 && r < _rows && c >= 0 && c < _cols && !_revealed[r, c])
                    return (r, c);
            }

            Console.WriteLine("❗ Invalid input. Try again (row col): ");
        }
    }

    private void Reveal(int r, int c)
    {
        _revealed[r, c] = true;
    }

    private bool IsGameComplete()
    {
        for (int r = 0; r < _rows; r++)
            for (int c = 0; c < _cols; c++)
                if (!_revealed[r, c]) return false;

        return true;
    }

    private void PrintBoard()
    {
        Console.WriteLine("Memory Board:");
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                Console.Write(_revealed[r, c] ? $" {_board[r, c]} " : " * ");
            }
            Console.WriteLine();
        }
    }
}
