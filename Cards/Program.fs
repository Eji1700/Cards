open System
open Logic
open Types.Games
open Types.Deck

//MAKE GAME STATE TYPE/OBJECT TO PASS TO ITSELF RECURSIVELY
//should have players and deck/dealer?  Then it just modifies itself and passes back?
// Could do a match on "type" of action, ex Deal

let a = LPlayer.createPlayer
let b = LPlayer.createPlayer

let table p np =
    np::p

let x = 
    LDeck.createDeck
    |> LDeck.shuffle


let gameState = {Deck = x;Players = [a]}

let gameState2 g b = 
    {Deck = g.Deck; Players = (table b g.Players.[0])}

 let y = Deal.test Deal.dealOne(x)


let blah = Deal.test  

[<EntryPoint>]
let main argv =

    printfn "%A" gameState2
    Console.ReadKey() |> ignore
    0 // return an integer exit code