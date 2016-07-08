MERGE INTO Tag AS Target  
USING (values 
	('MVC' ), 
	('MVVM' ), 
	('Observer' )
) AS Source (Naam)  
ON Target.Naam = Source.Naam  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Naam) VALUES (Naam);