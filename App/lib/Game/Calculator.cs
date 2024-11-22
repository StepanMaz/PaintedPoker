using static PaintedPokerLib.Game.Results;
using Result = PaintedPokerLib.Game.Results.RoundResult;

namespace PaintedPoker.Game;

interface ICalculator
{
    int? Calculate(Result result);
}

public class PointsCalculator : IResultVisitor<int>, ICalculator
{
    public static readonly PointsCalculator Instance = new PointsCalculator();
    public int? Calculate(Result result)
    {
        return result.Accept(this);
    }

    private int GetPoints(int stakes, int wins)
    {
        if (wins > stakes) return wins;
        if (wins == stakes) return GetPoints(wins);

        return (wins - stakes) * 10;
    }

    private int GetPoints(int wins) {
        if(wins == 0) return 5;
        return wins * 10;
    }

    int IResultVisitor<int>.Visit(DefaultRoundResult result)
    {
        return GetPoints(result.Stakes, result.Wins);
    }

    int IResultVisitor<int>.Visit(NoTrumpCardRoundResult result)
    {
        return GetPoints(result.Stakes, result.Wins);
    }

    int IResultVisitor<int>.Visit(NoStakesRoundResult result)
    {
        return GetPoints(result.Wins); 
    }

    int IResultVisitor<int>.Visit(WinLosesRoundResult result)
    {
        return -GetPoints(result.Wins); 
    }

    int IResultVisitor<int>.Visit(BlindStakesRoundResult result)
    {
        return GetPoints(result.Stakes, result.Wins);
    }

    public int Visit(PartialResult result)
    {
        return 0;
    }
}