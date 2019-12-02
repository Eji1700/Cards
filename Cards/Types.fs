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

    type Card=
        {Face:Faces; Suit: Suits}    

module Deck =  
    open Cards
    
    type Deck = 
        Card list

module Players =
    open Cards

    type Hand =
        Card list

    type Player =
        {Name:string; Hand:Hand; Stack:decimal; Bet:decimal;}