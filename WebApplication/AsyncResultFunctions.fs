module WebApplication.AsyncResultFunctions

open FsToolkit.ErrorHandling

let resultDivide a b = if b = 0 then Error("Cannot divide by zero, sir") else Ok (a/b)

let slowResultDivide a b =
    asyncResult {
        let! x = resultDivide a b
        do! Async.Sleep 400
        return x
    }

