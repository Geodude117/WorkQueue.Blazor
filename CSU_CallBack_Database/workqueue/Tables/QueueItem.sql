CREATE TABLE [workqueue].[QueueItem] (
    [QueueItemID]       INT            IDENTITY (1, 1) NOT NULL,
    [WescotRef]         INT            NOT NULL,
    [CustomerName]      NVARCHAR (100) NOT NULL,
    [CompletedDate]     DATETIME       NULL,
    [CompletedBy]       NVARCHAR (100) NULL,
    [DueDate]           DATETIME       NOT NULL,
    [CreatedDate]       DATETIME       NOT NULL,
    [CreatedBy]         NVARCHAR (100) NOT NULL,
    [Summary]           NVARCHAR (500) NOT NULL,
    [ParentQueueItemID] INT            NULL,
    [QueueID]           INT            NOT NULL,
    [LockTime]          DATETIME       NULL,
    [LockedBy]          NVARCHAR (100) NULL,
    CONSTRAINT [PK_Queue_Item] PRIMARY KEY CLUSTERED ([QueueItemID] ASC) WITH (FILLFACTOR = 85),
    CONSTRAINT [FK_Queue_Item_Queues] FOREIGN KEY ([QueueID]) REFERENCES [workqueue].[Queue] ([QueueID]),
    CONSTRAINT [FK_QueueItem_QueueItem] FOREIGN KEY ([ParentQueueItemID]) REFERENCES [workqueue].[QueueItem] ([QueueItemID])
);




GO

CREATE INDEX [IX_QueueItem_QueueId] ON [workqueue].[QueueItem] ([QueueId])
