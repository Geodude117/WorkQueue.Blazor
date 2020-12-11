CREATE TABLE [callback].[CallbackDetail] (
    [ID]             INT        NOT NULL IDENTITY,
    [WescotRef]              INT  NOT NULL,
    [DateForCallback]        DATETIME   NOT NULL,
    [NameOfCaller]           NVARCHAR (100) NOT NULL,
    [Relationship] NVARCHAR (100) NULL,
    [ContactNumber]          nvarchar (50) NOT NULL,
    [TimeToAvoid]            NVARCHAR(200)   NULL,
    [ReasonForCallback]      nvarchar (4000) NOT NULL,
    [ReasonForTransfer]      nvarchar (4000) NOT NULL,
    [HealthIssue]            nvarchar (4000) NULL,
    [QueueItemID]            INT        NULL,
    CONSTRAINT [PK_CallbackDetail] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE INDEX [IX_CallbackDetail_QueueItemID] ON [callback].[CallbackDetail] ([QueueItemId])

