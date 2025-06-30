using GameZone.Core;
namespace GameZone.GuessNumber;

public class GameSettings
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int MaxAttempts { get; set; }

    public static GameSettings FromDifficulty(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => new GameSettings { Min = 1, Max = 10, MaxAttempts = 5 },
            Difficulty.Medium => new GameSettings { Min = 1, Max = 50, MaxAttempts = 7 },
            Difficulty.Hard => new GameSettings { Min = 1, Max = 100, MaxAttempts = 10 },
            _ => throw new ArgumentOutOfRangeException(nameof(difficulty), "Invalid difficulty level")
        };
    }
}
