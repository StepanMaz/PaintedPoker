namespace PaintedPoker.Game.Deck;

public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

public sealed record Card(Suit Suit, Rank Rank) : IComparable<Card>
{
    public int CompareTo(Card? other)
    {
        if (other == null) return 1;

        int rankComparison = Rank.CompareTo(other.Rank);
        if (rankComparison != 0) return rankComparison;

        return Suit.CompareTo(other.Suit);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Rank);
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}
