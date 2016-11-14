CREATE TABLE [dbo].[StudiePunt] (
    [CursusCode] VARCHAR (50) NOT NULL,
    [Schooljaar] INT		  NOT NULL,
    [ToetsCode]  VARCHAR (50) NOT NULL,
    [Toetsvorm]  VARCHAR (50) NULL,
    [EC]         DECIMAL (18) NOT NULL,
    [Minimum]    VARCHAR (50) NULL,
    CONSTRAINT [PK_StudiePunten] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC, [ToetsCode] ASC),
    CONSTRAINT [FK_StudiePunten_Module] FOREIGN KEY ([CursusCode], [Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]),
    CONSTRAINT [FK_StudiePunten_Toetsvorm] FOREIGN KEY ([Toetsvorm]) REFERENCES [dbo].[Toetsvorm] ([Type])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_StudiePunten_Toetsvorm]
    ON [dbo].[StudiePunt]([Toetsvorm] ASC);

