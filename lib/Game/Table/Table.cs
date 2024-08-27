namespace PaintedPoker.Game.Table;

public class Table<TCell>
{
    private int _size;

    public Table(int size)
    {
        _size = size;
    }

    public List<Row<TCell>> Rows { get; set; } = new List<Row<TCell>>();
    public virtual int ColumnCount { get => _size; }

    public Row<TCell> NewRow()
    {
        return new Row<TCell>(ColumnCount);
    }
}