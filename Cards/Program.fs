open System
open Logic

let x = LDeck.createDeck

[<EntryPoint>]
let main argv =
    let a = LPlayer.createPlayer
    let y = LDeck.shuffle x
    let z = LDeck.dealCard y
    printfn "%A" a.Name
    printfn "%A" a.Stack
    printfn "%A" a.Bet
    printfn "%A" a.Hand
    Console.ReadKey() |> ignore
    0 // return an integer exit code