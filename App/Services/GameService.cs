using System.Dynamic;
using MongoDB.Driver;
using PaintedPoker.Entities;

namespace PaintedPoker.Services;

public interface IGameService
{
    Task<GameData> AddGame(GameData game);
    Task<GameData?> GetGameByName(string name);
    Task Save(GameData gameData);
}

public class GameService(IMongoDatabase database, ILogger<GameService> logger) : IGameService
{
    public IMongoCollection<GameData> Games => database.GetCollection<GameData>("games");
    public virtual async Task<GameData> AddGame(GameData game)
    {
        logger.LogInformation("New game {} created", game.Name);
        await Games.InsertOneAsync(game);
        return game;
    }

    public async Task Save(GameData gameData)
    {
        await Games.UpdateOneAsync(
            Builders<GameData>.Filter.Eq(x => x.Id, gameData.Id),
            Builders<GameData>.Update
                .Set(x => x.DeckSize, gameData.DeckSize)
                .Set(x => x.Name, gameData.Name)
                .Set(x => x.Rounds, gameData.Rounds)
                .Set(x => x.Players, gameData.Players)
        );
    }

    public virtual async Task<GameData?> GetGameByName(string name)
    {
        return await Games.Find(x => x.Name == name).FirstOrDefaultAsync();
    }
}

public class TrackingGameService : GameService
{
    public TrackingGameService(IMongoDatabase database, ILogger<GameService> logger) : base(database, logger)
    {
    }

    public override async Task<GameData> AddGame(GameData game)
    {
        var res = await base.AddGame(game);
        res.OnChange += g => Save(g);
        return res;
    }

    public override async Task<GameData?> GetGameByName(string name)
    {
        var res = await base.GetGameByName(name);
        if (res is null) return null;

        res.OnChange += g => Save(g);

        return res;
    }
}