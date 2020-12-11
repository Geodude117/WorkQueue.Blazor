CREATE TABLE [workqueue].[RAGRule] (
    [RAGRuleID] INT        NOT NULL,
    [RAGStatus] NVARCHAR(100) NOT NULL,
	[LowValue] DECIMAL(18,2) NOT NULL,
    [MidValue] DECIMAL(18, 2) NOT NULL, 
    [HighValue] DECIMAL(18, 2) NOT NULL, 
    [LowToHigh] BIT NOT NULL, 
    CONSTRAINT [PK_RAGRule] PRIMARY KEY CLUSTERED ([RAGRuleID] ASC)
);

