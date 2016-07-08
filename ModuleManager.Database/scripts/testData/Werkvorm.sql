MERGE INTO Werkvorm AS Target  
USING (values 
	('PR', 'Practicum'), 
	('TH', 'Hoorcollege'), 
	('WS', 'Workshop')
) AS Source ([Type], Omschrijving)  
ON Target.[Type] = Source.[Type]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([Type], Omschrijving) VALUES ([Type], Omschrijving)
WHEN MATCHED THEN
	UPDATE SET Omschrijving = Source.Omschrijving;