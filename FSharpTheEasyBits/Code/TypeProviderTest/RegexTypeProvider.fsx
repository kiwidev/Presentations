// Regex type provider
// Requires the FSharpx.TypeProviders.Regex NuGet package (possibly change version number below)
#r "packages\FSharpx.TypeProviders.Regex.1.8.41\lib\40\FSharpx.TypeProviders.Regex.dll"

open FSharpx

type Animal = { Type: string; Name: string }
type Person = { First: string; Last: string }
type Thing = 
  | Animal of Animal 
  | Person of Person
  | Unknown of string


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
