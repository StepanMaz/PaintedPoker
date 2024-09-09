namespace PaintedPoker.Game.Rules;

public interface IGameResult {
    public T Accept<T>(IGameResultVisitor<T> visitor);
}

public interface IGameResultVisitor<T> {
    public T Visit(DefaultResult result);
    public T Visit(DefaultResult.Partial result);
    public T Visit(DefaultResult.Empty result);
    public T Visit(WinsOnlyRoundResult result);
    public T Visit(WinsOnlyRoundResult.Empty result);
    public T Visit(NegativeRound result);
    public T Visit(NegativeRound.Empty result);
}

public interface IPointsCalculator {
    public int Calculate(IGameResult gameResult);
}

public interface IGameRules {
    public IPointsCalculator Calculator { get; }
}