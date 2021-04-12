﻿module Data.Repository

open Npgsql.FSharp
open Types
open FSharp.Control.Tasks

let GetActors =
    task {
        return! Helpers.connectionString
                |> Sql.connect
                |> Sql.query "SELECT a.first_name, a.last_name FROM actor a INNER JOIN film_actor fa on fa.actor_id=a.actor_id where a.actor_id=1"
                |> Sql.executeAsync (fun read -> {| FullName = $"""{read.string "first_name"} {read.string "last_name"}""" |})
                //|> Sql.executeAsync Helpers.autoGeneratedRecordReader<Actor>
                //|> Async.AwaitTask
    }