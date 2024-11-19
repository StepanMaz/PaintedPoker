
using static PaintedPokerLib.Game.Results;

namespace PaintedPoker.Game;

public class ResultFormatter : IResultVisitor<string>
{
    public static readonly ResultFormatter Instance = new ResultFormatter();
    const string FORMAT = "{1} / {0}";
    const string UNSET = "-";

    string Format(object stakes, object wins)
    {
        return string.Format(FORMAT, stakes, wins);
    }

    public string Visit(DefaultRoundResult result) => Format(result.Stakes, result.Wins);

    public string Visit(NoTrumpCardRoundResult result) => Format(result.Stakes, result.Wins);

    public string Visit(NoStakesRoundResult result) => Format(UNSET, result.Wins);

    public string Visit(WinLosesRoundResult result) => Format(UNSET, result.Wins);

    public string Visit(BlindStakesRoundResult result) => Format(result.Stakes, result.Wins);

    public string Visit(PartialResult result) => Format(result.Stakes, UNSET);
}