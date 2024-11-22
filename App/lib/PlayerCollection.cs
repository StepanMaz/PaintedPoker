using PaintedPoker.Entities;

namespace PaintedPoker.Utils;

public class PlayerCollection<TValue> : Dictionary<Guid, TValue>
{
    public TValue this[Player key]
    {
        get => this[key.Id];
        set => this[key.Id] = value;
    }

    public void Add(Player player, TValue value)
    {
        Add(player.Id, value);
    }

    public bool TryGetValue(Player player, out TValue value)
    {
        return TryGetValue(player.Id, out value!);
    }

    public bool ContainsKey(Player player)
    {
        return ContainsKey(player.Id);
    }
}
