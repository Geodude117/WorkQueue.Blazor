CREATE TABLE [workqueue].[QueueResult] (
    [QueueResultID] INT        NOT NULL,
    [QueueResult]   NVARCHAR(100) NOT NULL,
    [QueueID]       INT        NOT NULL,
    CONSTRAINT [PK_Queue_Result] PRIMARY KEY CLUSTERED ([QueueResultID] ASC),
    CONSTRAINT [FK_Queue_QueuesResult] FOREIGN KEY ([QueueID]) REFERENCES [workqueue].[Queue] ([QueueID])
);


GO

CREATE INDEX [IX_QueueResult_QueueID] ON [workqueue].[QueueResult] ([QueueID])
