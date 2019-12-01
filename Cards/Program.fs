open System
open Logic

let x = LDeck.createDeck

[<EntryPoint>]
let main argv =
    let y = LDeck.shuffle x
    let z = LDeck.dealCard y
    Console.ReadKey() |> ignore
    0 // return an integer exit code