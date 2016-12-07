MERGE INTO Onderdeel AS Target  
USING (values 
	('ABV1') , 
	('ABV2'), 
	('ABV3'), 
	('ABV4'),
	('GRW1'),
	('GRW2'),
	('GRW3'),
	('GRW4'),
	('MOD1'),
	('MOD2'),
	('MOD3'),
	('MOD4')
) AS Source (Code)  
ON Target.Code = Source.Code  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Code) VALUES (Code);