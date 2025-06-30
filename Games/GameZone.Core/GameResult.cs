namespace GameZone.Core;

public class GameResult
{
    public string GameName { get; set; } = "";
    public Difficulty Difficulty { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsSuccess { get; set; }
}
