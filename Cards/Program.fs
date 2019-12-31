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
        LPlayer.AddPlayer g1

    let g3 = 
        Deal.DealOne g2

    let g4 =    
        Deal.TakeOne 1 g3
    
    let g5 =
        Deal.DealOne g4
    
    let g6 =
        Deal.TakeOne 1 g5

    let g7 =
        Deal.DealOne g6

    let g8 =
        Deal.TakeOne 2 g7
        
    //let g4 = Output.DisplayHand g2.Players.Head.Hand
    let a = g8.Players.Item 0
    let b = g8.Players.Item 1
    printfn "Player %A Hand %A ID %A" a.Name a.Hand a.ID
    printfn "Player %A Hand %A ID %A" b.Name b.Hand b.ID


    Console.ReadKey() |> ignore
    0 // return an integer exit code