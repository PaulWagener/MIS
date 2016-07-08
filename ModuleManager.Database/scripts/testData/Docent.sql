SET IDENTITY_INSERT Docent ON;

MERGE INTO Docent AS Target  
USING (values 
	(1, 'Bart Mutsaers'), 
	(2, 'Ger Saris'), 
	(3, 'Stijn Smulders'),
	(4, 'Martijn Schuurmans')
) AS Source (Id, Naam)  
ON Target.Id = Source.Id  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, Naam) VALUES (Id, Naam);

SET IDENTITY_INSERT Docent OFF;