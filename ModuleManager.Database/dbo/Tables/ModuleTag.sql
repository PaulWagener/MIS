CREATE TABLE [dbo].[ModuleTag] (
    [Module_CursusCode] VARCHAR (50) NOT NULL,
    [Module_Schooljaar] VARCHAR (8)  NOT NULL,
    [Tag_Naam]          VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ModuleTag] PRIMARY KEY CLUSTERED ([Module_CursusCode] ASC, [Module_Schooljaar] ASC, [Tag_Naam] ASC),
    CONSTRAINT [FK_ModuleTag_Module] FOREIGN KEY ([Module_CursusCode], [Module_Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]),
    CONSTRAINT [FK_ModuleTag_Tag] FOREIGN KEY ([Tag_Naam]) REFERENCES [dbo].[Tag] ([Naam])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ModuleTag_Tag]
    ON [dbo].[ModuleTag]([Tag_Naam] ASC);

