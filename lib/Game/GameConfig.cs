using PaintedPoker.Game.Services;

namespace PaintedPoker.Game;

public enum DeckSize {
    Small,
    Large
}

public record GameInfo(GameName GameName, GameOptions GameOptions);

public class GameOptions {
    public required int PlayerCount { get; set; } 
    public required DeckSize DeckSize { get; set; } 
}