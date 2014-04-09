//
// Inspired by FSharpForFunAndProfit series (http://fsharpforfunandprofit.com)
// by Scott Wlaschin
// See http://fsharpforfunandprofit.com/posts/recipe-part2/ for many more details
// and a great explanation.
//

type MyDbEntity = { Id: int; First:string; Last: string; Age: int}
type MyInput = { First:string; Last: string; Age: int}


type Result<'a> =
    | Success of 'a
    | Failure of string

let ValidateAllOfTheThings input =
    if input.Age < 10 then
        Failure "You're too young to do this"
    else 
        Success input

let SaveToDatabase input =
    match input.First |> Seq.toList with
    | 'A'::_ -> Failure "Your name started with 'a'"
    | _ -> Success { MyDbEntity.First = input.First; Last = input.Last; Age = input.Age; Id = 45}

let EchoToUser result =
    match result with
    | Success _ -> "It all worked buddy!"
    | Failure msg -> sprintf "Sorry, no good %s" msg

let bind fx result =
    match result with
    | Success r -> fx r
    | Failure msg -> Failure msg

let (>>=) r fx = bind fx r

let MyBusinessMethod input =
    Success input
    >>= ValidateAllOfTheThings
    >>= SaveToDatabase
    |> EchoToUser


// Usage
MyBusinessMethod { First = "Fred";Last = "Dagg"; Age = 42 }