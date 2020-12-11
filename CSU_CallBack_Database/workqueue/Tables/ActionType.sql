CREATE TABLE [workqueue].[ActionType] (
    [ActionID]          INT        NOT NULL,
    [ActionDescription] NVARCHAR(100) NOT NULL,
    CONSTRAINT [PK_ActionType] PRIMARY KEY CLUSTERED ([ActionID] ASC)
);

