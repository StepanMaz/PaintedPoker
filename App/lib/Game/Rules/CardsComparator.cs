using PaintedPoker.Game.Deck;

namespace PaintedPoker.Game.Rules;

public class CardsDefaultComparer : IComparer<Card>
{
    public static readonly CardsDefaultComparer Instance = new CardsDefaultComparer();

    public virtual int Compare(Card? x, Card? y)
    {
        if (x is null || y is null) return 0;

        if (x.Suit == y.Suit) return (int)y.Rank - (int)x.Rank;

        return 0;
    }
}

public class TrumpCardComparer(Suit trumpSuit) : CardsDefaultComparer
{
    public override int Compare(Card? x, Card? y)
    {
        var defaultComparer = new CardsDefaultComparer();

        if (x is null || y is null) return 0;

        if (x.Suit == trumpSuit && y.Suit != trumpSuit) return (int)x.Rank + (int)y.Rank;
        if (y.Suit == trumpSuit && x.Suit != trumpSuit) return -(int)x.Rank - (int)y.Rank;

        return base.Compare(x, y);
    }
}