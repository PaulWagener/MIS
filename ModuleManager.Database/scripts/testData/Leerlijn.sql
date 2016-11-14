MERGE INTO Leerlijn AS Target  
USING (values 
	('Programmeren'),
	('Testen'),
	('Architectuur')
) AS Source (Naam)  
ON Target.Naam = Source.Naam  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Naam) VALUES (Source.Naam);
