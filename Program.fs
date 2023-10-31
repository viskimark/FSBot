namespace FSBot

module Program =
    open System.IO
    open System.Threading.Tasks
    open Microsoft.Extensions.Configuration
    open DSharpPlus
    open DSharpPlus.CommandsNext

    let appConfig =
        ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json", true, true)
            .Build()

    [<EntryPoint>]
    let main argv =
        let token = appConfig.["Token"]
        let config = DiscordConfiguration()
        config.Token <- token
        config.TokenType <- TokenType.Bot
        config.Intents <- DiscordIntents.All 
        //config.MinimumLogLevel <- Microsoft.Extensions.Logging.LogLevel.Debug

        let client = new DiscordClient(config)

        let commandsConfig = CommandsNextConfiguration()
        commandsConfig.StringPrefixes <- ["!"]

        let commands = client.UseCommandsNext commandsConfig
        commands.RegisterCommands<Commands> ()

        client.ConnectAsync () |> Async.AwaitTask |> Async.RunSynchronously
        Task.Delay -1  |> Async.AwaitTask |> Async.RunSynchronously

        0