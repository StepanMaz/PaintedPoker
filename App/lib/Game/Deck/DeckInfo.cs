using PaintedPoker.Game.Deck;

namespace PaintedPoker.Game.Rules.Deck;

public interface IDeckSizeInfo
{
    int TotalCardsCount { get; }
}

public class DeckInfo(int valuesCount) : IDeckSizeInfo
{
    public static DeckInfo Small = new DeckInfo(9);
    public static DeckInfo Large = new DeckInfo(12);

    public int ValuesCount => valuesCount;
    public int TotalCardsCount => valuesCount * 4;
}

public static class DeckInfoExtensions
{
    public static IEnumerable<Rank> DeckRanks(this DeckInfo deckInfo)
    {
        var all_ranks = Enum.GetValues<Rank>();

        return all_ranks.TakeLast(deckInfo.ValuesCount);
    }
}