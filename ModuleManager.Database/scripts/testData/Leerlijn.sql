MERGE INTO Leerlijn AS Target  
USING (values 
	('Programmeren', '1516'),
	('Testen', '1516'),
	('Architectuur', '1516')
) AS Source (Naam, Schooljaar)  
ON Target.Naam = Source.Naam  
	AND Target.Schooljaar = Source.Schooljaar
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Naam, Schooljaar) VALUES (Source.Naam, Source.Schooljaar);
