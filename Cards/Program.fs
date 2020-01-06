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
       let p = LPlayer.SelectPlayer g2.PlayersTurnID g2.Players
       let d, p = Deal.DealToPlayer g2.Deck p 
       {g2 with
            Players = LPlayer.UpdatePlayer g2.Players p;
            Deck = d}

    let g4 =
        let plyrs = LPlayer.SelectPlayers g2.Players
        let h = LPlayer.SelectHouse g2.Players
        let nPlyrs, d = Deal.DealToAll g2.Deck plyrs []
        {g2 with
            Players = h::nPlyrs; 
            Deck = d}        
        
    let a = g4.Players.Item 0
    let b = g4.Players.Item 1
    let c = g4.Players.Item 2
    printfn "Player %A Hand %A ID %A" a.Name a.Hand a.ID
    printfn "Player %A Hand %A ID %A" b.Name b.Hand b.ID
    printfn "Player %A Hand %A ID %A" c.Name c.Hand c.ID

    Console.ReadKey() |> ignore
    0 // return an integer exit code