namespace Logic
    module Validate =
        open System
        let rec InputMoney() = //not used during testing
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
            [ Two; Three; Four;
            Five; Six; Seven;
            Eight; Nine; Ten;
            Jack; Queen; King; Ace]
        
        let (|Red|Black|) suit = //unused, maybe for other games
            match suit with
            | Diamonds | Hearts -> Red
            | Clubs | Spades -> Black

        let printcolor c = //ditto
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

        let private setMoney() = //unused for now
            Console.Clear()
            printfn "How much money do you have?"
            InputMoney() 

        let private createPlayer (ps:list<Player>) =
            {ID = (Player ps.Length ); 
            Name = setName(); 
            Hand = []; 
            //Stack = setMoney(); Commented out while testing.
            Stack = 100m; 
            Bet = 0m}

        let AddPlayer g =
            {g with Players = 
                    (createPlayer g.Players) :: g.Players}
     
        // Maybe not needed at all or maybe should be used more?
        let UpdatePlayer (plyrs:list<Player>) (newP:Player) =
            plyrs
            |> List.map (fun (p) -> if p.ID = newP.ID then  
                                        {p with Hand = newP.Hand} 
                                    else p )

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
                    |> List.head
            (p,h)

        let NextPlayer g =
            let highestPlyr =
                g.Players
                |> List.maxBy (fun p -> p.ID)
            if g.PlayersTurnID <> highestPlyr.ID then 
                g.Players
                |> List.sortBy (fun p -> p.ID)
                |> List.find (fun p -> p.ID > g.PlayersTurnID)
            else
                SelectHouse g.Players

    module Deal =
        open Types.Deck
        open Types.Players
        open Types.Games
        open LPlayer

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

        let rec DealInitalHand gameState hndsz =
        //Deals to player left of the dealer (lowest id) first
        //Then each other player in ascending ID order
        //Then the dealer, repeat until desired hand size
            match hndsz with
            | 0 -> gameState
            | _ -> 
                let plyrs, house = SplitHousePlayers gameState.Players
                let finalPlyrs, newDeck = DealToAll gameState.Deck plyrs []
                let finalDeck, finalHouse = DealToPlayer newDeck house
                let newGameState = {gameState with 
                                        Deck = finalDeck; 
                                        Players = finalHouse::finalPlyrs;}
                DealInitalHand newGameState (hndsz-1)  

        let SetupGame gameState hndsz =
            let newGameState = DealInitalHand gameState hndsz
            let minPlayer =
                    SelectPlayers newGameState.Players
                    |> List.minBy (fun p -> p.ID)
            {newGameState with
                PlayersTurnID = minPlayer.ID;
                State = PlayerTurn}     

    module Output =
        open Types.Cards
        open Types.Players
        open Types.Games
        open LPlayer
        open System

        let DisplayCard c =
            match c with 
            | c -> printf "%A of %A" c.Face c.Suit

        let rec DisplayHand (h:Hand) =
            match h with 
            | [] -> printf "\n"
            | _ -> match h.Head with
                    c ->  DisplayCard c
                          if h.Tail <> [] then printf " and a "
                          DisplayHand h.Tail

        let DisplayDealer g =
            printf "Dealer shows a "
            let dealer = SelectHouse g.Players
            DisplayHand dealer.Hand.Tail 

        let rec DisplayPlayersHand (plyrs:List<Player>) =
            match plyrs with
            | [] -> printf "\n"
            | _->
                let p = plyrs.Head
                printf"\n%s shows a " p.Name
                DisplayHand p.Hand |> ignore
                DisplayPlayersHand plyrs.Tail

        let DisplayPlayers g =
            let plyrs = 
                SelectPlayers g.Players
                |> List.sortBy (fun p -> p.ID)
            DisplayPlayersHand plyrs

        let DisplayGame g =
            DisplayPlayers g
            DisplayDealer g

    module Input =
        open System
        open Types.Games
        open LPlayer
        open Deal
        open Output
        // More dynamic menu system?
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
            Console.Clear()
            printfn "1 - Start New Game\n\
                    2 - Add Player\n\
                    O - Options\n\
                    Q - Quit"
            let choice = Console.ReadKey(true).KeyChar.ToString()
            match choice.ToLower() with
            | "1" -> 
                Console.Clear()
                {gameState with State = NewGame}
            | "2" -> 
                Console.Clear()
                {gameState with State = AddAPlayer}
            | "o" -> 
                Console.Clear()
                {gameState with State = Options}
            | "q" -> 
                Console.Clear()
                {gameState with State = Quit}
            | _ ->
                printfn "Please choose a valid option"
                Console.ReadKey(true) |> ignore
                Console.Clear()
                StartMenu gameState

        let rec OptionMenu gameState =
            //Add number of decks, min bet, max players, starting stack
            //Rules variants, maybe new games eventually.
            Console.Clear()
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

        let rec PlayerAction gameState = 
            Console.Clear()
            DisplayGame gameState
            printfn "1 - Show hands\n\
                    2 - Hit\n\
                    3 - Stay\n\
                    "
            let choice = Console.ReadKey(true).KeyChar.ToString()
            match choice.ToLower() with
            | "1" -> 
                DisplayGame gameState
                PlayerAction gameState
            | "2" -> 
                Console.Clear()
                let plyr = SelectPlayer gameState.PlayersTurnID gameState.Players
                let d, newP = DealToPlayer gameState.Deck plyr
                let newPlyrs = UpdatePlayer gameState.Players newP
                let newGameState = 
                    {gameState with 
                        Deck = d; 
                        Players = newPlyrs}
                PlayerAction newGameState
            | "3" -> 
                let nxtPlayer = NextPlayer gameState
                let newGameState =
                    {gameState with
                        PlayersTurnID = nxtPlayer.ID;}
                PlayerAction newGameState
            | _ ->
                printfn "Please choose a valid option"
                Console.ReadKey(true) |> ignore
                Console.Clear()
                PlayerAction gameState
        
        let QuitGame() =
            Console.Clear()
            printfn "Thank you for playing"
            Console.ReadKey(true)

    module Game =
        open System
        open LPlayer
        open Input
        open Types.Games
        open Deal
        open Output

        let rec MainGameLoop gameState =
            match gameState.State with
            | Start -> 
                let newGameState = StartMenu gameState
                MainGameLoop newGameState
            | Options -> 
                let newGameState = OptionMenu gameState
                MainGameLoop newGameState
            | AddAPlayer ->
                let g = AddPlayer gameState
                let newGameState = {g with State = Start}
                MainGameLoop newGameState
            | NewGame ->
                let plyrs = SelectPlayers(gameState.Players).Length
                if plyrs < 1 then
                    Console.Clear()
                    printfn "Add at least one player"
                    Console.ReadKey(true) |> ignore
                    let newGameState = {gameState with State = Start}
                    MainGameLoop newGameState
                else
                    let newGameState  = SetupGame gameState 2
                    Console.Clear()
                    DisplayGame newGameState
                    MainGameLoop newGameState
            | PlayerTurn ->
                let newGameState = PlayerAction gameState
                gameState
            | HouseTurn ->
                QuitGame() |> ignore
                gameState
            | Quit ->
                QuitGame() |> ignore
                gameState