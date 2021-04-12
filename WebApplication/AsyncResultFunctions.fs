module WebApplication.AsyncResultFunctions

open FsToolkit.ErrorHandling
open System.Threading.Tasks

let resultDivide a b = if b = 0 then Error("Cannot divide by zero, sir") else Ok (float a/float b)

let slowResultDivide a b =
    result {
        let! x = resultDivide a b
        return x+2.
    }

let intermediateFunc a b =
    taskResult {
        let! res = slowResultDivide a b
        do! Task.Delay 40
        return res.ToString()
    }
    
let blah = Seq.foldBack (fun a state -> state-a) [1..30] 100