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
        //Config setup for the client
        let token = appConfig.["Token"]
        let config = DiscordConfiguration()
        config.Token <- token
        config.TokenType <- TokenType.Bot
        config.Intents <- DiscordIntents.All 
        //Uncomment this to enable debug logging
        //config.MinimumLogLevel <- Microsoft.Extensions.Logging.LogLevel.Debug

        let client = new DiscordClient(config)

        let commandsConfig = CommandsNextConfiguration()
        //Change command prefix(es) here
        commandsConfig.StringPrefixes <- ["!"]

        let commands = client.UseCommandsNext commandsConfig
        //You could register multiple types
        commands.RegisterCommands<Commands> ()

        client.ConnectAsync () |> Async.AwaitTask |> Async.RunSynchronously
        Task.Delay -1  |> Async.AwaitTask |> Async.RunSynchronously

        0