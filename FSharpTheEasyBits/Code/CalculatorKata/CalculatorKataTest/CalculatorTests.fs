namespace CalculatorKataTest

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open CalculatorKata

[<TestClass>]
type CalculatorKataTest() = 

    let calculator = Calculator()

    [<TestMethod>]
    member x.``Adding 1 number produces that number`` () = 
        
        let result = calculator.Add(1)
        Assert.AreEqual(1, result)

    [<TestMethod>]
    member x.``Adding 2 numbers adds them together`` () =

        let result = calculator.Add(1,2)
        Assert.AreEqual(3, result)

    [<TestMethod>]
    member x.``Adding 3 numbers adds them together`` () =

        let result = calculator.Add(1,2,3)
        Assert.AreEqual(6, result)

    [<TestMethod>]
    [<ExpectedException(typedefof<ArgumentOutOfRangeException>)>]
    member x.``Negative number should throw exception`` () =
        let result = calculator.Add(-1)
        Assert.Fail "Didn't throw exception"