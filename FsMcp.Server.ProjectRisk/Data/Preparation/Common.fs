namespace FsMcp.Server.ProjectRisk.Data.Preparation

[<AutoOpen>]
module Common =

    type RawValue =
        { ProjectID: string
          ProjectType: string
          TeamSize: int
          ProjectBudgetUSD: decimal
          EstimatedTimelineMonths: int
          ComplexityScore: float
          StakeholderCount: int
          MethodologyUsed: string
          TeamExperienceLevel: string
          PastSimilarProjects: int
          ExternalDependenciesCount: int
          ChangeRequestFrequency: float
          ProjectPhase: string
          RequirementStability: string
          TeamTurnoverRate: float
          VendorReliabilityScore: float
          HistoricalRiskIncidents: int
          CommunicationFrequency: float
          RegulatoryComplianceLevel: string
          TechnologyFamiliarity: string
          GeographicalDistribution: int
          StakeholderEngagementLevel: string
          SchedulePressure: float
          BudgetUtilizationRate: float
          ExecutiveSponsorship: string
          FundingSource: string
          MarketVolatility: float
          IntegrationComplexity: float
          ResourceAvailability: float
          PriorityLevel: string
          OrganizationalChangeFrequency: float
          CrossFunctionalDependencies: int
          PreviousDeliverySuccessRate: float
          TechnicalDebtLevel: float
          ProjectManagerExperience: string
          OrgProcessMaturity: string
          DataSecurityRequirements: string
          KeyStakeholderAvailability: string
          TechEnvironmentStability: string
          ContractType: string
          ResourceContentionLevel: string
          IndustryVolatility: string
          ClientExperienceLevel: string
          ChangeControlMaturity: string
          RiskManagementMaturity: string
          TeamColocation: string
          DocumentationQuality: string
          ProjectStartMonth: int
          CurrentPhaseDurationMonths: int
          SeasonalRiskFactor: float
          RiskLevel: string }

