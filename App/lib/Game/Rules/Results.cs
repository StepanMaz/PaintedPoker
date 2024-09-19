namespace PaintedPoker.Game.Rules;

public record struct Stakes(int value)
{
    public override string ToString() => value.ToString();
}

public record struct Wins(int value)
{
    public override string ToString() => value.ToString();
}

public record DefaultResult(Stakes Stakes, Wins Wins) : IGameResult
{
    public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.Visit(this);

    public record Partial(Stakes Stakes) : IGameResult
    {
        public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.Visit(this);
    }

    public record Empty : IGameResult
    {
        public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.Visit(this);
    }
}

public record WinsOnlyRoundResult(Wins Wins) : IGameResult
{
    public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.Visit(this);


    public record Empty() : IGameResult
    {
        public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.Visit(this);
    }
}

public record NegativeRound(Wins Wins) : IGameResult
{
    public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.Visit(this);

    public record Empty() : IGameResult
    {
        public T Accept<T>(IGameResultVisitor<T> visitor) => visitor.Visit(this);
    }
}
