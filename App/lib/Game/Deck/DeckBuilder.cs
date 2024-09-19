using PaintedPoker.Game.Rules.Deck;

namespace PaintedPoker.Game.Deck;

public class DeckBuilder
{
    private DeckInfo _deckInfo = DeckInfo.Small;
    private bool _shuffle = true;

    public DeckBuilder SetDeckInfo(DeckInfo info)
    {
        _deckInfo = info;

        return this;
    }

    public DeckBuilder SetShuffle(bool shuffle)
    {
        _shuffle = shuffle;

        return this;
    }

    public Deck Build()
    {
        var cards = Enum.GetValues<Suit>().SelectMany(suit => _deckInfo.DeckRanks().Select(rank => new Card(suit, rank)));

        if (_shuffle)
        {
            var shuffled_cards = cards.OrderBy(_ => Random.Shared.NextDouble());

            return new Deck(shuffled_cards);
        }

        return new Deck(cards);
    }
}