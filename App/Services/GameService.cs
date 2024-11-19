using MongoDB.Driver;
using PaintedPoker.Entities;

namespace PaintedPoker.Services;

public interface IGameService
{
    Task<GameData> AddGame(GameData game);
    Task<GameData?> GetGameByName(string name);
}

public class GameService(IMongoDatabase database) : IGameService
{
    public IMongoCollection<GameData> Games => database.GetCollection<GameData>("games");
    public async Task<GameData> AddGame(GameData game)
    {
        await Games.InsertOneAsync(game);
        return game;
    }

    public async Task<GameData?> GetGameByName(string name)
    {
        return await Games.Find(x => x.Name == name).FirstOrDefaultAsync();
    }
}