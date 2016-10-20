MERGE INTO [Status] AS Target  
USING (values 
	('Compleet'), 
	('Incompleet'), 
	('Nieuw')
) AS Source (Status1)  
ON Target.Status1 = Source.Status1  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Status1) VALUES (Status1);