using PaintedPoker.Game.Rules;

public class HeaderMetadata(string displayName)
{
    public string DisplayName { get; set; } = displayName;
}
public class CellMetadata(IGameResult gameResult)
{
    public IGameResult GameResult { get; set; } = gameResult;
}