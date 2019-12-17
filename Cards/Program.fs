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
    Deal.DealOne x

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
    let z = Deal.TakeOne (g1.Players.Head, (snd y))
    {g1 with Deck = fst y; Players = z::g1.Players}

let g3 = 
    let y = Deal.DealOne g2.Deck
    let z = Deal.TakeOne (g2.Players.Head, (snd y))
    {g2 with Deck = fst y; Players = z::g2.Players}

[<EntryPoint>]
let main argv =
    let g4 = Output.DisplayHand g3.Players.Head.Hand

    printfn "%A" (g2.Players.Length)
    Console.ReadKey() |> ignore
    0 // return an integer exit code