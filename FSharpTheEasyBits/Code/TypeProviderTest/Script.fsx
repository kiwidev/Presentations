// Uses SqlProvider from 
// http://www.pinksquirrellabs.com/post/2013/12/09/The-Erasing-SQL-type-provider.aspx
// https://github.com/fsprojects/SQLProvider/
/// 
// Copyright 2013, Ross McKinlay
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.


#r """packages\SQLProvider.0.0.9-alpha\lib\net40\FSharp.Data.SqlProvider.dll"""

open FSharp.Data.Sql

type sql = SqlDataProvider<"Data Source=(local);Initial Catalog=Northwind;Integrated Security=true;">

let products = sql.GetDataContext().``[dbo].[Products]``

products
|> Seq.filter (fun x-> x.Discontinued)
|> Seq.map (fun x -> x.ProductName)
|> Seq.map (sprintf "%s")
|> Seq.toArray


sql.GetDataContext().``[dbo].[Categories]``.Individuals.``As CategoryName``.``3, Confections``