using PaintedPoker.Game.Rules;
using PaintedPoker.Game.Table;

namespace PaintedPoker.Game.Builder;

public class GameBuilder
{
    public GameTable<CellMetadata, HeaderMetadata, HeaderMetadata> Build(GameOptions gameOptions)
    {
        var headers = Enumerable.Range(0, gameOptions.PlayerCount).Select(index => new HeaderMetadata($"Player {index}"));

        var table = new GameTable<CellMetadata, HeaderMetadata, HeaderMetadata>(headers);

        foreach (var round_size in GenerateRoundSizes(gameOptions.PlayerCount, gameOptions.DeckInfo))
        {
            var row = table.NewRow();
            for (int i = 0; i < row.Count; i++)
            {
                row[i] = new CellMetadata(EmptyResult.instance);
            }

            row.Header = new HeaderMetadata($"Round {round_size}");

            table.Rows.Add(row);
        }
        return table;
    }

    private IEnumerable<int> GenerateRoundSizes(int playerCount, DeckInfo deckInfo)
    {
        int max_round_size = GetMaxRoundSize(playerCount, deckInfo);

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

    private int GetMaxRoundSize(int playerCount, DeckInfo deckInfo)
    {
        return deckInfo.TotalCardsCount / playerCount;
    }
}