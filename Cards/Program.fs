open System
open Logic
open Types.Games

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal

let x = 
    LDeck.createDeck()
    |> LDeck.shuffle

let y = 
    Deal.dealOne x

let gameState =
    {Deck = x;
    Players = [];
    Dealt = None}

let updateGameState g =
    {Deck = g.Deck; 
    Players = g.Players; 
    Dealt = g.Dealt}

[<EntryPoint>]
let main argv =
    let g1 =
        LPlayer.addPlayer gameState

    let g2 = 
        let y = Deal.dealOne g1.Deck
        {g1 with Deck = fst y; Dealt = Some (snd y)}

    // let g3 =
    //     let z = Deal.takeOne (g2.Players.Head.Hand, (Some g2.Dealt))
    //     {}

    //Output.DisplayHand() g2.Players.Head.Hand

    printfn "%A" (g2.Players.Head.Name)
    printfn "%A" (g2.Deck.Length)
    Console.ReadKey() |> ignore
    0 // return an integer exit code