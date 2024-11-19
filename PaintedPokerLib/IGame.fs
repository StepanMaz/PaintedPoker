namespace PaintedPokerLib.Game

open System.Collections.Generic
open System.Threading.Tasks

type IGameInfo<'TPlayer> =
    abstract member Players: IReadOnlyList<'TPlayer>

type DealCardsConfig =
    { PerPlayerCardCount: int
      ShowTrumpCard: bool }

type IPlayersApi<'TPlayer> =
    abstract member DealCard: DealCardsConfig -> Task
    abstract member GetStakes: unit -> Task<IDictionary<'TPlayer, int>>
    abstract member ReportStakes: IDictionary<'TPlayer, Results.RoundResult> -> Task