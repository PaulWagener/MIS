CREATE TABLE [dbo].[ModuleWerkvorm] (
    [CursusCode]   VARCHAR (50)  NOT NULL,
    [Schooljaar]   INT			 NOT NULL,
    [WerkvormType] VARCHAR (5)   NOT NULL,
    [Organisatie]  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ModuleWerkvorm] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC, [WerkvormType] ASC),
    CONSTRAINT [FK_ModuleWerkvorm_Module] FOREIGN KEY ([CursusCode], [Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]),
    CONSTRAINT [FK_ModuleWerkvorm_Werkvorm] FOREIGN KEY ([WerkvormType]) REFERENCES [dbo].[Werkvorm] ([Type])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ModuleWerkvorm_Werkvorm]
    ON [dbo].[ModuleWerkvorm]([WerkvormType] ASC);

