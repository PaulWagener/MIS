CREATE TABLE [dbo].[ModuleDocent] (
    [Docenten_Id]       INT          NOT NULL,
    [Module_CursusCode] VARCHAR (50) NOT NULL,
    [Module_Schooljaar] INT			 NOT NULL,
    CONSTRAINT [PK_ModuleDocent] PRIMARY KEY CLUSTERED ([Docenten_Id] ASC, [Module_CursusCode] ASC, [Module_Schooljaar] ASC),
    CONSTRAINT [FK_ModuleDocent_Docent] FOREIGN KEY ([Docenten_Id]) REFERENCES [dbo].[Docent] ([Id]),
    CONSTRAINT [FK_ModuleDocent_Module] FOREIGN KEY ([Module_CursusCode], [Module_Schooljaar]) REFERENCES [dbo].[Module] ([CursusCode], [Schooljaar]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ModuleDocent_Module]
    ON [dbo].[ModuleDocent]([Module_CursusCode] ASC, [Module_Schooljaar] ASC);

