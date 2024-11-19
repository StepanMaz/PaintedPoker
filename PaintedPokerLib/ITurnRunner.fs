namespace PaintedPokerLib.Game

type TurnResult<'TPlayer> = { Winner: 'TPlayer }

type ITurnRunner<'TPlayer> =
    abstract member PlayTurn: IPlayersApi<'TPlayer> -> TurnResult<'TPlayer>