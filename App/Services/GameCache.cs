using PaintedPoker.Entities;

namespace PaintedPoker.Services;

public interface IGameCache
{
    public Task Set(GameData data);
    public Task<GameData?> Get(string name);
}

public class GameCache : IGameCache
{
    Dictionary<string, WeakReference<GameData>> _cache = new();

    Task IGameCache.Set(GameData data)
    {
        if (_cache.ContainsKey(data.Name))
        {
            _cache[data.Name] = new WeakReference<GameData>(data, false);
        }
        else
        {
            _cache.Add(data.Name, new WeakReference<GameData>(data, false));
        }
        return Task.CompletedTask;
    }

    public Task<GameData?> Get(string name)
    {
        WeakReference<GameData>? reference;
        var hasCache = _cache.TryGetValue(name, out reference);
        if (!hasCache || reference is null) return Task.FromResult<GameData?>(null);

        GameData? gameData;
        var hasRefValue = reference.TryGetTarget(out gameData);
        if (!hasRefValue || gameData is null) return Task.FromResult<GameData?>(null);

        return Task.FromResult<GameData?>(gameData);
    }
}