CREATE TABLE [workqueue].[QueueGroup] (
    [QueueGroupID] INT        NOT NULL,
    [Name]         NVARCHAR(100) NOT NULL,
    [IsActive]     BIT        NOT NULL,
    [AccessGroupPublic] NVARCHAR(50) NULL, 
    [AccessGroupBase] NVARCHAR(50) NULL, 
    [AccessGroupExtended] NVARCHAR(50) NULL, 
    [AccessGroupAdmin] NVARCHAR(50) NULL, 
    [DefaultQueueID] INT NULL DEFAULT 1, 
    CONSTRAINT [PK_QueueGroup] PRIMARY KEY CLUSTERED ([QueueGroupID] ASC)
);

