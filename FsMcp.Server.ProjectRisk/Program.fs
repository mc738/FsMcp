open Freql.Csv
open Freql.Sqlite
open FsMcp.Server.ProjectRisk.Data.Preparation

module Test =


    let createProjectTypesTableSql =
        [ """
            CREATE TABLE project_types (name TEXT NOT NULL);
            """
          """
            INSERT INTO project_types (name)
            SELECT DISTINCT project_type
            FROM _raw;
            """ ]

    let createProjectMethodologiesTable =
        [ """
            CREATE TABLE project_methodologies (name TEXT NOT NULL);
            """
          """
            INSERT INTO project_methodologies (name)
            SELECT DISTINCT methodology_used
            FROM _raw;
            """ ]

    let createTeamExperienceLevelsTable =
        [ """
            CREATE TABLE team_experience_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO team_experience_levels (name)
            SELECT DISTINCT team_experience_level
            FROM _raw;
            """ ]

    let createProjectPhasesTable =
        [ """
            CREATE TABLE project_phases (name TEXT NOT NULL);
            """
          """
            INSERT INTO project_phases (name)
            SELECT DISTINCT project_phase
            FROM _raw;
            """ ]

    let createRequirementStabilityTypesTable =
        [ """
            CREATE TABLE requirement_stability_types (name TEXT NOT NULL);
            """
          """
            INSERT INTO requirement_stability_types (name)
            SELECT DISTINCT requirement_stability
            FROM _raw;
            """ ]

    let createRegulatoryComplianceLevelsTable =
        [
            """
            CREATE TABLE regulatory_compliance_levels (name TEXT NOT NULL);
            """
            """
            INSERT INTO regulatory_compliance_levels (name)
            SELECT DISTINCT regulatory_compliance_level
            FROM _raw;
            """
        ]

    let createTechnologyFamiliarityLevelsTable =
        """
        CREATE TABLE technology_familiarity_levels
        AS 
        SELECT DISTINCT technology_familiarity
        FROM _raw;
        """

    let createStakeholderEngagementLevelsTable =
        """
        CREATE TABLE stakeholder_engagement_levels
        AS 
        SELECT DISTINCT stakeholder_engagement_level
        FROM _raw;
        """

    let createExecutiveSponsorshipLevelsTable =
        """
        CREATE TABLE executive_sponsorship_levels
        AS 
        SELECT DISTINCT executive_sponsorship
        FROM _raw;
        """

    let run _ =
        let (rows, errors) =
            CsvParser.parseFileV2<RawValue>
                true
                "C:\\Users\\mclif\\Downloads\\archive (2)\\project_risk_raw_dataset.csv"
            |> CsvParser.splitResults

        use ctx =
            SqliteContext.Create("C:\\Users\\mclif\\Projects\\data\\FsMcp\\data\\project_risk.db")

        ctx.CreateTable<RawValue>("_raw") |> ignore

        rows |> List.iter (fun value -> ctx.Insert<RawValue>("_raw", value))

        ctx.ExecuteSqlNonQuery(createProjectTypesTableSql) |> ignore
        ctx.ExecuteSqlNonQuery(createProjectMethodologiesTable) |> ignore
        ctx.ExecuteSqlNonQuery(createTeamExpereinceLevelsTable) |> ignore
        ctx.ExecuteSqlNonQuery(createProjectPhasesTable) |> ignore
        ctx.ExecuteSqlNonQuery(createRequirementStabilityTypesTable) |> ignore
        ctx.ExecuteSqlNonQuery(createRegulatoryComplianceLevelsTable) |> ignore
        ctx.ExecuteSqlNonQuery(createTechnologyFamiliarityLevelsTable) |> ignore
        ctx.ExecuteSqlNonQuery(createStakeholderEngagementLevelsTable) |> ignore
        ctx.ExecuteSqlNonQuery(createExecutiveSponsorshipLevelsTable) |> ignore



Test.run ()

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
