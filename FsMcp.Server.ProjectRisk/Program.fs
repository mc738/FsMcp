
open Freql.Csv
open Freql.Sqlite
open FsMcp.Server.ProjectRisk.Data.Preparation

module Test =
    
    let run _ =
        let (rows, errors) =
            CsvParser.parseFileV2<FsMcp.Server.ProjectRisk.Data.Preparation.Common.RawValue> true "C:\\Users\\mclif\\Downloads\\archive (2)\\project_risk_raw_dataset.csv"
            |> CsvParser.splitResults
        
        use ctx = SqliteContext.Create("C:\\Users\\mclif\\Projects\\data\\FsMcp\\project_risk.db")
        
        ctx.CreateTable<FsMcp.Server.ProjectRisk.Data.Preparation.Common.RawValue>("_raw") |> ignore
        
        rows
        |> List.iter (fun value -> ctx.Insert<RawValue>("_raw", value))
        
        
        
        
        ()
        

Test.run ()

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"