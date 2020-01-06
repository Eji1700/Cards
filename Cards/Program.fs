open System
open Logic
open Types.Games
open Types.Players

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal1

[<EntryPoint>]
let main argv =
 
    let house =
        {ID= -1; 
        Name = "House"; 
        Hand = []; 
        Stack = 0m; 
        Bet = 0m }

    let initialGameState =
        {Deck = LDeck.CreateDeck() |> LDeck.Shuffle;
        Players = [house];
        PlayersTurnID = 1;
        Table = []}

    let g1 =
        LPlayer.AddPlayer initialGameState

    let g2 =
        LPlayer.AddPlayer g1

    let g3 = 
        let d, tble = Deal.DealInitalHand g2.Deck g2.Players 2
        {g2 with
            Deck = d;
            Players = tble}

    let a = g3.Players.Item 0
    let b = g3.Players.Item 1
    let c = g3.Players.Item 2
    printfn "Player %A Hand %A ID %A" a.Name a.Hand a.ID
    printfn "Player %A Hand %A ID %A" b.Name b.Hand b.ID
    printfn "Player %A Hand %A ID %A" c.Name c.Hand c.ID

    Console.ReadKey() |> ignore
    0 // return an integer exit code