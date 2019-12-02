namespace Logic
open Types

module LCard =
    open Cards

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
    open Deck
    open LCard

    let createDeck : Deck =
        List.allPairs faces suits 
        |> List.map create

    let shuffle (deck: Deck) : Deck = 
        let random = System.Random()
        deck |> List.sortBy (fun x -> random.Next())

    let dealCard (deck: Deck) : Deck =
        printfn "%A" deck.Head
        deck.Tail

module LPlayer =
    open System
    open Players

    //move to own module?
    let rec inputMoney() =
        match (System.Decimal.TryParse(Console.ReadLine())) with
        | (true, value) -> value
        | (false, _) ->  
            printfn "Please enter a valid amount of Money"
            inputMoney()

    let createPlayer =
        printfn "What is your name?"
        let n = Console.ReadLine()
        printfn "How much money do you have?"
        let s = inputMoney()
        {Name = n; Hand = []; Stack = s; Bet = 0m}