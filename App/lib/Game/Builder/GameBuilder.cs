using PaintedPoker.Game.Rules;
using PaintedPoker.Game.Rules.Deck;
using TableType = PaintedPoker.Game.Table.GameTable<CellMetadata, HeaderMetadata, HeaderMetadata>;
namespace PaintedPoker.Game.Builder;

public class Game(TableType gameTable)
{
    public TableType GameTable => gameTable;

#nullable disable
    public event Action TableChanged;

    public void NotifyTableChanged()
    {
        TableChanged?.Invoke();
    }
}

public class GameBuilder
{
    public int MinPlayerCount => 2;
    public int MaxPlayerCount => ForceAllDeckUsage ? DeckInfo.TotalCardsCount - 1 : DeckInfo.TotalCardsCount;
    public int PlayerCount { get; set; } = 2;
    public DeckInfo DeckInfo { get; set; } = DeckInfo.Small;
    public bool UseRound_NoTrumpCards { get; set; } = true;
    public bool UseRound_BlindStakes { get; set; } = true;
    public bool UseRound_NegativeWins { get; set; } = true;
    public bool UseRound_NoStakes { get; set; } = true;

    /// <summary>
    /// Controls if all cards can be dealt to players
    /// </summary>
    public bool ForceAllDeckUsage { get; set; } = false;

    public Game Build()
    {
        return new Game(CreateTable());
    }

    private TableType CreateTable()
    {
        var headers = Enumerable.Range(0, PlayerCount).Select(index => new HeaderMetadata($"Player {index}"));

        var table = new TableType(headers);

        foreach (var round_size in GenerateRoundSizes(PlayerCount, DeckInfo, ForceAllDeckUsage))
        {
            var row = table.NewRow();
            for (int i = 0; i < row.Count; i++)
            {
                row[i] = new CellMetadata(new DefaultResult.Empty());
            }

            row.Header = new HeaderMetadata($"round {round_size}");

            table.Rows.Add(row);
        }

        if (UseRound_NoTrumpCards)
        {
            var row = table.NewRow();
            for (int i = 0; i < row.Count; i++)
            {
                row[i] = new CellMetadata(new DefaultResult.Empty());
            }

            row.Header = new HeaderMetadata($"no trump cards");

            table.Rows.Add(row);
        }

        if (UseRound_BlindStakes)
        {
            var row = table.NewRow();
            for (int i = 0; i < row.Count; i++)
            {
                row[i] = new CellMetadata(new DefaultResult.Empty());
            }

            row.Header = new HeaderMetadata($"blind stakes");

            table.Rows.Add(row);
        }

        if (UseRound_NegativeWins)
        {
            var row = table.NewRow();
            for (int i = 0; i < row.Count; i++)
            {
                row[i] = new CellMetadata(new NegativeRound.Empty());
            }

            row.Header = new HeaderMetadata($"wins loses");

            table.Rows.Add(row);
        }

        if (UseRound_NoStakes)
        {
            var row = table.NewRow();
            for (int i = 0; i < row.Count; i++)
            {
                row[i] = new CellMetadata(new WinsOnlyRoundResult.Empty());
            }

            row.Header = new HeaderMetadata($"no stakes");

            table.Rows.Add(row);
        }

        return table;
    }

    private IEnumerable<int> GenerateRoundSizes(int playerCount, DeckInfo deckInfo, bool useAllCards)
    {
        int max_round_size = GetMaxRoundSize(playerCount, deckInfo, useAllCards);

        for (int i = 1; i <= max_round_size; i++)
        {
            yield return i;
        }

        yield return max_round_size;

        for (int i = max_round_size; i >= 1; i--)
        {
            yield return i;
        }
    }

    private int GetMaxRoundSize(int playerCount, DeckInfo deckInfo, bool useAllCards)
    {
        if (!useAllCards && deckInfo.TotalCardsCount % playerCount == 0)
        {
            return deckInfo.TotalCardsCount / playerCount - 1;
        }

        return deckInfo.TotalCardsCount / playerCount;
    }
}