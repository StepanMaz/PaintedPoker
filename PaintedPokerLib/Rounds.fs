namespace PaintedPokerLib.Game

module Round =
    [<AbstractClass>]
    type RoundBase(cardCount: int) =
        member this.CardCount = cardCount
        abstract member Accept: visitor: IRoundVisitor<'T> -> 'T

    and IRoundVisitor<'T> =
        abstract member Visit: round: DefaultRound -> 'T
        abstract member Visit: round: NoTrumpCardsRound -> 'T
        abstract member Visit: round: BlindStakesRound -> 'T
        abstract member Visit: round: WinLosesRound -> 'T
        abstract member Visit: round: NoStakesRound -> 'T


    and DefaultRound(cardCount: int) =
        inherit RoundBase(cardCount)
        override this.Accept(visitor: IRoundVisitor<'T>) : 'T = visitor.Visit(this)

    and NoStakesRound(cardCount: int) =
        inherit RoundBase(cardCount)
        override this.Accept(visitor: IRoundVisitor<'T>) : 'T = visitor.Visit(this)

    and WinLosesRound(cardCount: int) =
        inherit RoundBase(cardCount)
        override this.Accept(visitor: IRoundVisitor<'T>) : 'T = visitor.Visit(this)

    and BlindStakesRound(cardCount: int) =
        inherit RoundBase(cardCount)
        override this.Accept(visitor: IRoundVisitor<'T>) : 'T = visitor.Visit(this)

    and NoTrumpCardsRound(cardCount: int) =
        inherit RoundBase(cardCount)
        override this.Accept(visitor: IRoundVisitor<'T>) : 'T = visitor.Visit(this)
