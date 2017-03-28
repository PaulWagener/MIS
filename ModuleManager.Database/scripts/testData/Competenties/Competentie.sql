SET IDENTITY_INSERT Competenties.Competentie ON

MERGE INTO Competenties.Competentie AS Target  
USING (VALUES 
	(1, 1, 'Beheren'),
	(2, 2, 'Analyseren'),
	(3, 3, 'Ontwerpen'),
	(4, 4, 'Realiseren'),
	(5, 5, 'Adviseren'),
	(6, 6, 'Onderzoeken'),
	(7, 7, 'Communiceren'),
	(8, 8, 'Samenwerken'),
	(9, 9, 'Professionaliseren')
) AS Source (Id, Volgnummer, Naam)  
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, Volgnummer, Naam) VALUES (Id, Volgnummer, Naam)
WHEN MATCHED THEN
	UPDATE SET
		Volgnummer = Source.Volgnummer,
		Naam = Source.Naam;

SET IDENTITY_INSERT Competenties.Competentie OFF