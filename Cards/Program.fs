open System
open Types

let x = Deck.createDeck

[<EntryPoint>]
let main argv =
    printfn "%A" x
    0 // return an integer exit code