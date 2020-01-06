namespace Logic
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
        
        let UpdatePlayer (plyrs:list<Player>) (newP:Player) =
            plyrs
            |> List.map (fun (p) -> if p.ID = newP.ID then  {p with Hand = newP.Hand} else p )

        let SelectPlayer id plyrs =
            plyrs
            |> List.filter (fun p -> p.ID = id) 
            |> List.head

    module Deal =
        // open ListHelper
        open Types.Deck
        open Types.Players
        open Types.Games

        let DealToPlayer (d:Deck) p =
            let d, c = d.Tail, d.Head
            (d:Deck), {p with Hand = Some c::p.Hand} 

        // let DealOne (d:Deck)  =
        //         ((d.Tail:Deck), d.Head) 

        // let TakeOne id c plyrs  =
        //     plyrs
        //     |> List.map (fun (p) -> if p.ID = id then  {p with Hand = c::p.Hand} else p )

        // let DealToSpecificPlayer d id plyrs =
        //     let d, c = DealOne d
        //     (d, (TakeOne idS ome c) plyrs)) (

        // let rec DealToAllPlayers d plyrs acc =
        //     match plyrs with 
        //     | [] -> ignore
        //     | _ -> 
        //         let nd, nPlyrs= DealToSpecificPlayer d plyrs.Head.ID plyrs
        //         DealToAllPlayers nd nPlyrs.Tail

        // let x = 1

        // let rec dealToEachPlayer plyrs =
        //     match plyrs with
        //     | [] -> ignore
        //     | _ -> ignore 

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
                    Some c ->  DisplayCard c; DisplayHand h.Tail
                    | None -> ignore