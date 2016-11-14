CREATE TABLE [dbo].[ModuleLeerlijn] (
    [Leerlijn_Naam]       VARCHAR (50) NOT NULL,
    [Module_CursusCode]   VARCHAR (50) NOT NULL,
    [Module_Schooljaar]   INT		   NOT NULL,
    CONSTRAINT [PK_ModuleLeerlijn] PRIMARY KEY CLUSTERED ([Leerlijn_Naam] ASC, [Module_CursusCode] ASC, [Module_Schooljaar] ASC),
    CONSTRAINT [FK_ModuleLeerlijn_Leerlijn] FOREIGN KEY ([Leerlijn_Naam]) REFERENCES [dbo].[Leerlijn] ([Naam]),
    CONSTRAINT [FK_ModuleLeerlijn_Module] FOREIGN KEY ([Module_CursusCode], [Module_Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ModuleLeerlijn_Module]
    ON [dbo].[ModuleLeerlijn]([Module_CursusCode] ASC, [Module_Schooljaar] ASC);

