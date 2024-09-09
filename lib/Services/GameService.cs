namespace PaintedPoker.Game.Services;
using Game = PaintedPoker.Game.Builder.Game;

public record GameName(string name)
{
    public string FormattedName => name.ToLower().Replace(" ", "-");
}

public class GameService(ILogger<GameService> logger)
{
    private Dictionary<string, Game> _games = new();

    public void Add(GameName name, Game game)
    {
        _games.Add(name.FormattedName, game);
        logger.LogInformation("New game added, {}. Current count: {}", name.name, _games.Count);
    }

    public bool HasGame(GameName name) {
        return _games.ContainsKey(name.FormattedName);
    }

    public Game GetGame(GameName name)
    {
        return _games[name.FormattedName];
    }
}