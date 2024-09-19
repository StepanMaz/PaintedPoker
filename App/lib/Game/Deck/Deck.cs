namespace PaintedPoker.Game.Deck;

public class Deck {
    protected Queue<Card> _cards;

    public int Count => _cards.Count;

    public Deck(IEnumerable<Card> cards) {
        _cards = new Queue<Card>(cards);
    }

    public IEnumerable<Card> DrawCards(int count) {
        if (count > _cards.Count) 
            throw new InvalidOperationException("Not enough cards in the deck.");
        
        var res = new List<Card>();

        for (int i = 0; i < count; i++)
        {   
            res.Add(_cards.Dequeue());
        }

        return res;
    }
}

public class TrumpDeck : Deck
{
    public Card TrumpCard { get; init; }

    public TrumpDeck(IEnumerable<Card> cards) : base(cards)
    {
        TrumpCard = cards.Last();
    }
}


