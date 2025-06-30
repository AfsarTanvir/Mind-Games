namespace GameZone.GuessNumber;
using GameZone.Core;

public class GuessTheNumberGame : IGame
{
    private readonly GameSettings _settings;
    private readonly int _secretNumber;
    public string Name => "Guess the Number";
    private readonly Difficulty _difficulty;

    public GuessTheNumberGame(Difficulty difficulty)
    {
        _difficulty = difficulty;
        _settings = GameSettings.FromDifficulty(difficulty);
        var random = new Random();
        _secretNumber = random.Next(_settings.Min, _settings.Max + 1);
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"🎯 Difficulty: {_difficulty}");
        Console.WriteLine($"Guess the number between {_settings.Min} and {_settings.Max}");
        int attempts = 0;

        while (attempts < _settings.MaxAttempts)
        {
            Console.Write($"Attempt {attempts + 1}/{_settings.MaxAttempts}: ");
            if (!int.TryParse(Console.ReadLine(), out var guess))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }

            attempts++;

            if (guess == _secretNumber)
            {
                Console.WriteLine("🎉 Correct! You won!");
                return;
            }

            Console.WriteLine(guess < _secretNumber ? "Too low!" : "Too high!");
        }

        Console.WriteLine($"❌ You lost! The number was {_secretNumber}");
    }
}
