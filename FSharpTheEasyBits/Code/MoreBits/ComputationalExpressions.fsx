
// Using the option monad

// Old school chaining

Some 4
    |> Option.map ((+) 1)
    |> Option.bind (fun x -> Some(x * 2))
    |> function
        | Some(x) -> sprintf "Answer was %d" x
        | None -> "No cigar buddy"


// Minimum functionality for a computational expression
// see http://msdn.microsoft.com/en-us/library/dd233182.aspx for a fuller list
// of functions that can be implemented

type MaybeBuilder() =
    member this.Bind(x,f) = Option.bind f x
    member this.Return(x) = Some x

let maybe = new MaybeBuilder()

let doStuff input = 
    maybe 
        {
        let! x = input
        let x = x + 1
        let! x = Some(x * 2)
        return x
        }
    |> function
        | Some(x) -> sprintf "Answer was %d" x
        | None -> "No cigar buddy"

doStuff (Some 4)
doStuff None


// async expressions
#r "System.Net.Http.dll"

open System.Net.Http

let grabStuffFromService (url:string) =
    
    async {
        // use is the equivalent of C# using
        use client = new HttpClient()
        let sw = new System.Diagnostics.Stopwatch()
        sw.Start()
        let! response = client.GetAsync("http://www.google.com") |> Async.AwaitTask
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        sw.Stop()
        return (content, sw.ElapsedMilliseconds)
    }

let grabFromServices =
    async {
        let! responses = ["http://www.google.com"; "http://www.bing.com"; "http://www.microsoft.com"]
                            |> List.map (fun url -> async {
                                                            let! response = grabStuffFromService url
                                                            printfn "Request to %s took %dms" url (snd response)
                                                            return match response with
                                                                   | (content, elapsed) -> (url, content, elapsed)
                                                          })
                            |> Async.Parallel
                            
        let maxString = responses
                        |> Array.map (function
                                      | (url, content, elapsed) -> (url, (String.length content)))
                        |> Array.maxBy snd
        printf "The largest string was from %s, with length %d." (fst maxString) (snd maxString)

        return maxString
    }

grabFromServices
    |> Async.RunSynchronously
    |> ignore