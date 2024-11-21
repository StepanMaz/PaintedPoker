namespace PaintedPokerLib.Game
open System.Threading.Tasks

type TurnResult<'TPlayer> = { Winner: 'TPlayer }

type TurnInfo = { TurnIndex: int }

type ITurnRunner<'TPlayer> =
    abstract member PlayTurn: api: IPlayersApi<'TPlayer> * info: TurnInfo -> Task<TurnResult<'TPlayer>>