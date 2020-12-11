CREATE TABLE [workqueue].[Queue] (
    [QueueID]      INT NOT NULL,
    [QueueGroupID] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
    [IsActive]     BIT NOT NULL,
    [RAGRuleID] INT NULL, 
    [DefaultStatus] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_Queue] PRIMARY KEY CLUSTERED ([QueueID] ASC),
    CONSTRAINT [FK_Queue_QueueGroup] FOREIGN KEY ([QueueGroupID]) REFERENCES [workqueue].[QueueGroup] ([QueueGroupID]), 
    CONSTRAINT [FK_Queue_RAGRule] FOREIGN KEY ([RAGRuleID]) REFERENCES [workqueue].[RAGRule]([RAGRuleID])
);


GO

CREATE INDEX [IX_Queue_QueueGroupId] ON [workqueue].[Queue] ([QueueGroupId])

GO

CREATE INDEX [IX_Queue_RAGRuleID] ON [workqueue].[Queue] ([RAGRuleID])
