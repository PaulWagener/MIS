MERGE INTO Niveau AS Target  
USING (values 
	('Beginner'), 
	('Expert'),
	('Gevorderde')
) AS Source (Niveau1)  
ON Target.Niveau1 = Source.Niveau1
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Niveau1) VALUES (Niveau1);