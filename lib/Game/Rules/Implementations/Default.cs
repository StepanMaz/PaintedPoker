namespace PaintedPoker.Game.Rules;

public class DefaultRules : IGameRules
{
    public static readonly DefaultRules instance = new DefaultRules();

    public IPointsCalculator Calculator => DefaultPointsCalculator.instance;
}

public class DefaultPointsCalculator : IPointsCalculator
{
    public static readonly DefaultPointsCalculator instance = new DefaultPointsCalculator();

    public int Calculate(IGameResult gameResult)
    {
        return gameResult.Accept(new ResultsVisitor());
    }

    private static int GetWinsPoints(Wins wins)
    {
        return wins.value * 10;
    }

    private static int GetLossPoints(int difference)
    {
        return Math.Abs(difference) * -10;
    }

    private static int CalculateDefaultResult(Wins wins, Stakes stakes)
    {
        int _wins = wins.value, _stakes = stakes.value;

        if (_stakes < _wins) return _wins;

        if (_wins < _stakes) return GetLossPoints(_stakes - _wins);

        if (wins.value == 0) return 0;

        return GetWinsPoints(wins);
    }

    private class ResultsVisitor : BaseVisitor<int>
    {
        public override int Visit(DefaultResult result)
        {
            return CalculateDefaultResult(result.Wins, result.Stakes);
        }

        public override int Visit(NegativeRound result)
        {
            return GetLossPoints(result.Wins.value);
        }

        public override int Visit(WinsOnlyRoundResult result)
        {
            return GetWinsPoints(result.Wins);
        }
    }
}