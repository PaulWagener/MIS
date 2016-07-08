MERGE INTO Icon AS Target  
USING (values 
	('database'), 
	('code')
) AS Source (Naam)  
ON Target.Naam = Source.Naam  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Naam) VALUES (Naam);