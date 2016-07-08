MERGE INTO Opleiding AS Target  
USING (values 
	('Informatica','Informatica')
) AS Source (Naam, Beschrijving)  
ON Target.Naam = Source.Naam  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Naam, Beschrijving) VALUES (Source.Naam, Source.Beschrijving)
WHEN MATCHED THEN
	UPDATE SET Beschrijving = Source.Beschrijving;