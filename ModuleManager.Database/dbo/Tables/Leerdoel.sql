CREATE TABLE [dbo].[Leerdoel] (
    [CursusCode]   VARCHAR (50)  NOT NULL,
    [Schooljaar]   INT			 NOT NULL,
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Beschrijving] VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Leerdoel] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Leerdoel_Module] FOREIGN KEY ([CursusCode], [Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar])
);

