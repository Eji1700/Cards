namespace Logic
    // module ListHelper = 
    //     let UpdateElement key f st =
    //             st |> List.map (fun (k, v) -> if k = key then k, f v else k, v)

    module Validate =
        open System
        let rec InputMoney() =
            match (System.Decimal.TryParse(Console.ReadLine())) with
            | (true, value) -> value
            | (false, _) ->  
                printfn "Please enter a valid amount of Money"
                InputMoney()

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

        let CreateDeck(): Deck =
            List.allPairs faces suits 
            |> List.map create

        let Shuffle (deck: Deck) : Deck = 
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
            InputMoney()

        let private createPlayer (ps:list<Player>) =
            {ID = ps.Length; Name = setName(); Hand = []; Stack = setMoney(); Bet = 0m}

        let AddPlayer g =
            {g with Players = (createPlayer g.Players) :: g.Players}
        
        let UpdatePlayer (g:Game) (up:Player) =
            ignore

    module Deal =
        // open ListHelper
        open Types.Deck
        open Types.Players
        open Types.Games

        let DealOne (d:Deck)  =
                ((d.Tail:Deck), d.Head) 

        let TakeOne id c plyrs  =
            plyrs
            |> List.map (fun (p) -> if p.ID = id then  {p with Hand = c::p.Hand} else p )
    
        let DealToPlayer id g =
            let d = DealOne g.Deck
            let c = snd d
            (fst d, c, TakeOne id (Some c) g.Players)

        let rec DealToEachPlayer plyrs =
            ignore

        // let rec DealInitalHands g =
        //         let players = 
        //             List.filter (fun (p) -> p.ID >= 0) g

        //         let rec eachPlayer = players
        //         x

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