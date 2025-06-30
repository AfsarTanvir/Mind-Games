using GameZone.Core;
using GameZone.GuessNumber;
using GameZone.MemoryGame;

while (true)
{
    Console.Clear();
    Console.WriteLine("🎮 Welcome to Game Zone 🎮");
    Console.WriteLine("Select a game:");
    Console.WriteLine("1. Guess the Number");
    Console.WriteLine("2. Memory Game");
    Console.WriteLine("3. Dice Roll (Coming Soon)");
    Console.WriteLine("0. Exit");

    Console.Write("Enter your choice: ");
    var gameChoice = Console.ReadLine();

    switch (gameChoice)
    {
        case "1":
            PlayGuessNumberGame();
            break;

        case "2":
            PlayMemoryGame();
            break;

        case "3":
            Console.WriteLine("🎲 Dice Roll Game is under development...");
            Pause();
            break;

        case "0":
            Console.WriteLine("👋 Goodbye!");
            return;

        default:
            Console.WriteLine("❌ Invalid choice. Try again.");
            Pause();
            break;
    }
}

void PlayGuessNumberGame()
{
    Console.Clear();
    Console.WriteLine("🎯 Guess the Number Game");
    Console.WriteLine("Select difficulty:");
    Console.WriteLine("1. Easy");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Hard");

    var diffChoice = Console.ReadLine();
    var difficulty = diffChoice switch
    {
        "1" => Difficulty.Easy,
        "2" => Difficulty.Medium,
        "3" => Difficulty.Hard,
        _ => Difficulty.Easy
    };

    var game = new GuessTheNumberGame(difficulty);
    game.Start();

    Pause();
}

void PlayMemoryGame()
{
    Console.WriteLine("Select difficulty: 1. Easy 2. Medium 3. Hard");
    var memDiff = Console.ReadLine();

    var memDifficulty = memDiff switch
    {
        "1" => Difficulty.Easy,
        "2" => Difficulty.Medium,
        "3" => Difficulty.Hard,
        _ => Difficulty.Easy
    };

    var memoryGame = new MemoryGame(memDifficulty);
    memoryGame.Start();

    Pause();
}

void Pause()
{
    Console.WriteLine("\nPress any key to return to main menu...");
    Console.ReadKey();
}
