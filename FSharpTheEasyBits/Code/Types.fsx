
// Record type
type MethodResult = { Result : int; Message: string }

// Create one
{ Result = 42; Message = "Meaning of life" }

// Comparisons are ok
{ Result = 42; Message = "Meaning of life" } = { Result = 42; Message = "Meaning of life" }

// We can construct new ones from old ones
let methodResult = { Result = 42; Message = "Meaning of life" }
{ methodResult with Message = "No, really"}


// Discriminated union
type IntResult = 
    | Result of int
    | Error of string

// Create one
Result 42
Error "Didn't work boss"


// Pattern matching
let printMe result =
    match result with
    | Result r -> sprintf "Worked, %d" r
    | Error e -> sprintf "No way: %s" e

printMe (Result 42)
printMe (Error "Didn't work boss")




// For additional


// Basic type
type Animal = { Type: string; Name: string }
type Person = { First: string; Last: string }



type Thing = 
  | Animal of Animal 
  | Person of Person
  | Unknown of string

let speak thing = 
  match thing with
  | Animal { Type = t; Name = name} -> (sprintf "Hi I'm a %s called %s" t name)
  | Person { First = first; Last = "Dagg"} -> (sprintf "Oh really mate, swill back that beer and call me %s" first)
  | Person { First = first; Last = last } -> (sprintf "Hi I'm a person called %s %s" first last)
  | Unknown str -> str

// Use it
let daisySpeech = Animal { Type = "Cow"; Name= "Daisy"} |> speak



// Reverse it

let (|Regex|_|) pattern input =
      let m = System.Text.RegularExpressions.Regex.Match(input, pattern)
      if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
      else None

let reverseSpeech whatTheySaid =
  match whatTheySaid with
  | Regex "Hi I'm a (.*) called (.*)" [t; daisy;] -> Animal { Name = daisy; Type = t }
  | Regex "Hi I'm a person called ([^ ]*) (.*)" [first; last] -> Person { First = first; Last = last }
  | str -> Unknown str

reverseSpeech daisySpeech

// Or with a type provider
// Requires the FSharpx.TypeProviders.Regex NuGet package (possibly change version number below)
#r "..\packages\FSharpx.TypeProviders.Regex.1.8.41\lib\40\FSharpx.TypeProviders.Regex.dll"
open FSharpx

let inline (|Check|_|) (matcher: string -> ^a) input =
    let r = matcher input
    match (^a : (member Success : bool) r) with
    | true -> Some(r)
    | false -> None

    
type AnimalRegex = Regex< "Hi I'm a (?<AnimalType>.*) called (?<Name>.*)" >
type PersonRegex = Regex< "Hi I'm a person called (?<First>[^ ]*) (?<Last>.*)" >

let reverseSpeechProvider whatTheySaid = 
    let animalMatch = AnimalRegex().Match
    let personMatch = PersonRegex().Match
    match whatTheySaid with
    | Check animalMatch a -> Animal { Name = a.Name.Value; Type = a.AnimalType.Value }
    | Check personMatch p -> Person { First = p.First.Value; Last = p.Last.Value }
    | _ -> Unknown whatTheySaid
    