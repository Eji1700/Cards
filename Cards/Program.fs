﻿open System
open Logic
open Types.Games
open Types.Players

// Could do a match on "type" of action, ex Deal1

[<EntryPoint>]
let main argv =
    let house =
        {ID = House; 
        Name = "House"; 
        Hand = []; 
        Stack = 0m; 
        Bet = 0m }

    let initialGameState =
        {Deck = LDeck.CreateDeck() |> LDeck.Shuffle;
        Players = [house];
        PlayersTurnID = Player 1;
        Table = [];
        State = Start}

    Game.MainGameLoop initialGameState |> ignore 
    Console.ReadKey() |> ignore
    0 // return an integer exit code