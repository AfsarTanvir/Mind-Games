using GameZone.Core;

namespace GameZone.DiceGame;

public class DiceRaceGame : IGame
{
    private readonly Random _random = new();
    private int _player1Pos = 1;
    private int _player2Pos = 1;
    private readonly bool _isComputerOpponent;
    private static readonly Dictionary<int, int> value = [];
    private readonly Dictionary<int, int> _trapCells = value;

    public string Name => "Dice Race";

    public DiceRaceGame(bool isComputerOpponent)
    {
        _isComputerOpponent = isComputerOpponent;
        GenerateTrapCells();
    }

    private void GenerateTrapCells()
    {
        _trapCells.Clear();
        int trapCount = 5; // you can randomize this too

        while (_trapCells.Count < trapCount)
        {
            int cell = _random.Next(2, 20); // traps not on start/end
            if (_trapCells.ContainsKey(cell)) continue;
            _trapCells[cell] = -_random.Next(1, 6); // -1 to -5
        }
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine("🎲 Welcome to Dice Race Game!");
        Console.WriteLine($"First to reach 20 wins. Traps ahead!");

        while (true)
        {
            PlayerTurn("Player 1", ref _player1Pos);
            if (_player1Pos >= 20)
            {
                Console.WriteLine("🏆 Player 1 wins!");
                break;
            }

            if (_isComputerOpponent)
            {
                PlayerTurn("Computer", ref _player2Pos, isComputer: true);
            }
            else
            {
                PlayerTurn("Player 2", ref _player2Pos);
            }

            if (_player2Pos >= 20)
            {
                Console.WriteLine(_isComputerOpponent ? "🤖 Computer wins!" : "🏆 Player 2 wins!");
                break;
            }

            PrintBoard();
            Console.WriteLine("Press any key for next round...");
            Console.ReadKey();
        }
    }

    private void PlayerTurn(string name, ref int position, bool isComputer = false)
    {
        Console.WriteLine($"\n{name}'s turn...");
        if (!isComputer)
        {
            Console.WriteLine("Press any key to roll dice...");
            Console.ReadKey();
        }

        int roll = _random.Next(1, 7);
        Console.WriteLine($"{name} rolled a {roll}");

        position += roll;

        if (_trapCells.TryGetValue(position, out int penalty))
        {
            Console.WriteLine($"💀 Oh no! {name} hit a trap: {penalty}");
            position += penalty;
        }

        if (position < 1) position = 1;
        if (position > 20) position = 20;

        Console.WriteLine($"{name} is now at position {position}");
    }

    private void PrintBoard()
    {
        Console.Write("\nBoard:");
        Console.Write("    ");
        for (int i = 1; i <= 20; i++)
            Console.Write($"{i,3}");

        Console.Write("\ntrap");
        Console.Write("      ");
        for (int i = 1; i <= 20; i++)
        {
            if (_trapCells.TryGetValue(i, out var trapValue))
                Console.Write($"{trapValue,3}");
            else
                Console.Write("   ");
        }

        Console.Write("\nPlayer1   ");
        for (int i = 1; i <= 20; i++)
        {
            Console.Write(_player1Pos == i ? "  * " : "   ");
        }

        Console.Write("\nPlayer2   ");
        for (int i = 1; i <= 20; i++)
        {
            Console.Write(_player2Pos == i ? "  * " : "   ");
        }

        Console.WriteLine();
    }
}
