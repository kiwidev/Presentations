// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

module Program

open canopy
open runner

[<EntryPoint>]
let main argv = 
    start firefox

    BasicTest.all()

    run()   

    printfn "press [enter] to exit"
    System.Console.ReadLine() |> ignore

    quit()
    0
