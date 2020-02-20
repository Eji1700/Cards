namespace Types
    module Cards =
        type Suits =
            | Spades
            | Hearts
            | Clubs
            | Diamonds

        type Faces =
            | Two | Three | Four | Five | Six | Seven
            | Eight | Nine | Ten | Jack | Queen | King | Ace

        type Card =
            {Face:Faces; Suit: Suits}    

    module Deck =  
        open Cards
        
        type Deck = 
            Card list

    module Players =
        open Cards

        type Identity =
            | House
            | Player of playerID : int

        type Hand =
            Card List

        type Player =
            {ID:Identity ;Name:string; Hand:Hand; Stack:decimal; Bet:decimal;}

    module Games = 
        open Cards
        open Deck
        open Players

        type State =
            | Start
            | Options
            | AddAPlayer
            | NewGame
            | PlayerTurn
            | HouseTurn
            | Quit

        type Game = 
            {Deck: Deck;
             Players: Player list;
             Table: Hand;
             PlayersTurnID:Identity;
             State: State}
        
        type Deal =
            (Deck*Player) -> (Deck*Card)