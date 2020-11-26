CREATE TABLE [dbo].[DomainType] 
(
    [Id]           INT             NOT NULL IdENTITY,
    [TypeName]     NVARCHAR (100)  NOT NULL,
    [ClassObject]  NVARCHAR (100)  NOT NULL
    CONSTRAINT [PK_DomainType] PRIMARY KEY CLUSTERED ([Id] ASC),
);
