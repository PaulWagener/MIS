MERGE INTO Werkvorm AS Target  
USING (values 
	('PR', 'Practicum'), 
	('WS', 'Workshop'),
	(N'GR', N'Groepswerk'),
	(N'HC', N'Hoorcollege'),
	(N'IN', N'Individueel'),
	(N'PU', N'Projectuur'),
	(N'TH', N'Theorie'),
	(N'TU', N'Tutoruur')
) AS Source ([Type], Omschrijving)  
ON Target.[Type] = Source.[Type]
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([Type], Omschrijving) VALUES ([Type], Omschrijving)
WHEN MATCHED THEN
	UPDATE SET Omschrijving = Source.Omschrijving;