open System
open Logic
open Types.Games
open Types.Deck

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal

let x = 
    LDeck.createDeck()
    |> LDeck.shuffle

[<EntryPoint>]
let main argv =
    let a = LPlayer.createPlayer()
    let b = LPlayer.createPlayer()
    printfn "%A" b
    printfn "%A" a
    Console.ReadKey() |> ignore
    0 // return an integer exit code