open System
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open ModelContextProtocol.Server
open System.ComponentModel

type IMyService =

    abstract member Test: unit -> unit

    abstract member GetMessage: unit -> string

type MyService() =

    interface IMyService with

        member this.Test() = ()
        member this.GetMessage() = "Hello from MyService"

module Tools =

    [<AbstractClass; Sealed>]
    [<McpServerToolType>]
    type EchoTools private () =

        [<McpServerTool; Description("Echoes the message back to the client.")>]
        static member Echo(test: IMyService, [<Description("The word to be echoed back")>] message: string) =
            $"{test.GetMessage()} Echo from McpTools: {message}"

        [<McpServerTool; Description("Echoes in reverse the message sent by the client.")>]
        static member ReverseEcho(test: IMyService, message: string) =
            message |> Seq.rev |> Seq.toArray |> String

let build _ =

    let builder = Host.CreateEmptyApplicationBuilder(null)

    builder.Logging.AddConsole(fun consoleLogOutput -> consoleLogOutput.LogToStandardErrorThreshold <- LogLevel.Trace)
    |> ignore


    builder.Services
        .AddScoped<IMyService>(fun _ -> MyService())
        .AddMcpServer()
        .WithStdioServerTransport()
        //.WithTools([])
        //.WithTools([Tools.EchoTools])
        .WithToolsFromAssembly()
    |> ignore

    builder

let run (builder: HostApplicationBuilder) = builder.Build().Run()

let runAsync (builder: HostApplicationBuilder) = builder.Build().RunAsync()

build () |> runAsync |> ignore

while true do
    Async.Sleep(1000) |> Async.RunSynchronously



// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
