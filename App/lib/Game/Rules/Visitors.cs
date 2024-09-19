namespace PaintedPoker.Game.Rules;

public abstract class BaseVisitor<T>(T @default = default!) : IGameResultVisitor<T>
{
    public virtual T Visit(DefaultResult result) => @default;

    public virtual T Visit(DefaultResult.Partial result) => @default;

    public virtual T Visit(DefaultResult.Empty result) => @default;

    public virtual T Visit(WinsOnlyRoundResult result) => @default;

    public virtual T Visit(WinsOnlyRoundResult.Empty result) => @default;

    public virtual T Visit(NegativeRound result) => @default;

    public virtual T Visit(NegativeRound.Empty result) => @default;
}

public class StringFormatterVisitor : BaseVisitor<string>
{
    private const string DEFAULT_ROUND_FORMAT = "{1} / {0}";
    public override string Visit(DefaultResult result) => string.Format(DEFAULT_ROUND_FORMAT, result.Stakes, result.Wins);

    public override string Visit(DefaultResult.Partial result) => string.Format(DEFAULT_ROUND_FORMAT, result.Stakes, "?");

    public override string Visit(DefaultResult.Empty result) => string.Format(DEFAULT_ROUND_FORMAT, "?", "?");

    public override string Visit(WinsOnlyRoundResult result) => string.Format("{0}", result.Wins);

    public override string Visit(WinsOnlyRoundResult.Empty result) => "?"; 

    public override string Visit(NegativeRound result) => string.Format("{0}", result.Wins);
    
    public override string Visit(NegativeRound.Empty result) => "?"; 
}