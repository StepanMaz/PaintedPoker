namespace PaintedPoker.Game.Table;

public interface IRow<TCell>
{
    int Count { get; }
    TCell this[int index]
    {
        get; set;
    }
}

public abstract class Table<TCell, TRow> where TRow : IRow<TCell>
{
    public abstract TRow NewRow();
    public abstract int ColumnCount { get; }
    public virtual IList<TRow> Rows { get; set; } = new List<TRow>();
}

public abstract class TableWithHeader<TCell, TRow, THeader> : Table<TCell, TRow> where TRow : IRow<TCell>
{
    public TableWithHeader(IEnumerable<THeader> headers)
    {
        Headers = headers.ToList();
    }

    public IList<THeader> Headers { get; set; }
}

public class GameTable<TCell, TPlayer, TRowHeader> : TableWithHeader<TCell, RowWithHeader<TCell, TRowHeader>, TPlayer>
{
    public GameTable(IEnumerable<TPlayer> headers) : base(headers) { }

    public override int ColumnCount => Headers.Count;

    public override RowWithHeader<TCell, TRowHeader> NewRow()
    {
        return new RowWithHeader<TCell, TRowHeader>(ColumnCount);
    }
}

public class Row<TCell> : IRow<TCell>
{
    private TCell[] _data;

    internal Row(int size)
    {
        Count = size;
        _data = new TCell[size];
    }

    public TCell this[int index]
    {
        get => _data[index];
        set => _data[index] = value;
    }

    public int Count { get; init; }
}

public class RowWithHeader<TCell, THeader> : Row<TCell>
{
    internal RowWithHeader(int size) : base(size) { }

#nullable disable
    public THeader Header { get; set; }
}
