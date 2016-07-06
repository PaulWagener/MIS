CREATE TABLE [dbo].[Fase] (
    [Naam]          VARCHAR (50)  NOT NULL,
    [Beschrijving]  VARCHAR (500) NULL,
    [FaseType]      VARCHAR (50)  NOT NULL,
    [OpleidingNaam] VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Fase_1] PRIMARY KEY CLUSTERED ([Naam] ASC),
    CONSTRAINT [FK_Fase_FaseType] FOREIGN KEY ([FaseType]) REFERENCES [dbo].[FaseType] ([Type])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Fase_FaseType]
    ON [dbo].[Fase]([FaseType] ASC);

