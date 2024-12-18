namespace PaintedPokerLib.Game

open System.Collections.Generic
open System.Linq
open Results
open System.Threading.Tasks

type RoundResult<'TPlayer> =
    { Results: IDictionary<'TPlayer, RoundResult> }

type RoundRunner<'TPlayer when 'TPlayer: equality>(playersApi: IPlayersApi<'TPlayer>, turnRunner: ITurnRunner<'TPlayer>) =
    member this.RunRound(round: Round.RoundBase) = round.Accept(this)

    interface Round.IRoundVisitor<Task<RoundResult<'TPlayer>>> with
        member this.Visit(round: Round.DefaultRound) =
            task {
                do!
                    playersApi.DealCard(
                        { PerPlayerCardCount = round.CardCount
                          ShowTrumpCard = true }
                    )

                let! stakes = playersApi.GetStakes()

                do!
                    playersApi.ReportStakes(
                        stakes
                        |> Seq.map (fun (KeyValue (k, v)) -> (k, PartialResult(v) :> RoundResult))
                        |> dict
                    )

                let roundRes = stakes.ToDictionary((fun k -> k.Key), (fun v -> (v.Value, 0)))

                for i = 1 to round.CardCount do
                    let! turnRes = turnRunner.PlayTurn(playersApi, { TurnIndex = i })
                    let (stakes, wins) = roundRes[turnRes.Winner]
                    roundRes[turnRes.Winner] <- (stakes, wins + 1)

                return
                    { Results =
                        roundRes
                        |> Seq.map (fun (KeyValue (key, (stakes, wins))) ->
                            (key, DefaultRoundResult(stakes, wins) :> RoundResult))
                        |> dict }
            }

        member this.Visit(round: Round.NoTrumpCardsRound) : Task<RoundResult<'TPlayer>> =
            task {
                do!
                    playersApi.DealCard(
                        { PerPlayerCardCount = round.CardCount
                          ShowTrumpCard = false }
                    )

                let! stakes = playersApi.GetStakes()

                do!
                    playersApi.ReportStakes(
                        stakes
                        |> Seq.map (fun (KeyValue (k, v)) -> (k, PartialResult(v) :> RoundResult))
                        |> dict
                    )

                let roundRes = stakes.ToDictionary((fun k -> k.Key), (fun v -> (v.Value, 0)))

                for i = 1 to round.CardCount do
                    let! turnRes = turnRunner.PlayTurn(playersApi, { TurnIndex = i })
                    let (stakes, wins) = roundRes[turnRes.Winner]
                    roundRes[turnRes.Winner] <- (stakes, wins + 1)

                return
                    { Results =
                        roundRes
                        |> Seq.map (fun (KeyValue (key, (stakes, wins))) ->
                            (key, NoTrumpCardRoundResult(stakes, wins) :> RoundResult))
                        |> dict }
            }

        member this.Visit(round: Round.BlindStakesRound) : Task<RoundResult<'TPlayer>> =
            task {
                let! stakes = playersApi.GetStakes()

                do!
                    playersApi.ReportStakes(
                        stakes
                        |> Seq.map (fun (KeyValue (k, v)) -> (k, PartialResult(v) :> RoundResult))
                        |> dict
                    )

                do!
                    playersApi.DealCard(
                        { PerPlayerCardCount = round.CardCount
                          ShowTrumpCard = false }
                    )

                let roundRes = stakes.ToDictionary((fun k -> k.Key), (fun v -> (v.Value, 0)))

                for i = 1 to round.CardCount do
                    let! turnRes = turnRunner.PlayTurn(playersApi, { TurnIndex = i })
                    let (stakes, wins) = roundRes[turnRes.Winner]
                    roundRes[turnRes.Winner] <- (stakes, wins + 1)

                return
                    { Results =
                        roundRes
                        |> Seq.map (fun (KeyValue (key, (stakes, wins))) ->
                            (key, BlindStakesRoundResult(stakes, wins) :> RoundResult))
                        |> dict }
            }

        member this.Visit(round: Round.WinLosesRound) : Task<RoundResult<'TPlayer>> =
            task {
                do!
                    playersApi.DealCard(
                        { PerPlayerCardCount = round.CardCount
                          ShowTrumpCard = false }
                    )

                let roundRes = Dictionary()

                for i = 1 to round.CardCount do
                    let! turnRes = turnRunner.PlayTurn(playersApi, { TurnIndex = i })

                    roundRes[turnRes.Winner] <- if roundRes.ContainsKey(turnRes.Winner) then
                                                    roundRes[turnRes.Winner] + 1
                                                else
                                                    1

                return
                    { Results =
                        roundRes
                        |> Seq.map (fun (KeyValue (key, wins)) -> (key, WinLosesRoundResult(wins) :> RoundResult))
                        |> dict }
            }

        member this.Visit(round: Round.NoStakesRound) : Task<RoundResult<'TPlayer>> =
            task {
                do!
                    playersApi.DealCard(
                        { PerPlayerCardCount = round.CardCount
                          ShowTrumpCard = false }
                    )

                let roundRes = Dictionary()

                for i = 1 to round.CardCount do
                    let! turnRes = turnRunner.PlayTurn(playersApi, { TurnIndex = i })
                    roundRes[turnRes.Winner] <- if roundRes.ContainsKey(turnRes.Winner) then
                                                    roundRes[turnRes.Winner] + 1
                                                else
                                                    1

                return
                    { Results =
                        roundRes
                        |> Seq.map (fun (KeyValue (key, wins)) -> (key, NoStakesRoundResult(wins) :> RoundResult))
                        |> dict }
            }
