using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaintedPoker.Utils;
using PaintedPokerLib.Deck;
using static PaintedPokerLib.Game.Round;
using static PaintedPokerLib.Game.Results;

namespace PaintedPoker.Entities;

#nullable disable
public class GameData
{
    public ObjectId Id { get; set; }
    public required string Name { get; set; }
    public required DeckSize DeckSize { get; set; }
    public required ICollection<RoundDef> Rounds { get; set; }
    public required ICollection<Player> Players { get; set; }

    public event Func<GameData, Task> OnChange;

    public Task NotifySelf() {
        return OnChange?.Invoke(this);
    }
}

public class RoundDef
{
    public required RoundBase Round { get; set; }

    [BsonSerializer(typeof(PlayerCollectionSerializer<RoundResult>))]
    public required PlayerCollection<RoundResult> Results { get; set; }
}


public class Player
{
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}