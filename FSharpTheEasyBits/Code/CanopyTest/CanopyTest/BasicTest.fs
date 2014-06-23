module BasicTest

open canopy
open runner

let basic _ =
    "can open google" &&& fun _ ->
        url "http://www.google.com"
        (element "input[type='text']") << "Christchurch"
        click "button[aria-label='Google Search']"
        (elements "h3.r a") |> List.toArray |> (fun x -> x.[1]) |> click

let all _ =
    basic()