namespace WebApplication.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open WebApplication
open FsToolkit.ErrorHandling
open System.Threading.Tasks
open FSharp.Control.Tasks
open Data.Types
open Data.Repository

[<ApiController>]
[<Route("[controller]")>]
type WeatherForecastController (logger : ILogger<WeatherForecastController>) =
    inherit ControllerBase()
    
    let summaries =
        [|
            "Freezing"
            "Bracing"
            "Chilly"
            "Cool"
            "Mild"
            "Warm"
            "Balmy"
            "Hot"
            "Sweltering"
            "Scorching"
        |]
        
    member private this.GetRandomData =
        let rng = Random()
        [|
            for index in 0..4 ->
                { Date = DateTime.Now.AddDays(float index)
                  TemperatureC = rng.Next(-20,55)
                  Summary = summaries.[rng.Next(summaries.Length)] }
        |]
    [<HttpGet>]
    [<ProducesResponseType(typeof<Int32>, 200)>]
    member this.Get(a: int, b: int) =
        task {  
            let! a = AsyncResultFunctions.intermediateFunc a b
            do! Task.Delay 100
            return a |> this.MapResult
        }
        
    [<HttpGet>]
    [<ProducesResponseType(typeof<List<Actor>>, 200)>]
    [<Route("actors")>]
    member this.GetActors() =
        task {  
            return! GetActors
        }    
        
    member private this.MapResult res =
        match res with
        | Ok val'    -> this.Ok({| Value = val'|}) :> ActionResult
        | Error err  -> this.BadRequest({| Message = err |}) :> ActionResult
        