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

        type Hand =
            Card Option List

        type Player =
            {ID:int ;Name:string; Hand:Hand; Stack:decimal; Bet:decimal;}

    module Games = 
        open Cards
        open Deck
        open Players

        type Game = 
            {Deck: Deck;
             Players: Player list;
             Table: Hand;
             PlayersTurnID:int}
        
        type Deal =
            Deck -> (Deck*Card)

        type PickupCard = 
            (Player*Card) -> Player