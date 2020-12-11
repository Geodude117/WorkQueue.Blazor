CREATE TABLE [dbo].[DomainGroup] (
    [Id]                    INT             NOT NULL IdENTITY,
    [GroupName]             NVARCHAR (100)  NOT NULL,
    [ClassMapping]          NVARCHAR (200)  NOT NULL,
    [ExternalReferenceId]   NVARCHAR (100)  NOT NULL,
    [IsActive]              BIT             NOT NULL,
    [AccessGroupPublic]     NVARCHAR (100)   NULL,
    [AccessGroupBase]       NVARCHAR (100)   NULL,
    [AccessGroupExtended]   NVARCHAR (100)   NULL,
    [AccessGroupAdmin]      NVARCHAR (100)   NULL,

    CONSTRAINT [PK_DomainGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);
