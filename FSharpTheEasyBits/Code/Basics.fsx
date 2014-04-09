
// Assign a value
let number = 3

// Create our first function
let add x y = x + y

// Create our first function that looks like C#
let addWithTypes (x:int) (y:int) :int = 
  x + y

// Now use it
add 4 5



// Currying (partial application)
let add3 = add 3

add3 4


// Pipelining
let pipelineResult = 4 |> add3


// Lamdas
let add3Lambda = (fun x -> x + 3)

4 |> add3Lambda
add3Lambda 4

4 |> (fun x -> x + 3)

// Lists and ranges
[0..10]

// Combining
[0..10]
|> List.map add3
|> List.filter (fun x -> x % 2 = 0)


// Or in a function

let printOdd nums =
    let isOdd = (fun x -> x % 2 = 0)
    nums
    |> List.filter isOdd
    |> List.map (sprintf "%d")

printOdd [0..50]