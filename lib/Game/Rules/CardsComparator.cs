using PaintedPoker.Game.Deck;

namespace PaintedPoker.Game.Rules;

public class DefaultCardsComparer : IComparer<Card>
{
    public static readonly DefaultCardsComparer Instance = new DefaultCardsComparer();

    public int Compare(Card? x, Card? y)
    {
        if (x is null || y is null) return 0;

        if (x.Suit == y.Suit) return (int)y.Rank - (int)x.Rank;

        return 0;
    }
}