MERGE INTO FaseType AS Target  
USING (values 
	('Major'),
	('Minor'),
	('Propedeuse')
) AS Source (Type)  
ON Target.Type = Source.Type  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Type) VALUES (Type);