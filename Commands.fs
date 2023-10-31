namespace FSBot

open System
open System.Threading.Tasks
open DSharpPlus
open DSharpPlus.CommandsNext
open DSharpPlus.CommandsNext.Attributes
open DSharpPlus.Entities

type Commands() =
    inherit BaseCommandModule()

    //You can declare the command itself, and its description with a tag like this
    [<Command "hi"; Description "Say hello to F#">]
    member this.Hi (ctx: CommandContext) =
        task {
            sprintf "Hello %s from F#!" ctx.User.Mention
            |> ctx.RespondAsync |> ignore
        } :> Task //Command methods always need to return Task, not Task<unit>!