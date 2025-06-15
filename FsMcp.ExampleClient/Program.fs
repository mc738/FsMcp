open System
open Microsoft.Extensions.AI
open Microsoft.Extensions.Logging
open ModelContextProtocol.Client
open ModelContextProtocol.Server
open OllamaSharp

let run () =
    task {
        
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

    let! mcpClient = McpClientFactory.CreateAsync(s, clientOptions)

    let ollamaChatClient = new OllamaApiClient(Uri("http://localhost:11434/"), "llama3.2") :> IChatClient




    //let chatClient = Chat(ollamaChatClient)

    let! mcpTools = mcpClient.ListToolsAsync()
                    
    let t = mcpTools |> Seq.map (fun tool -> tool :> AITool) 

    let chatClient =
        ChatClientBuilder(ollamaChatClient).UseLogging(loggerFactory).UseFunctionInvocation().Build()
        
        
    let mutable exit = false
    
    printfn "Type your message below (type 'exit' to quit):"
    
    
    while exit = false do
        let chatOptions = ChatOptions()
        
        printf ">>> "
        
        let userInput = Console.ReadLine()
        
        if userInput = "exit" then
            Console.WriteLine("Exiting chat...")
            exit <- true
        else
                    
            let messages =
                [
                    ChatMessage(ChatRole.System, "You are a helpful assistant")
                    ChatMessage(ChatRole.User, userInput)
                ]
            
            chatOptions.Tools <- t |> ResizeArray
            
            let! response = chatClient.GetResponseAsync(messages, chatOptions)
            
            let message = response.Messages |> Seq.tryFindBack (fun m -> m.Role = ChatRole.Assistant)
            
            match message with
            | None -> printfn "AI: (no assistant message received)"
            | Some value ->
                //value.Contents |> Seq.map (fun value -> value)
                
                printfn $"AI: {value.Text}"
                
                printfn "AI: (no assistant message received)"
                
           
            ()
   
    return ()    
    }


run () |> Async.AwaitTask |> Async.RunSynchronously

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
