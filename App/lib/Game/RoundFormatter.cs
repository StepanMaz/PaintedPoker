using PaintedPokerLib.Game;

namespace PaintedPoker.Game;

public class RoundFormatter : Round.IRoundVisitor<string>
{
    public static readonly RoundFormatter Instance = new RoundFormatter();

    const string FORMAT = "{0} ({1})";

    string Format(object name, object count) {
        return string.Format(FORMAT, name, count);
    }

    public string Visit(Round.DefaultRound round)
    {
        return Format("Default", round.CardCount);
    }

    public string Visit(Round.NoTrumpCardsRound round)
    {
        return Format("No Trump Cards", round.CardCount);
    }

    public string Visit(Round.BlindStakesRound round)
    {
        return Format("Blind Stakes", round.CardCount);
    }

    public string Visit(Round.WinLosesRound round)
    {
        return Format("Win Loses", round.CardCount);
    }

    public string Visit(Round.NoStakesRound round)
    {
        return Format("Gold", round.CardCount);
    }
}