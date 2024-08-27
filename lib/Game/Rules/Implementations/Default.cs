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

    private static int CalculateDefaultResult(DefaultRoundResult result)
    {
        var (stakes, wins) = result;

        if (stakes < wins) return wins;

        if (wins < stakes) return (stakes - wins) * -10;

        if (wins == 0) return 5;

        return wins * 10;
    }

    private class ResultsVisitor : IGameResultVisitor<int>
    {
        public int VisitDefaultResult(DefaultRoundResult result)
        {
            return CalculateDefaultResult(result);
        }


        public int VisitPartialResult(PartialResult result)
        {
            return 0;
        }

        public int VisitEmptyResult(EmptyResult result)
        {
            return 0;
        }
    }
}