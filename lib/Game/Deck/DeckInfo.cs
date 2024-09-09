namespace PaintedPoker.Game.Rules;

public interface IDeckSizeInfo {
    int TotalCardsCount { get; }
}

public class DeckInfo(int valuesCount) : IDeckSizeInfo
{
    public static DeckInfo Small = new DeckInfo(9);
    public static DeckInfo Large = new DeckInfo(12);

    public int TotalCardsCount { get => valuesCount * 4; }
}