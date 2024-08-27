namespace PaintedPoker.Game.Rules;

public interface IGameResult {
    public T Accept<T>(IGameResultVisitor<T> visitor);
}

public interface IGameResultVisitor<T> {
    public T VisitDefaultResult(DefaultRoundResult result);
    public T VisitPartialResult(PartialResult result);
    public T VisitEmptyResult(EmptyResult result);
}

public interface IPointsCalculator {
    public int Calculate(IGameResult gameResult);
}

public interface IGameRules {
    public IPointsCalculator Calculator { get; }
}