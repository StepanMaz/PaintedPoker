namespace PaintedPokerLib.Deck

open System.Collections.Generic
open System.Runtime.CompilerServices

type DeckResult<'TCard> =
    | AllCards of Cards: IReadOnlyList<'TCard>
    | PartialCards of Cards: IReadOnlyList<'TCard> * MissingCount: int
    | NotEnoughCards

type IReadOnlyDeck<'TCard> =
    inherit IEnumerable<'TCard>
    abstract member CardCount: int
    abstract member IsEmpty: unit -> bool
    abstract member Peek: count: int -> DeckResult<'TCard>

type IDeck<'TCard> =
    inherit IReadOnlyDeck<'TCard>
    abstract member Draw: count: int -> DeckResult<'TCard>

module DeckExtensions =
    [<Extension>]
    let PeekOne (deck: IReadOnlyDeck<'TCard>) =
        if (deck.IsEmpty()) then
            None
        else
            let cards = deck.Peek(1)

            match cards with
            | AllCards cards -> Some(cards[0])
            | PartialCards (cards, _) ->
                match cards.Count with
                | a when a > 0 -> Some(cards.[0])
                | _ -> None
            | NotEnoughCards -> None

    [<Extension>]
    let DrawOne (deck: IDeck<'TCard>) =
        if (deck.IsEmpty()) then
            None
        else
            let cards = deck.Draw(1)

            match cards with
            | AllCards cards -> Some(cards[0])
            | PartialCards (cards, _) ->
                match cards.Count with
                | a when a > 0 -> Some(cards.[0])
                | _ -> None
            | NotEnoughCards -> None
