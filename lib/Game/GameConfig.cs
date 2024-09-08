using PaintedPoker.Game.Services;

namespace PaintedPoker.Game;

public enum DeckSize
{
    Small,
    Large
}

public interface IDeckSizeInfo
{
    public int TotalCardsCount { get; }
}


public class DeckInfo(int valuesCount) : IDeckSizeInfo
{
    public static DeckInfo Small = new DeckInfo(9);
    public static DeckInfo Large = new DeckInfo(12);

    public int TotalCardsCount { get => valuesCount * 4; }
}

public record GameInfo(GameName GameName, GameOptions GameOptions);

public class GameOptions
{
    public required int PlayerCount { get; set; }
    public required DeckInfo DeckInfo { get; set; }
}