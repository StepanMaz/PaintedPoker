using PaintedPoker.Game.Rules;
using PaintedPoker.Game.Table;

namespace PaintedPoker.Game.Services;

public record GameName(string name) {
    public string FormattedName => name.ToLower().Replace(" ", "-");
}

public class GameTableService(ILogger<GameTableService> logger) {
    private Dictionary<string, Table<IGameResult>> _games = new ();

    public void Add(GameName name, Table<IGameResult> table) {
        _games.Add(name.FormattedName, table);
        logger.LogInformation("New game added, {}. Current count: {}", name.name, _games.Count);
    }

    public Table<IGameResult> GetTable(GameName name) {
        return _games[name.FormattedName];
    }
}