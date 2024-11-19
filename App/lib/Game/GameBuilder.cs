using PaintedPoker.Entities;
using PaintedPokerLib.Deck;
using PaintedPokerLib.Game;

namespace PaintedPoker.Game;

public class GameBuilder
{
    string _name = "";
    ICollection<Player> _players = [];
    ICollection<RoundDef> _rounds = [];
    DeckSize _deckSize;
    public GameBuilder SetName(string name)
    {
        _name = name;
        return this;
    }

    public GameBuilder SetPlayers(IEnumerable<Player> players)
    {
        _players = players.ToList();
        return this;
    }

    public GameBuilder SetRounds(IEnumerable<Round.RoundBase> rounds)
    {
        _rounds = rounds.Select(r => new RoundDef()
        {
            Round = r,
            Results = []
        }).ToList();

        return this;
    }

    public GameBuilder SetDeckSize(DeckSize deckSize)
    {
        _deckSize = deckSize;
        return this;
    }

    public GameBuilder SetRounds(Action<RoundBuilder> configure)
    {
        var builder = new RoundBuilder();
        builder.SetDeckSize(_deckSize);
        builder.SetPlayerCount(_players.Count);

        configure(builder);

        _rounds = builder.Build().Select(r => new RoundDef()
        {
            Round = r,
            Results = []
        }).ToList();

        return this;
    }

    public GameBuilder ConfigureRounds()
    {
        return this;
    }

    public GameData Build()
    {
        return new GameData()
        {
            Name = _name,
            Players = _players,
            Rounds = _rounds,
            DeckSize = _deckSize
        };
    }
}

public class RoundBuilder
{
    DeckSize _deckSize;
    int _playerCount;
    bool includeDefaultRounds = true,
    includeNoStakesRound = false,
    includeWinLosesRound = false,
    includeNoTrumpCardRound = false,
    includeBlindStakesRound = false;
    public RoundBuilder SetDeckSize(DeckSize deckSize)
    {
        _deckSize = deckSize;

        return this;
    }

    public RoundBuilder SetPlayerCount(int count)
    {
        _playerCount = count;
        return this;
    }


    public RoundBuilder IncludeNoStakesRound(bool include)
    {
        includeNoStakesRound = include;
        return this;
    }

    public RoundBuilder IncludeWinLosesRound(bool include)
    {
        includeWinLosesRound = include;
        return this;
    }

    public RoundBuilder IncludeNoTrumpCardRound(bool include)
    {
        includeNoTrumpCardRound = include;
        return this;
    }

    public RoundBuilder IncludeBlindStakesRound(bool include)
    {
        includeBlindStakesRound = include;
        return this;
    }

    public RoundBuilder IncludeDefaultRounds(bool include)
    {
        includeDefaultRounds = include;
        return this;
    }

    public IEnumerable<Round.RoundBase> Build()
    {
        int maxDeckSize = (int)_deckSize / _playerCount;
        if (includeDefaultRounds)
        {
            for (int i = 1; i <= maxDeckSize; i++)
            {
                yield return new Round.DefaultRound(i);
            }
            yield return new Round.DefaultRound(maxDeckSize);
            for (int i = maxDeckSize; i > 0; i--)
            {
                yield return new Round.DefaultRound(i);
            }
        }

        if (includeBlindStakesRound)
            yield return new Round.BlindStakesRound(maxDeckSize);

        if (includeWinLosesRound)
            yield return new Round.WinLosesRound(maxDeckSize);

        if (includeNoStakesRound)
            yield return new Round.NoStakesRound(maxDeckSize);

        if (includeNoTrumpCardRound)
            yield return new Round.NoTrumpCardsRound(maxDeckSize);
    }
}
