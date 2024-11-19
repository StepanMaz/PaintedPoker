namespace PaintedPokerLib.Deck

open System.Collections.Generic
open System.Linq

type Deck<'TCard>(cards: IEnumerable<'TCard>) =
    let mutable deck: List<'TCard> = cards.ToList()

    interface IDeck<'TCard> with
        member this.CardCount = deck.Count

        member this.IsEmpty() = deck.Count = 0

        member this.Peek(count: int) : DeckResult<'TCard> =
            match deck.Count with
            | 0 -> NotEnoughCards
            | cardsCount when cardsCount < count -> PartialCards(deck :> IReadOnlyList<'TCard>, count - cardsCount)
            | _ -> AllCards(deck.Take(count).ToList() :> IReadOnlyList<'TCard>)

        member this.Draw(count: int) : DeckResult<'TCard> =
            let drawnCards = (this :> IDeck<'TCard>).Peek(count)
            deck <- deck.Skip(count).ToList()
            drawnCards

    interface IEnumerable<'TCard> with
        member this.GetEnumerator() =
            (deck.GetEnumerator() :> IEnumerator<'TCard>)

        member this.GetEnumerator() : System.Collections.IEnumerator =
            (deck.GetEnumerator() :> System.Collections.IEnumerator)
