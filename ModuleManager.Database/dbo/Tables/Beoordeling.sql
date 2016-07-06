CREATE TABLE [dbo].[Beoordeling] (
    [CursusCode]   VARCHAR (50)  NOT NULL,
    [Schooljaar]   VARCHAR (8)   NOT NULL,
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Beschrijving] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Beoordelingen] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC, [Id] ASC),
    CONSTRAINT [FK_Beoordelingen_Module] FOREIGN KEY ([CursusCode], [Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar])
);

