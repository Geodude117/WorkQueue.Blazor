CREATE TABLE [dbo].[DomainInformation] 
(
    [Id]        INT             NOT NULL IdENTITY,
    [Title]     NVARCHAR (100)  NOT NULL,
    [ObjectMapping] NVARCHAR (100)  NOT NULL,
    [Order]     INT             NOT NULL,
    [TypeId]    INT             NOT NULL,
    [GroupId]   INT             NOT NULL,
    [Arguments]   NVARCHAR (100)   NULL
    CONSTRAINT [PK_DomainInformation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DomainGroup] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[DomainGroup] ([Id]),
    CONSTRAINT [FK_DomainTypeId] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[DomainType] ([Id])

);
