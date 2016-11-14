MERGE INTO Competentie AS Target  
USING (values 
	('BC1', 'Procesanalyse uitvoeren', 'Stelt de student in staat.. ' ), 
	('BC2',	'Testplan opzetten', 'Stelt de student in staat.. ' ),
	('BC3',	'Technisch ontwerpen', 'Stelt de student in staat.. ' )
) AS Source (Code, Naam, Beschrijving)  
ON Target.Code = Source.Code
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Code, Naam, Beschrijving) VALUES (Code, Naam, Beschrijving)
WHEN MATCHED THEN
	UPDATE SET
		Naam = Source.Naam,
		Beschrijving = Source.Beschrijving;