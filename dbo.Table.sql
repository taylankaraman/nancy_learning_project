CREATE TABLE [dbo].[Table] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (50) NOT NULL,
    [Author] NVARCHAR (50) NOT NULL,
    [Content] NVARCHAR(500) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

