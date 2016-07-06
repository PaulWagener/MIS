CREATE TABLE [dbo].[Weekplanning] (
    [CursusCode] VARCHAR (50)  NOT NULL,
    [Schooljaar] VARCHAR (8)   NOT NULL,
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Week]       VARCHAR (10)  NOT NULL,
    [Onderwerp]  VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Weekplanning] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC, [Id] ASC),
    CONSTRAINT [FK_Weekplanning_Module] FOREIGN KEY ([CursusCode], [Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar])
);

