open System
open Microsoft.Extensions.AI
open Microsoft.Extensions.Logging
open ModelContextProtocol.Client
open ModelContextProtocol.Server
open OllamaSharp

let clientOptions = McpClientOptions()

clientOptions.ClientInfo <- ModelContextProtocol.Protocol.Implementation(Name = "demo-client", Version = "1.0.0")

let serverConfig =
    StdioClientTransportOptions(
        Command =
            "C:\\Users\\mclif\\Projects\\dotnet\\FsMcp\\FsMcp.ExampleServer\\bin\\Debug\\net9.0\\FsMcp.ExampleServer.exe"
    )

serverConfig.Name <- "Demo server"

use loggerFactory =
    LoggerFactory.Create(fun builder -> builder.AddConsole().SetMinimumLevel(LogLevel.Information) |> ignore)

let s = StdioClientTransport(serverConfig, loggerFactory)

let mcpClient = McpClientFactory.CreateAsync(s, clientOptions)

let ollamaChatClient = new OllamaApiClient(Uri(""), "llama3.2") :> IChatClient




//let chatClient = Chat(ollamaChatClient)

let chatClient =
    ChatClientBuilder(ollamaChatClient).UseLogging(loggerFactory).UseFunctionInvocation().Build()
    
let response = chatClient.GetResponseAsync(Seq.empty, ChatOptions(Tools = ResizeArray()))

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
