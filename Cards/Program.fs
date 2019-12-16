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
        let y = Deal.dealOne gameState.Deck
        {Deck = fst y; Players = [ ]; Dealt = Some (snd y)}

    printfn "%A" (g1.Deck.Length)
    Console.ReadKey() |> ignore
    0 // return an integer exit code