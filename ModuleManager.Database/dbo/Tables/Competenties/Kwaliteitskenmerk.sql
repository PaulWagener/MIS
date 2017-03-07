CREATE TABLE [Competenties].[Kwaliteitskenmerk]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CompetentieOnderdeelId] INT NOT NULL,
	[Volgnummer] INT NOT NULL,
	[Omschrijving] VARCHAR(255) NOT NULL,

	CONSTRAINT [UQ_Kwaliteitskenmerk_CompetentieOnderdeelId_Volgnummer] UNIQUE ([CompetentieOnderdeelId], Volgnummer),
	CONSTRAINT [FK_Kwaliteitskenmerk_CompetentieOnderdeel] FOREIGN KEY ([CompetentieOnderdeelId]) REFERENCES [Competenties].[CompetentieOnderdeel](Id)
)
