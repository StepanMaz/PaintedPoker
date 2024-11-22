namespace PaintedPokerLib.Game

module Results =
    [<AbstractClass>]
    type RoundResult() =
        abstract member Accept: visitor: IResultVisitor<'T> -> 'T

    and IResultVisitor<'T> =
        abstract member Visit: result: DefaultRoundResult -> 'T
        abstract member Visit: result: NoTrumpCardRoundResult -> 'T
        abstract member Visit: result: NoStakesRoundResult -> 'T
        abstract member Visit: result: WinLosesRoundResult -> 'T
        abstract member Visit: result: BlindStakesRoundResult -> 'T
        abstract member Visit: result: PartialResult -> 'T

    and DefaultRoundResult(stakes: int, wins: int) =
        inherit RoundResult()
        member this.Wins = wins
        member this.Stakes = stakes

        override this.Accept(visitor: IResultVisitor<'T>) : 'T = visitor.Visit(this)

    and NoTrumpCardRoundResult(stakes: int, wins: int) =
        inherit RoundResult()
        member this.Wins = wins
        member this.Stakes = stakes

        override this.Accept(visitor: IResultVisitor<'T>) : 'T = visitor.Visit(this)

    and NoStakesRoundResult(wins: int) =
        inherit RoundResult()
        member this.Wins = wins

        override this.Accept(visitor: IResultVisitor<'T>) : 'T = visitor.Visit(this)

    and WinLosesRoundResult(wins: int) =
        inherit RoundResult()
        member this.Wins = wins

        override this.Accept(visitor: IResultVisitor<'T>) : 'T = visitor.Visit(this)

    and BlindStakesRoundResult(stakes: int, wins: int) =
        inherit RoundResult()
        member this.Stakes = stakes
        member this.Wins = wins

        override this.Accept(visitor: IResultVisitor<'T>) : 'T = visitor.Visit(this)

    and PartialResult(stakes: int) =
        inherit RoundResult()
        member this.Stakes = stakes
        override this.Accept(visitor: IResultVisitor<'T>) : 'T = visitor.Visit(this)
