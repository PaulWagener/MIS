CREATE TABLE [dbo].[Module] (
    [CursusCode]        VARCHAR (50)  NOT NULL,
    [Schooljaar]        INT			  NOT NULL,
    [Beschrijving]      NVARCHAR(MAX) NULL,
    [Naam]              NVARCHAR(50)  NOT NULL,
    [VerantwoordelijkeDocentId] INT  NOT NULL,
    [Status]            VARCHAR(50)  NOT NULL,
	[Gecontroleerd]		BIT NOT NULL,
    [OnderdeelCode]     VARCHAR(50)  NOT NULL,
    [Icon]              CHAR (20)     NOT NULL,
    [Blok]				VARCHAR(8) NOT NULL,
	[ReadOnly]			BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC),
    CONSTRAINT [FK_Module_Icons] FOREIGN KEY ([Icon]) REFERENCES [dbo].[Icon] ([Naam]),
    CONSTRAINT [FK_Module_Onderdeel] FOREIGN KEY ([OnderdeelCode]) REFERENCES [dbo].[Onderdeel] ([Code]),
    CONSTRAINT [FK_Module_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[Status] ([Status1]),
	CONSTRAINT [FK_Module_Schooljaar] FOREIGN KEY ([Schooljaar]) REFERENCES [dbo].[Schooljaar] ([JaarId]),
	CONSTRAINT [FK_Module_Docent] FOREIGN KEY (VerantwoordelijkeDocentId) REFERENCES [dbo].[Docent] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Module_Icons]
    ON [dbo].[Module]([Icon] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Module_Onderdeel]
    ON [dbo].[Module]([OnderdeelCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Module_Status]
    ON [dbo].[Module]([Status] ASC);


