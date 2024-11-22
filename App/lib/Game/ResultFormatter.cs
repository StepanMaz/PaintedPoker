
using static PaintedPokerLib.Game.Results;

namespace PaintedPoker.Game;

public class ResultFormatter(string format) : IResultVisitor<string>
{
    const string UNSET = "-";

    string Format(object stakes, object wins)
    {
        return string.Format(format, stakes, wins);
    }

    public string Visit(DefaultRoundResult result) => Format(result.Stakes, result.Wins);

    public string Visit(NoTrumpCardRoundResult result) => Format(result.Stakes, result.Wins);

    public string Visit(NoStakesRoundResult result) => Format(UNSET, result.Wins);

    public string Visit(WinLosesRoundResult result) => Format(UNSET, -result.Wins);

    public string Visit(BlindStakesRoundResult result) => Format(result.Stakes, result.Wins);

    public string Visit(PartialResult result) => Format(result.Stakes, UNSET);
}

public static class ResultFormatterExtensions {
    private static ResultFormatter formatter = new ("{1} / {0}"); 
    public static string Format(this RoundResult round) {
        return round.Accept(formatter);
    }
}