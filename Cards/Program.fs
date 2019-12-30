open System
open Logic
open Types.Games

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal1

[<EntryPoint>]
let main argv =
    let initialGameState =
        {Deck = LDeck.CreateDeck() |> LDeck.Shuffle;
        Players = [];
        Dealt = None
        }

    let g1 =
        LPlayer.AddPlayer initialGameState

    let g2 = 
        let y = Deal.DealOne g1.Deck
        let z = (snd y)
        let x = {g1 with Deck = fst y; Dealt = (Some z)}
        Deal.TakeOne 1 x

    let g3 =    
        Deal.TakeOne 1 g2
        
    //let g4 = Output.DisplayHand g2.Players.Head.Hand

    printfn "%A" (g2.Players.Head.Hand.Head)
    printfn "%A" (g3.Players.Head.Hand.Head)

    Console.ReadKey() |> ignore
    0 // return an integer exit code