MERGE INTO Fase AS Target  
USING (values 
	('SO', 'Software ontwikkeling', 'Major', 'Informatica'), 
	('BI', 'Bedrijfskundige Informatica', 'Major', 'Informatica')
) AS Source (Naam, Beschrijving, FaseType, Opleidingnaam)  
ON Target.Naam = Source.Naam  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Naam, Beschrijving, FaseType, Opleidingnaam) VALUES (Source.Naam, Source.Beschrijving, Source.FaseType, Source.Opleidingnaam)
WHEN MATCHED THEN
	UPDATE SET
		Beschrijving = Source.Beschrijving, 
		FaseType = Source.FaseType, 
		Opleidingnaam = Source.Opleidingnaam;