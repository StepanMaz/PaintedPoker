namespace PaintedPokerLib.Cards

type Suit =
    | Hearts = 1
    | Diamonds = 2
    | Clubs = 3
    | Spades = 4

type Rank =
    | Two = 2
    | Three = 3
    | Four = 4
    | Five = 5
    | Six = 6
    | Seven = 7
    | Eight = 8
    | Nine = 9
    | Ten = 10
    | Jack = 11
    | Queen = 12
    | King = 13
    | Ace = 14

[<Struct>]
type Card = { Suit: Suit; Rank: Rank }
