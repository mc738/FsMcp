
open Freql.Csv

module Test =
    
    let run _ =
        let (rows, errors) =
            CsvParser.parseFileV2<FsMcp.Server.ProjectRisk.Data.Preparation.Common.RawValue> true "C:\\Users\\mclif\\Downloads\\archive (2)\\project_risk_raw_dataset.csv"
            |> CsvParser.splitResults
        
        
        ()
        

Test.run ()

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"