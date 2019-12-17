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
        open LCard

        let createDeck (): Deck =
            List.allPairs faces suits 
            |> List.map create

        let shuffle (deck: Deck) : Deck = 
            let random = System.Random()
            deck |> List.sortBy (fun x -> random.Next())

    module LPlayer =
        open Types.Players
        open Types.Games
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

        let addPlayer g =
            {g with Players = createPlayer() :: g.Players}

    module Deal =
        open Types.Games

        let dealOne: Deal =
            fun (d) -> d.Tail, d.Head

        let takeOne: PickupCard =
            fun (h,c) -> Some c::h