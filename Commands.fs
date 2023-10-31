namespace FSBot

open System
open System.Threading.Tasks
open DSharpPlus
open DSharpPlus.CommandsNext
open DSharpPlus.CommandsNext.Attributes
open DSharpPlus.Entities

type Commands() =
    inherit BaseCommandModule()

    [<Command "hi"; Description "Say hello to F#">]
    member this.Hi (ctx: CommandContext) =
        task {
            sprintf "Hello %s from F#!" ctx.User.Mention
            |> ctx.RespondAsync |> ignore
        } :> Task