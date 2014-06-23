
// Some simple common Monads

// Option

let optionInt = Some 4
let (optionIntWithNone:Option<int>) = None


// Option with generics
let inline safesquare input = 
    match input with
    | Some s -> s * s
    | None -> Unchecked.defaultof<'a>

// Reimplementing standard monadic bind
let bind fx = function
    | Some s -> fx s
    | None -> None

Some(4) |> bind (fun x -> Some(x + 1))

// Or use the built in operators
Some(4) |> Option.bind (fun x -> Some(x + 1))

Some(4) |> Option.map (fun x -> x + 1)



// Basic exception handling
let tryDivide x y =
    try
        Some(x / y)
    with
        | _ -> None

exception MyError of string

let tryDivideWithError x y =
    try
        if y = 0 then raise (MyError "y cannot by 0")
        Some(x / y)
    with
        | MyError(e) -> None 
