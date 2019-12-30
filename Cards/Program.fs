open System
open Logic
open Types.Games

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal1

[<EntryPoint>]
let main argv =
    let x = 
        LDeck.CreateDeck()
        |> LDeck.Shuffle

    let gameState =
        {Deck = x;
        Players = [];
        Dealt = None}

    let g1 =
        LPlayer.AddPlayer gameState

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