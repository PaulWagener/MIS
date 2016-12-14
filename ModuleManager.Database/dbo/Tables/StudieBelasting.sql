CREATE TABLE [dbo].[StudieBelasting] (
    [CursusCode]  VARCHAR (50) NOT NULL,
    [Schooljaar]  INT		   NOT NULL,
    [Activiteit]  VARCHAR (50) NOT NULL,
    [ContactUren] INT          NULL,
    [Duur]        VARCHAR (50) NOT NULL,
    [Frequentie]  VARCHAR (50) NOT NULL,
    [SBU]         INT          NOT NULL,
    CONSTRAINT [PK_StudieBelasting] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC, [Activiteit] ASC),
    CONSTRAINT [FK_StudieBelasting_Module] FOREIGN KEY ([CursusCode], [Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]) ON DELETE CASCADE
);

