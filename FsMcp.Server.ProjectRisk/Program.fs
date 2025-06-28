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
        [ """
            CREATE TABLE regulatory_compliance_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO regulatory_compliance_levels (name)
            SELECT DISTINCT regulatory_compliance_level
            FROM _raw;
            """ ]

    let createTechnologyFamiliarityLevelsTable =
        [ """
            CREATE TABLE technology_familiarity_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO technology_familiarity_levels (name)
            SELECT DISTINCT technology_familiarity
            FROM _raw;
            """ ]

    let createStakeholderEngagementLevelsTable =
        [ """
            CREATE TABLE stakeholder_engagement_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO stakeholder_engagement_levels (name)
            SELECT DISTINCT stakeholder_engagement_level
            FROM _raw;
            """ ]

    let createExecutiveSponsorshipLevelsTable =
        [ """
            CREATE TABLE executive_sponsorship_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO executive_sponsorship_levels (name)
            SELECT DISTINCT executive_sponsorship
            FROM _raw;
            """ ]

    let createPriorityLevelsTable =
        [ """
            CREATE TABLE priority_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO priority_levels (name)
            SELECT DISTINCT priority_leve
            FROM _raw;
            """ ]

    let createProjectManagerExperienceLevelsTable =
        [ """
            CREATE TABLE project_manager_experience_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO project_manager_experience_levels (name)
            SELECT DISTINCT project_manager_experience
            FROM _raw;
            """ ]

    let createOrgProcessMaturityLevelsTable =
        [ """
            CREATE TABLE org_process_maturity_levels (name TEXT NOT NULL);
            """
          """
            INSERT INTO org_process_maturity_levels (name)
            SELECT DISTINCT org_process_maturity
            FROM _raw;
            """ ]

    // data_security_requirements

    let createDataSecurityRequirementsTable =
        [ """
            CREATE TABLE data_security_requirements (name TEXT NOT NULL);
            """
          """
            INSERT INTO data_security_requirements (name)
            SELECT DISTINCT data_security_requirements
            FROM _raw;
            """ ]

    // key_stakeholder_availability

    let createKeyStakeholderAvailabilitiesTable =
        [ """
            CREATE TABLE key_stakeholder_availabilities (name TEXT NOT NULL);
            """
          """
            INSERT INTO key_stakeholder_availabilities (name)
            SELECT DISTINCT key_stakeholder_availability
            FROM _raw;
            """ ]
        
        

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

        List.concat
            [ createProjectTypesTableSql
              createProjectMethodologiesTable
              createTeamExperienceLevelsTable
              createProjectPhasesTable
              createRequirementStabilityTypesTable
              createRegulatoryComplianceLevelsTable
              createTechnologyFamiliarityLevelsTable
              createStakeholderEngagementLevelsTable
              createExecutiveSponsorshipLevelsTable
              createPriorityLevelsTable
              createProjectManagerExperienceLevelsTable
              createOrgProcessMaturityLevelsTable
              createDataSecurityRequirementsTable
              createKeyStakeholderSvailabilitiesTable ]
        |> List.iter (fun sql -> ctx.ExecuteSqlNonQuery(sql) |> ignore)

// project_manager_experience

Test.run ()

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"
