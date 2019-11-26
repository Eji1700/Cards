open System
open Types

let x = Deck.createDeck
let y = Deck.createDeck2
let z = Deck.createDeck3

[<EntryPoint>]
let main argv =
    if x = y then printfn "true" else printfn "false"
    0 // return an integer exit code