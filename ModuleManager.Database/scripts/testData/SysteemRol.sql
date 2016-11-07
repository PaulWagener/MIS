MERGE INTO SysteemRol AS Target  
USING (values 
	('Admin' ), 
	('Docent' ), 
	('Student' )
) AS Source ([Role])  
ON Target.[Role] = Source.[Role]  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT ([Role]) VALUES ([Role]);