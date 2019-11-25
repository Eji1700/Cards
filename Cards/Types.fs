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

    let (|Red|Black|) suit =
        match suit with
        | Diamonds | Hearts -> Red
        | Clubs | Spades -> Black