MERGE INTO Competentie AS Target  
USING (values 
	('BC1', '1516', 'Procesanalyse uitvoeren', 'Stelt de student in staat.. ' ), 
	('BC2',	'1516',	'Testplan opzetten', 'Stelt de student in staat.. ' ),
	('BC3',	'1516',	'Technisch ontwerpen', 'Stelt de student in staat.. ' )
) AS Source (Code, Schooljaar, Naam, Beschrijving)  
ON Target.Code = Source.Code  
	AND Target.Schooljaar = Source.Schooljaar
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Code, Schooljaar, Naam, Beschrijving) VALUES (Code, Schooljaar, Naam, Beschrijving)
WHEN MATCHED THEN
	UPDATE SET
		Naam = Source.Naam,
		Beschrijving = Source.Beschrijving;