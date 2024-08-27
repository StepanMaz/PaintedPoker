namespace PaintedPoker.Game.Rules;

public class ResultStringFormatter : IGameResultVisitor<string>
{

    public string VisitDefaultResult(DefaultRoundResult result)
    {
        return $"{result.wins} / {result.stakes}";
    }

    public string VisitPartialResult(PartialResult result)
    {
        return $"? / {result.stakes}";
    }

    public string VisitEmptyResult(EmptyResult result)
    {
        return $"? / ?";
    }
}
