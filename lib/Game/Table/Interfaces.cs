namespace PaintedPoker.Game.Table;

public interface IReadOnlyRow<TCell> {
    TCell this[int index] { get; }
}

public interface IRow<TCell> {
    TCell this[int index] { get; set; }
}