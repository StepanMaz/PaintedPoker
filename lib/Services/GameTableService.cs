using PaintedPoker.Game.Rules;
using PaintedPoker.Game.Table;
using TableType = PaintedPoker.Game.Table.GameTable<CellMetadata, HeaderMetadata, HeaderMetadata>;

namespace PaintedPoker.Game.Services;

public record GameName(string name)
{
    public string FormattedName => name.ToLower().Replace(" ", "-");
}

public class GameTableService(ILogger<GameTableService> logger)
{
    private Dictionary<string, TableType> _games = new();

    public void Add(GameName name, TableType table)
    {
        _games.Add(name.FormattedName, table);
        logger.LogInformation("New game added, {}. Current count: {}", name.name, _games.Count);
    }

    public TableType GetTable(GameName name)
    {
        return _games[name.FormattedName];
    }
}