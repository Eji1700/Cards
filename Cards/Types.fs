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
    
    let suits =
        [Spades; Hearts; Clubs; Diamonds]

    let faces =
        [ Two ; Three ; Four ; Five ; Six ; Seven
        ; Eight ; Nine ; Ten ; Jack ; Queen ; King ; Ace]
    
    let (|Red|Black|) suit =
        match suit with
        | Diamonds | Hearts -> Red
        | Clubs | Spades -> Black

    let printcolor c =
        match c with
        | Red -> "Red"
        | Black -> "Black"

    let create (f, s) =
        {Face = f; Suit = s}

module Deck =  
    open Cards
    type Deck = 
        Card list

    let createDeck : Deck  =
         [for suit in suits do
            for face in faces do
                yield {Face=face; Suit=suit}]

    let createDeck2 =
        Seq.allPairs faces suits 
        |> Seq.map (fun (face, suit) -> { Face=face; Suit=suit })

    let createDeck3 =
        Seq.allPairs faces suits 
        |> Seq.map create