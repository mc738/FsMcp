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

type Product =
    {
        Name: string
        Value: decimal
        Tags: string list
    }
    
let products =
    [
        { Name = "Foo product 1"
          Value = 100m
          Tags = [ "foo" ] }
        
        { Name = "Foo product 2"
          Value = 40m
          Tags = [ "foo" ] }
        
        { Name = "Foo product 3"
          Value = 55m
          Tags = [ "foo" ] }
        
        { Name = "Foo product 4"
          Value = 59.99m
          Tags = [ "foo" ] }
        
        { Name = "Foo product 5"
          Value = 50m
          Tags = [ "foo" ] }
        
        
        { Name = "Bar product 1"
          Value = 50m
          Tags = [ "bar" ] }
    ]

module Tools =

    
    type Filters =
        {
            RiskFactors: string list
            TeamLocations: string list
            FundingTypes: string list
        }
    
    [<AbstractClass; Sealed>]
    [<McpServerToolType>]
    type EchoTools private () =

        [<McpServerTool; Description("Echoes the message back to the client.")>]
        static member Echo(test: IMyService, [<Description("The word to be echoed back")>] message: string) =
            $"{test.GetMessage()} Echo from McpTools: {message}"

        [<McpServerTool; Description("Echoes in reverse the message sent by the client.")>]
        static member ReverseEcho(test: IMyService, message: string) =
            message |> Seq.rev |> Seq.toArray |> String
            
            
        [<McpServerTool; Description("Get a list of products based on a tag")>]
        static member GetProducts(test: IMyService, [<Description("The tag to search for")>] tag: string) =
            products |> Seq.filter (fun product -> product.Tags |> List.contains tag)

        [<McpServerTool; Description("Get a list of filters")>]            
        static member GetFilters(test: IMyService, [<Description("The filters to search for")>] filters: Filters) =
            //logger.LogInformation($"{filters}")
            filters

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
