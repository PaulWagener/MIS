CREATE TABLE [Competenties].[CompetentieOnderdeel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CompetentieId] INT NOT NULL,
	[Volgnummer] INT NOT NULL,
	[Naam] VARCHAR(50) NOT NULL,

	CONSTRAINT [UQ_CompetentieOnderdeel_CompetentieId_Volgnummer] UNIQUE (CompetentieId, Volgnummer),
	CONSTRAINT [FK_CompetentieOnderdeel_Competentie] FOREIGN KEY (CompetentieId) REFERENCES [Competenties].[Competentie](Id)
)
