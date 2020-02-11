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
            Console.Clear()
            printfn "What is your name?"
            Console.ReadLine()

        let private setMoney() =
            Console.Clear()
            printfn "How much money do you have?"
            InputMoney() 

        let private createPlayer (ps:list<Player>) =
            {ID = (Player ps.Length ); Name = setName(); Hand = []; Stack = setMoney(); Bet = 0m}

        let AddPlayer g =
            {g with Players = (createPlayer g.Players) :: g.Players}
        
        let UpdatePlayer (plyrs:list<Player>) (newP:Player) =
            plyrs
            |> List.map (fun (p) -> if p.ID = newP.ID then  {p with Hand = newP.Hand} else p )

        let SelectPlayer id plyrs =
            plyrs
            |> List.find (fun p -> p.ID = id)

        let SelectPlayers plyrs =
            plyrs
            |> List.filter (fun p -> p.ID <> House)

        let SelectHouse plyrs =
            plyrs
            |> List.filter (fun p -> p.ID = House)
            |> List.head

        let SplitHousePlayers plyrs =
            let p = plyrs
                    |> List.filter (fun p -> p.ID <> House) 
            let h = plyrs
                    |> List.filter (fun p -> p.ID = House)
            (p,h)

    module Deal =
        open Types.Deck
        open Types.Players

        let DealToPlayer (d:Deck) p =
            match d with 
            | [] -> failwith "no cards left in the deck"
            | c :: d -> (d:Deck), {p with Hand = c::p.Hand}

        let rec DealToAll deck plyrs dealtPlayers =
            match plyrs with
            | [] -> (dealtPlayers, deck)
            | player :: plyrsRest ->
                let (newDeck, newPlayer) = DealToPlayer deck player
                let dealtPlayers = newPlayer :: dealtPlayers
                DealToAll newDeck plyrsRest dealtPlayers

        let rec DealInitalHand d tble (hndsz:int) =
            match hndsz with
            | 0 -> d, tble
            | _ -> 
            let plyrs, h = LPlayer.SplitHousePlayers tble
            let fplyrs, nd = DealToAll d plyrs []
            let fd, fh = DealToPlayer nd h.Head
            let ftble = fh::fplyrs
            DealInitalHand fd ftble (hndsz-1)

    module Input =
        open System
        open Types.Games
        open LPlayer
        // let rec MenuChoice f gameState =
        //     let choice = Console.ReadKey(true)
        //     match choice.KeyChar with
        //     | '1' ->
        //         f gameState
        //     | _ ->
        //         printfn "Please choose a valid option"
        //         Console.ReadKey(true) |> ignore
        //         MenuChoice f gameState

        // let rec getChoices lst =
        //     //translates the list of menu choices from lst/dic to value/text/function
        //     //Overkill.  Eventually needs to be done but in the meantime need to just get something working.
        //     let x = 1
        //     x

        // let rec MenuChoice lst gameState =
        //     // should take a list/dictionary of "Key" and "function", then print, then interpret the choice.
        //     let choice = Console.ReadKey(true)
        //     lst
        //     |> List.exists 

        let rec StartMenu gameState =
            printfn "1 - Start New Game\n2 - Options\n3 - Quit"
            let choice = Console.ReadKey(true)
            match choice.KeyChar with
            | '1' -> 
                Console.Clear()
                {gameState with State = NewGame}
            | '2' -> 
                Console.Clear()
                {gameState with State = Options}
            | '3' -> 
                Console.Clear()
                {gameState with State = Quit}
            | _ ->
                printfn "Please choose a valid option"
                Console.ReadKey(true) |> ignore
                Console.Clear()
                StartMenu gameState

        let rec OptionMenu gameState =
            printfn "There are currently no options, press 1 to go back"
            let choice = Console.ReadKey(true)
            match choice.KeyChar with   
                | '1' -> 
                    Console.Clear()
                    {gameState with State = Start}
                | _ ->
                    printfn "Please choose a valid option"
                    Console.ReadKey(true) |> ignore
                    Console.Clear()
                    OptionMenu gameState

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
                    c ->  DisplayCard c; DisplayHand h.Tail
    
    module Game =
        open System
        open LPlayer
        open Input
        open Types.Games

        let rec MainGameLoop gameState =
            match gameState.State with
            | Start -> 
                let newGameState = (StartMenu gameState)
                MainGameLoop newGameState
            | Options -> 
                let newGameState = (OptionMenu gameState)
                MainGameLoop newGameState
            | _ -> gameState