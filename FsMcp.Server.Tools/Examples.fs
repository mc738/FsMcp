namespace FsMcp.Server.Tools

open System
open System.ComponentModel
open ModelContextProtocol.Server

module Examples =

    // Static classes in FSharp - https://stackoverflow.com/questions/13101995/defining-static-classes-in-f

    [<AbstractClass; Sealed>]
    type EchoTools private () =

        [<McpServerTool; Description("Echoes the message back to the client.")>]
        static member Echo(message: string) = $"Echo from McpTools: {message}"

        [<McpServerTool; Description("Echoes in reverse the message sent by the client.")>]
        static member ReverseEcho(message: string) =
            message |> Seq.rev |> Seq.toArray |> String
