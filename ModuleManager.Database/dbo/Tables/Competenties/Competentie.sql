CREATE TABLE [Competenties].[Competentie]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Volgnummer] INT NOT NULL,
	[Naam] VARCHAR(50) NOT NULL,

	CONSTRAINT UQ_Competentie_Volgnummer UNIQUE (Volgnummer)
)
