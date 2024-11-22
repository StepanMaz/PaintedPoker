using PaintedPokerLib.Deck;

class DeckBuilderTests
{
    [Test]
    public void DeckShouldBeOfSizeSmall()
    {
        const int SMALL_DECK_SIZE = 36;

        var deck = new DeckBuilder().SetDeckSize(DeckSize.Small).Build();

        Assert.That(deck.CardCount, Is.EqualTo(SMALL_DECK_SIZE));
    }

    [Test]
    public void DeckShouldBeOfSizeLarge()
    {
        const int LARGE_DECK_SIZE = 52;

        var deck = new DeckBuilder().SetDeckSize(DeckSize.Large).Build();

        Assert.That(deck.CardCount, Is.EqualTo(LARGE_DECK_SIZE));
    }

    [Test]
    public void DeckShouldBeShuffled()
    {
        var notShuffled = new DeckBuilder().Build();
        var shuffled = new DeckBuilder().Shuffle().Build();

        Assert.IsFalse(notShuffled.SequenceEqual(shuffled));
    }
}