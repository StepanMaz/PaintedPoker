namespace PaintedPokerLib.Deck

open System.Runtime.InteropServices
open PaintedPokerLib.Cards
open System.Linq
open System

type DeckSize =
    | Small = 36
    | Large = 52

type DeckBuilder() =
    [<Literal>]
    let SMALL_DECK_RANKS_COUNT = 9

    [<Literal>]
    let LARGE_DECK_RANKS_COUNT = 13

    let mutable _shuffle: bool = false
    let mutable _deckSize: DeckSize = DeckSize.Small

    member this.Shuffle([<Optional; DefaultParameterValue(true)>] shuffle: bool) : DeckBuilder =
        _shuffle <- shuffle
        this

    member this.SetDeckSize(deckSize: DeckSize) : DeckBuilder =
        _deckSize <- deckSize
        this

    member this.Build() : IDeck<Card> =
        let ranks =
            Enum
                .GetValues<Rank>()
                .TakeLast(
                    match _deckSize with
                    | DeckSize.Small -> SMALL_DECK_RANKS_COUNT
                    | DeckSize.Large -> LARGE_DECK_RANKS_COUNT
                    | _ -> SMALL_DECK_RANKS_COUNT
                )

        let suits = Enum.GetValues<Suit>()

        let cards =
            seq {
                for rank in ranks do
                    for suit in suits do
                        yield { Suit = suit; Rank = rank }
            }

        new Deck<Card>(
            if _shuffle then
                this.shuffle (cards)
            else
                cards
        )

    member private this.shuffle<'a>(seq: 'a seq) : 'a seq =
        seq.OrderBy(fun x -> Random.Shared.Next())
