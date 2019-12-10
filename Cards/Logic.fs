namespace Logic
    module Validate =
        open System
        let rec inputMoney() =
            match (System.Decimal.TryParse(Console.ReadLine())) with
            | (true, value) -> value
            | (false, _) ->  
                printfn "Please enter a valid amount of Money"
                inputMoney()

    module LCard =
        open Types.Cards

        let suits =
            [Spades; Hearts; Clubs; Diamonds]

        let faces =
            [ Two ; Three ; Four ; Five ; Six ; Seven
            ; Eight ; Nine ; Ten ; Jack ; Queen ; King ; Ace]
        
        let (|Red|Black|) suit =
            match suit with
            | Diamonds | Hearts -> Red
            | Clubs | Spades -> Black

        let printcolor c =
            match c with
            | Red -> "Red"
            | Black -> "Black"

        let create (f, s) =
            {Face = f; Suit = s}

    module LDeck =
        open Types.Deck
        open Types.Players
        open LCard

        let createDeck (): Deck =
            List.allPairs faces suits 
            |> List.map create

        let shuffle (deck: Deck) : Deck = 
            let random = System.Random()
            deck |> List.sortBy (fun x -> random.Next())

    module LPlayer =
        open Types.Players
        open System
        open Validate

        let setName() =
            printfn "What is your name?"
            Console.ReadLine()

        let setMoney() =
            printfn "How much money do you have?"
            inputMoney()

        let createPlayer() =
            {Name = setName(); Hand = []; Stack = setMoney(); Bet = 0m}

    module Deal =
        open Types.Cards
        open Types.Deck
        open Types.Players
        open Types.Games

        let drawOne (d:Deck) (p:Player) : Deck =
            let x:Hand = [d.Head]
            List.append p.Hand x
            d.Tail 
            
        let dealOne (d:Deck)  =
             (d.Tail:Deck), (d.Head:Card)

        let test (a:Deal) (d:Deck) =
            a 