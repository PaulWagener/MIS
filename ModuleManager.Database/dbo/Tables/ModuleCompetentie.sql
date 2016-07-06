CREATE TABLE [dbo].[ModuleCompetentie] (
    [CursusCode]            VARCHAR (50) NOT NULL,
    [Schooljaar]            VARCHAR (8)  NOT NULL,
    [CompetentieCode]       VARCHAR (50) NOT NULL,
    [CompetentieSchooljaar] VARCHAR (8)  NOT NULL,
    [Niveau]                VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ModuleCompetentie] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC, [CompetentieCode] ASC, [CompetentieSchooljaar] ASC),
    CONSTRAINT [FK_ModuleCompetentie_Competentie] FOREIGN KEY ([CompetentieCode], [CompetentieSchooljaar]) REFERENCES [dbo].[Competentie] ([Code], [Schooljaar]),
    CONSTRAINT [FK_ModuleCompetentie_Module] FOREIGN KEY ([CursusCode], [Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]),
    CONSTRAINT [FK_ModuleCompetentie_Niveau] FOREIGN KEY ([Niveau]) REFERENCES [dbo].[Niveau] ([Niveau1])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ModuleCompetentie_Competentie]
    ON [dbo].[ModuleCompetentie]([CompetentieCode] ASC, [CompetentieSchooljaar] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ModuleCompetentie_Niveau]
    ON [dbo].[ModuleCompetentie]([Niveau] ASC);

