namespace Logic
    module ListHelper = 
        let updateElement key f st =
                st |> List.map (fun (k, v) -> if k = key then k, f v else k, v)

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

        let private setName() =
            printfn "What is your name?"
            Console.ReadLine()

        let private setMoney() =
            printfn "How much money do you have?"
            inputMoney()

        let private createPlayer (ps:list<Player>) =
            {ID = ps.Length + 1; Name = setName(); Hand = []; Stack = setMoney(); Bet = 0m}

        let addPlayer g =
            {g with Players = (createPlayer g.Players) :: g.Players}
        
        let updatePlayer (g:Game) (up:Player) =
            ignore

    module Deal =
        open ListHelper
        open Types.Cards
        open Types.Deck
        open Types.Players
        open Types.Games

        let DealOne (d:Deck) =
            (d.Tail:Deck), (d.Head: Card)
        
        let TakeOne (g:Game, id) =
            {g with Players =
                        g.Players
                        |> List.map (fun (p) -> if p.ID = id then  {p with Hand = g.Dealt::p.Hand} else p )
            }
    
    module Output =
        open Types.Cards
        open Types.Players

        let DisplayCard c =
            match c with 
            | c -> printfn "%A of %A" c.Face c.Suit

        let rec DisplayHand (h:Hand) =
            match h with 
            | [] -> ignore
            | _ -> match h.Head with
                    | Some c -> DisplayCard c; DisplayHand h.Tail
                    | None -> ignore