namespace PaintedPoker.Game;

using static PaintedPokerLib.Game.Results;

class ResultNormalizer : IResultVisitor<(int?, int?)>
{
    public (int? stakes, int? wins) Normalize(RoundResult? result)
    {
        if (result is null) return (null, null);

        return result.Accept(this);
    }

    (int?, int?) IResultVisitor<(int?, int?)>.Visit(DefaultRoundResult result)
    {
        return (result.Stakes, result.Wins);
    }

    (int?, int?) IResultVisitor<(int?, int?)>.Visit(NoTrumpCardRoundResult result)
    {
        return (result.Stakes, result.Wins);
    }

    (int?, int?) IResultVisitor<(int?, int?)>.Visit(NoStakesRoundResult result)
    {
        return (null, result.Wins);
    }

    (int?, int?) IResultVisitor<(int?, int?)>.Visit(WinLosesRoundResult result)
    {
        return (null, result.Wins);
    }

    (int?, int?) IResultVisitor<(int?, int?)>.Visit(BlindStakesRoundResult result)
    {
        return (result.Stakes, result.Wins);
    }

    (int?, int?) IResultVisitor<(int?, int?)>.Visit(PartialResult result)
    {
        return (result.Stakes, null);
    }
}

public static class ResultNormalizerExtensions
{
    private static ResultNormalizer normalizer = new();
    public static (int? stakes, int? wins) Normalize(this RoundResult? result)
    {
        if (result is null) return (null, null);
        return result.Accept(normalizer);
    }
}