// define a unit of measure

open System

[<Measure>] type cm
[<Measure>] type inch

let paper = 29.97<cm>
let ruler = 12.0<inch>

let convertToCm x = x * 2.54<cm/inch>
let inline convertToInch x = x / 2.54<cm/inch>

let rulerInCm = convertToCm ruler

// Here's what a distance formula looks like without any units
let distance a t = 0.5 * a * (t * t)

distance 10.0 1.5

// And here we are with units
// Note we can't put inches in anywhere, or ms etc

[<Measure>] type m
[<Measure>] type sec

let distanceWithUnits (a:float<m/sec ^ 2>) (t:float<sec>) = 0.5 * a * (t * t)
distanceWithUnits 10.0<m/sec/sec> 1.5<sec>


// Or we can have simpler methods that just ensure that things are the same
10<m> + 20<m>

let genericDistance (a:float<'u>) (t:float<'t>) = 0.5 * a * (t * t)

// Use as before
genericDistance 10.0<m/sec/sec> 1.5<sec>

// Or with inches now
genericDistance 10.0<inch/sec/sec> 1.5<sec>

// combine a conversion
genericDistance (convertToInch 10.0<cm/sec/sec>) 1.5<sec>

// formatting using pipes
10.0<cm/sec/sec>
|> convertToInch
|> (fun a -> genericDistance a 1.5<sec>)
