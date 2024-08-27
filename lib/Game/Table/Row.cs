namespace PaintedPoker.Game.Table;

public class Row<TCell> : IRow<TCell>, IReadOnlyRow<TCell>
{
    private TCell[] _data;

    public IEnumerable<TCell> Cells => _data;

    internal Row(int size)
    {
        _data = new TCell[size];
    }

    public TCell this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }
}
