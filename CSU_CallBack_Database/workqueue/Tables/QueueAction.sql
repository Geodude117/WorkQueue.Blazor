CREATE TABLE [workqueue].[QueueAction] (
    [QueueActionID] INT        NOT NULL,
    [QueueResultID] INT        NOT NULL,
    [ActionID]      INT        NOT NULL,
    [ActionCode]    NVARCHAR(100) NOT NULL,
    [DefaultDays] INT NULL , 
    CONSTRAINT [PK_Queue_Action] PRIMARY KEY CLUSTERED ([QueueActionID] ASC),
    CONSTRAINT [FK_Queue_Action_Action_Type] FOREIGN KEY ([ActionID]) REFERENCES [workqueue].[ActionType] ([ActionID]),
    CONSTRAINT [FK_Queue_Action_Queue_Result] FOREIGN KEY ([QueueResultID]) REFERENCES [workqueue].[QueueResult] ([QueueResultID])
);


GO

CREATE INDEX [IX_QueueAction_ActionID] ON [workqueue].[QueueAction] ([ActionID])

GO

CREATE INDEX [IX_QueueAction_QueueResultID] ON [workqueue].[QueueAction] ([QueueResultID])
