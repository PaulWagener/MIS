CREATE TABLE [dbo].[Module] (
    [CursusCode]        VARCHAR (50)  NOT NULL,
    [Schooljaar]        VARCHAR (8)   NOT NULL,
    [Beschrijving]      NVARCHAR(MAX) NULL,
    [Naam]              NVARCHAR(50)  NOT NULL,
    [Verantwoordelijke] NVARCHAR(50)  NOT NULL,
    [Status]            VARCHAR(50)  NOT NULL,
    [OnderdeelCode]     VARCHAR(50)  NOT NULL,
    [Icon]              CHAR (20)     NOT NULL,
    [Blok] VARCHAR(8) NOT NULL, 
    CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED ([CursusCode] ASC, [Schooljaar] ASC),
    CONSTRAINT [FK_Module_Icons] FOREIGN KEY ([Icon]) REFERENCES [dbo].[Icon] ([Naam]),
    CONSTRAINT [FK_Module_Onderdeel] FOREIGN KEY ([OnderdeelCode]) REFERENCES [dbo].[Onderdeel] ([Code]),
    CONSTRAINT [FK_Module_Status] FOREIGN KEY ([Status]) REFERENCES [dbo].[Status] ([Status1])
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

