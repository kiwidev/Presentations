#r """packages\FSharp.Data.SqlClient.1.2.22\lib\net40\FSharp.Data.SqlClient.dll"""


open FSharp.Data

[<Literal>]
let connectionString = @"Data Source=(local);Initial Catalog=Northwind;Integrated Security=true;"


[<Literal>]
let query = "
   SELECT TOP 10 *
   FROM [dbo].[Categories]
"

type SalesPersonQuery = SqlCommandProvider<query, connectionString>

let cmd = new SalesPersonQuery()

 

cmd.AsyncExecute()
    |> Async.RunSynchronously
    |> Seq.toList
    |> List.length

 