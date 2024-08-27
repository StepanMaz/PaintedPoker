namespace PaintedPoker.Game.Rules;

public record struct EmptyResult : IGameResult
{
    public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.VisitEmptyResult(this);
    public static readonly EmptyResult instance = new();
}

public record struct PartialResult(int stakes) : IGameResult
{
    public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.VisitPartialResult(this);
}

public record struct DefaultRoundResult(int stakes, int wins) : IGameResult
{
    public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.VisitDefaultResult(this);

}