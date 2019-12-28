open System
open Logic
open Types.Games

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal



// let g3 = 
//     let y = Deal.DealOne g2.Deck
//     let z = Deal.TakeOne (g2.Players.Head, (snd y))
//     {g2 with Deck = fst y; Players = z::g2.Players}

[<EntryPoint>]
let main argv =
    let x = 
        LDeck.createDeck()
        |> LDeck.shuffle

    let gameState =
        {Deck = x;
        Players = [];
        Dealt = None}

    let updateGameState g =
        {Deck = g.Deck; 
        Players = g.Players; 
        Dealt = g.Dealt}

    let g1 =
        LPlayer.addPlayer gameState

    let g2 = 
        let y = Deal.DealOne g1.Deck
        let z = (snd y)
        let x = {g1 with Deck = fst y; Dealt = (Some z)}
        Deal.TakeOne (x, 1)

    let g3 =    
        Deal.TakeOne (g2, 1)
        

    //let g4 = Output.DisplayHand g2.Players.Head.Hand

    printfn "%A" (g2.Players.Head.Hand.Head)

    Console.ReadKey() |> ignore
    0 // return an integer exit code