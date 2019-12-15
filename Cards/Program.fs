open System
open Logic

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal

let x = 
    LDeck.createDeck()
    |> LDeck.shuffle

let y = 
    Deal.dealOne x

[<EntryPoint>]
let main argv =
    printfn "%A" (snd y)
    Console.ReadKey() |> ignore
    0 // return an integer exit code