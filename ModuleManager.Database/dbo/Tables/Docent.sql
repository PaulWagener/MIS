CREATE TABLE [dbo].[Docent] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Naam] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Docenten] PRIMARY KEY CLUSTERED ([Id] ASC)
);

