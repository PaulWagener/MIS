CREATE TABLE [dbo].[Voorkennis] (
    [Voorkennis_CursusCode] VARCHAR (50) NOT NULL,
    [Voorkennis_Schooljaar] VARCHAR (8)  NOT NULL,
    [Vervolg_CursusCode]    VARCHAR (50) NOT NULL,
    [Vervolg_Schooljaar]    VARCHAR (8)  NOT NULL,
    CONSTRAINT [PK_Voorkennis] PRIMARY KEY CLUSTERED ([Voorkennis_CursusCode] ASC, [Voorkennis_Schooljaar] ASC, [Vervolg_CursusCode] ASC, [Vervolg_Schooljaar] ASC),
    CONSTRAINT [FK_Voorkennis_Module] FOREIGN KEY ([Voorkennis_CursusCode], [Voorkennis_Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]),
    CONSTRAINT [FK_Voorkennis_Module1] FOREIGN KEY ([Vervolg_CursusCode], [Vervolg_Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Voorkennis_Module1]
    ON [dbo].[Voorkennis]([Vervolg_CursusCode] ASC, [Vervolg_Schooljaar] ASC);

